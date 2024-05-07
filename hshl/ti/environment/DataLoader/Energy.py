import pandas as pd
from sqlalchemy import create_engine
import requests
import os
import psycopg2
import io

host = os.getenv("POSTGRES_HOST", "localhost")
user = os.getenv("POSTGRES_USER", "postgres")
database = os.getenv("POSTGRES_DATABASE", "postgres")
password = os.getenv("POSTGRES_PASSWORD", "hshl")
connection_string = f"postgresql://{user}:{password}@{host}:5432/{database}"

try:
    connection = psycopg2.connect(host=host, user=user, password=password, database=database)
    connection.autocommit = True
except (Exception) as error:
    print(error)

def clear():
    with connection.cursor() as cur:
        cur.execute("drop schema if exists energy CASCADE;")
        cur.execute("create schema energy;")

def import_data():
    url = "https://data.open-power-system-data.org/renewable_power_plants/2020-08-25/renewable_power_plants_DE.csv"
    request = requests.get(url)
    df = pd.read_csv(io.StringIO(request.text), sep=",")
    df['commissioning_date'] = pd.to_datetime(df['commissioning_date'])
    df['decommissioning_date'] = pd.to_datetime(df['decommissioning_date'])
    engine = create_engine(connection_string)
    df.to_sql('renewable_power_plants', engine, index=False, if_exists='replace', schema='energy')

def fix_data():
    with connection.cursor() as cur:
        cur.execute("alter table energy.renewable_power_plants add location geography(POINT);")
        cur.execute("UPDATE energy.renewable_power_plants SET location = ST_SetSRID(ST_MakePoint(lon, lat), 4326);")