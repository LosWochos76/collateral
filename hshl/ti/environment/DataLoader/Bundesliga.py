import psycopg2
import os
import requests
import json

host = os.getenv("POSTGRES_HOST", "localhost")
user = os.getenv("POSTGRES_USER", "postgres")
password = os.getenv("POSTGRES_PASSWORD", "hshl")

try:
    with psycopg2.connect(host=host, user=user, password=password) as connection:
        connection.autocommit = True
        with connection.cursor() as cur:
            cur.execute("create database bundesliga;")
        connection.close()
except (Exception) as error:
    print(error)

def clear_vereine():
    with psycopg2.connect(host=host, user=user, password=password, database="bundesliga") as connection:
        connection.autocommit = True
        with connection.cursor() as cur:
            cur.execute("drop table if exists vereine;")
            cur.execute("""create table vereine (
                        verein_id serial primary key,
                        name varchar(200) not null,
                        liga int not null)""")

def load_vereine(season, league):
    url = f"https://api.openligadb.de/getavailableteams/bl{league}/{season}"
    request = requests.get(url)
    vereine = json.loads(request.content)

    with psycopg2.connect(host=host, user=user, password=password, database="bundesliga") as connection:
        connection.autocommit = True
        with connection.cursor() as cur:
            for verein in vereine:
                sql = f"insert into vereine (verein_id, name, liga) values ({verein['teamId']}, '{verein['teamName']}', {league});"
                cur.execute(sql)

def clear_spiele():
    with psycopg2.connect(host=host, user=user, password=password, database="bundesliga") as connection:
        connection.autocommit = True
        with connection.cursor() as cur:
            cur.execute("drop table if exists spiele;")
            cur.execute("""create table spiele (
                spiel_id serial primary key,
                spieltag int not null,
                datum timestamp not null,
                heim_team int not null,
                gast_team int not null,
                tore_heim int,
                tore_gast int)""")

def load_spiele(season, league):
    url = f"https://api.openligadb.de/getmatchdata/bl{league}/{season}"
    request = requests.get(url)
    spiele = json.loads(request.content)

    with psycopg2.connect(host=host, user=user, password=password, database="bundesliga") as connection:
        connection.autocommit = True
        with connection.cursor() as cur:
            for spiel in spiele:
                print(spiel)
                sql = f"""insert into spiele (spiel_id, spieltag, datum, heim_team, gast_team, tore_heim, tore_gast) values
                    ({spiel['matchID']}, {spiel['group']['groupOrderID']}, '{spiel['matchDateTime']}', 
                    {spiel['team1']['teamId']}, {spiel['team2']['teamId']}, 
                    {spiel['matchResults'][0]['pointsTeam1']}, {spiel['matchResults'][0]['pointsTeam2']})"""
                print(sql)
                cur.execute(sql)