import pandas as pd
from sqlalchemy import create_engine
import requests
import io
import Database
from entsoe import EntsoePandasClient
from psycopg2.extras import execute_values

def clear():
    connection = Database.get_connection()
    with connection.cursor() as cur:
        cur.execute("drop schema if exists energy CASCADE;")
        cur.execute("create schema energy;")

def import_renewables():
    print("Lade renewables vom Server...")
    url = "https://data.open-power-system-data.org/renewable_power_plants/2020-08-25/renewable_power_plants_DE.csv"
    request = requests.get(url)
    df = pd.read_csv(io.StringIO(request.text), sep=",", dtype={14: str, 19: str})
    df['commissioning_date'] = pd.to_datetime(df['commissioning_date'])
    df['decommissioning_date'] = pd.to_datetime(df['decommissioning_date'])
    connection_string = Database.get_connection_string()
    engine = create_engine(connection_string)
    print("Importiere renewables in Datenbank...")
    df.to_sql('renewable_power_plants', engine, index=False, if_exists='replace', schema='energy')

def fix_locations_of_renewables():
    print("Korrigiere Standorte von renewables...")
    connection = Database.get_connection()
    with connection.cursor() as cur:
        cur.execute("alter table energy.renewable_power_plants add location geography(POINT);")
        cur.execute("UPDATE energy.renewable_power_plants SET location = ST_SetSRID(ST_MakePoint(lon, lat), 4326);")

@staticmethod
def import_day_ahead_prices(api_key, start, end):
    print(f"Starte Import (DE-LUX) für {start} bis {end}...")
    client = EntsoePandasClient(api_key=api_key)

    try:
        series = client.query_day_ahead_prices('DE_LU', start=start, end=end)
    except Exception as e:
        print(f"Fehler beim Abruf von ENTSO-E: {e}")
        return

    if series.empty:
        print("Keine Daten im gewählten Zeitraum gefunden.")
        return

    df = series.to_frame(name='preis').reset_index()
    df.rename(columns={'index': 'zeitpunkt'}, inplace=True)
    df['preis'] = df['preis'].astype(float)
    data_tuples = [tuple(x) for x in df[['zeitpunkt', 'preis']].to_numpy()]

    conn = None
    try:
        conn = Database.get_connection()
        with conn.cursor() as cur:
            cur.execute("""
                    CREATE TABLE IF NOT EXISTS energy.day_ahead_prices (
                        zeitpunkt TIMESTAMP WITH TIME ZONE PRIMARY KEY,
                        preis NUMERIC(10, 2)
                    );
                """)

            sql = """
                    INSERT INTO energy.day_ahead_prices (zeitpunkt, preis)
                    VALUES %s
                    ON CONFLICT (zeitpunkt) DO UPDATE 
                    SET preis = EXCLUDED.preis;
                """

            execute_values(cur, sql, data_tuples)
            conn.commit()

        print(f"Erfolg: {len(data_tuples)} Preise gespeichert.")
    except Exception as e:
        print(f"Datenbankfehler: {e}")
        if conn:
            conn.rollback()

def import_prices():
    now = pd.Timestamp.now(tz='Europe/Berlin')
    start_date = now - pd.DateOffset(years=2)
    print(f"Starte automatischen Import (Letzte 2 Jahre)...")
    import_day_ahead_prices("", start_date, now)

def load():
    import_prices()
    import_renewables()
    fix_locations_of_renewables()
