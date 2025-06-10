import psycopg2
import os

connection = None

def get_connection_string():
    host = os.getenv("POSTGRES_HOST", "localhost")
    user = os.getenv("POSTGRES_USER", "postgres")
    database = os.getenv("POSTGRES_DATABASE", "postgres")
    password = os.getenv("POSTGRES_PASSWORD", "hshl")
    return f"postgresql://{user}:{password}@{host}:5432/{database}"

def get_connection():
    global connection

    if connection is not None and connection.closed == 0:
        return connection

    # Verbindung neu aufbauen
    host = os.getenv("POSTGRES_HOST", "localhost")
    user = os.getenv("POSTGRES_USER", "postgres")
    database = os.getenv("POSTGRES_DATABASE", "postgres")
    password = os.getenv("POSTGRES_PASSWORD", "hshl")

    try:
        connection = psycopg2.connect(host=host, user=user, password=password, database=database)
        connection.autocommit = True
    except Exception as error:
        print("Fehler beim Aufbau der DB-Verbindung:", error)
        connection = None

    return connection