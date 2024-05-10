import psycopg2
import os
import requests
import json

host = os.getenv("POSTGRES_HOST", "localhost")
user = os.getenv("POSTGRES_USER", "postgres")
database = os.getenv("POSTGRES_DATABASE", "postgres")
password = os.getenv("POSTGRES_PASSWORD", "hshl")
connection = psycopg2.connect(host=host, user=user, password=password, database=database)
connection.autocommit = True

def clear():
    global connection
    with connection.cursor() as cur:
        cur.execute("drop schema if exists bundesliga CASCADE;")
        cur.execute("create schema bundesliga;")
        cur.execute("""create table bundesliga.vereine (
                    verein_id serial primary key,
                    name varchar(200) not null,
                    liga int not null)""")
        cur.execute("""create table bundesliga.spiele (
            spiel_id serial primary key,
            spieltag int not null,
            datum timestamp not null,
            heim_team int not null,
            gast_team int not null,
            tore_heim int,
            tore_gast int)""")

def get_current_season():
    current_year = datetime.datetime.now().year
    if datetime.datetime.now() < datetime.datetime(current_year, 8, 1):
        return current_year - 1
    else:
        return current_year

def load_vereine(season, league):
    global connection
    url = f"https://api.openligadb.de/getavailableteams/bl{league}/{season}"
    request = requests.get(url)
    vereine = json.loads(request.text)

    with connection.cursor() as cur:
        for verein in vereine:
            sql = f"insert into bundesliga.vereine (verein_id, name, liga) values ({verein['teamId']}, '{verein['teamName']}', {league});"
            cur.execute(sql)

def load_spiele(season, league):
    global connection
    url = f"https://api.openligadb.de/getmatchdata/bl{league}/{season}"
    request = requests.get(url)
    spiele = json.loads(request.text)

    with connection.cursor() as cur:
        for spiel in spiele:
            tore_heim = 'NULL' if len(spiel['matchResults']) == 0 else spiel['matchResults'][0]['pointsTeam1']
            tore_gast = 'NULL' if len(spiel['matchResults']) == 0 else spiel['matchResults'][0]['pointsTeam2']
            sql = f"""insert into bundesliga.spiele (spiel_id, spieltag, datum, heim_team, gast_team, tore_heim, tore_gast) values
                ({spiel['matchID']}, {spiel['group']['groupOrderID']}, '{spiel['matchDateTime']}', 
                {spiel['team1']['teamId']}, {spiel['team2']['teamId']}, 
                {tore_heim}, {tore_gast})"""
            cur.execute(sql)
