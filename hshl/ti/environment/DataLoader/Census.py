import psycopg2
import os

host = os.getenv("POSTGRES_HOST", "localhost")
user = os.getenv("POSTGRES_USER", "postgres")
database = os.getenv("POSTGRES_DATABASE", "postgres")
password = os.getenv("POSTGRES_PASSWORD", "hshl")

try:
    connection = psycopg2.connect(host=host, user=user, password=password, database=database)
    connection.autocommit = True
except (Exception) as error:
    print(error)

def import_data():
    f = open("databases/census.sql", "r")
    sql = f.read()
    f.close()
    with connection.cursor() as cur:
        cur.execute(sql)

def correct_data():
    with connection.cursor() as cur:
        cur.execute("SET search_path TO public, postgis, census;")
        cur.execute("ALTER TABLE census.nyc_census_blocks ADD COLUMN geom_wgs84 geometry(MultiPolygon, 4326);")
        cur.execute("UPDATE census.nyc_census_blocks SET geom_wgs84 = ST_Transform(geom, 4326);");
        cur.execute("ALTER TABLE census.nyc_homicides ADD COLUMN geom_wgs84 geometry(Point, 4326);");
        cur.execute("UPDATE census.nyc_homicides SET geom_wgs84 = ST_Transform(geom, 4326);");
        cur.execute("ALTER TABLE census.nyc_neighborhoods ADD COLUMN geom_wgs84 geometry(MultiPolygon, 4326);")
        cur.execute("UPDATE census.nyc_neighborhoods SET geom_wgs84 = ST_Transform(geom, 4326);");
        cur.execute("ALTER TABLE census.nyc_streets ADD COLUMN geom_wgs84 geometry(MultiLineString, 4326);");
        cur.execute("UPDATE census.nyc_streets SET geom_wgs84 = ST_Transform(geom, 4326);");
        cur.execute("ALTER TABLE census.nyc_subway_stations ADD COLUMN geom_wgs84 geometry(Point, 4326);");
        cur.execute("UPDATE census.nyc_subway_stations SET geom_wgs84 = ST_Transform(geom, 4326);");





