import psycopg2
import os
import requests
from dateutil import parser
import datetime

host = os.getenv("POSTGRES_HOST", "localhost")
user = os.getenv("POSTGRES_USER", "postgres")
database = os.getenv("POSTGRES_DATABASE", "postgres")
password = os.getenv("POSTGRES_PASSWORD", "hshl")

try:
    connection = psycopg2.connect(host=host, user=user, password=password, database=database)
    connection.autocommit = True
except (Exception) as error:
    print(error)

def clear():
    with connection.cursor() as cur:
        cur.execute("drop schema if exists wetter CASCADE;")
        cur.execute("create schema wetter;")
        cur.execute("""create table wetter.wetterstationen (
                    stations_id serial primary key,
                    standort varchar(200) not null,
                    hoehe double precision not null,
                    geo_breite double PRECISION not null,
                    geo_hoehe double PRECISION not null,
                    bundesland varchar(200) not null);""")

def load_stationen():
    url = "https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/hourly/air_temperature/recent/TU_Stundenwerte_Beschreibung_Stationen.txt"
    response = requests.get(url)
    lines = response.text.split("\r\n")
    yesterday = datetime.date.today() - datetime.timedelta(days=1)

    with connection.cursor() as cur:
        for i in range(2, len(lines)-1):
            id = int(lines[i][0:5])
            from_date = parser.parse(lines[i][21:29])
            to_date = parser.parse(lines[i][30:39])
            stationshoehe = float(lines[i][41:53])
            geo_breite = float(lines[i][54:65])
            geo_hoehe = float(lines[i][66:75])
            stationsname = lines[i][76:156].rstrip()
            bundesland = lines[i][157:].rstrip()
            sql = f"""insert into wetter.wetterstationen (stations_id, standort, hoehe, geo_breite, geo_hoehe, bundesland) values
            ({id}, '{stationsname}', {stationshoehe}, {geo_breite}, {geo_hoehe}, '{bundesland}')"""
            cur.execute(sql)

def load_messungen():
    # stündliche Daten: https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/hourly/
    pass
