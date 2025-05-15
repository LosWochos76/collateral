import pandas as pd
from sqlalchemy import create_engine
import requests
import io
import Database

def clear():
    connection = Database.get_connection()
    with connection.cursor() as cur:
        cur.execute("drop schema if exists energy CASCADE;")
        cur.execute("create schema energy;")

def import_data():
    url = "https://data.open-power-system-data.org/renewable_power_plants/2020-08-25/renewable_power_plants_DE.csv"
    request = requests.get(url)
    df = pd.read_csv(io.StringIO(request.text), sep=",", dtype={14: str, 19: str})
    df['commissioning_date'] = pd.to_datetime(df['commissioning_date'])
    df['decommissioning_date'] = pd.to_datetime(df['decommissioning_date'])
    connection_string = Database.get_connection_string()
    engine = create_engine(connection_string)
    df.to_sql('renewable_power_plants', engine, index=False, if_exists='replace', schema='energy')

def fix_data():
    connection = Database.get_connection()
    with connection.cursor() as cur:
        cur.execute("alter table energy.renewable_power_plants add location geography(POINT);")
        cur.execute("UPDATE energy.renewable_power_plants SET location = ST_SetSRID(ST_MakePoint(lon, lat), 4326);")