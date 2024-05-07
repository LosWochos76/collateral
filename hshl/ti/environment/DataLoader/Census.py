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

