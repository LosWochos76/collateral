import pandas as pd
import psycopg2

# 1. CSV-Datei einlesen und vorbereiten
def load_movies():
    df = pd.read_csv("movies.csv")
    movies = []

    for _, row in df.iterrows():
        title = row.get("title", "").strip()
        genre = row.get("genre", "").strip()
        wiki_plot = row.get("wiki_plot", "") or ""
        imdb_plot = row.get("imdb_plot", "") or ""
        plot = f"{wiki_plot}\n{imdb_plot}".strip()

        if title and plot:
            movies.append({"title": title, "genre": genre, "plot": plot})

    return pd.DataFrame(movies)

# 2. Schema und Tabelle in Postgres erzeugen (ohne Embeddings)
def create_schema_and_table(connection):
    with connection.cursor() as cursor:
        cursor.execute("""
        DROP SCHEMA IF EXISTS similarity CASCADE;
        CREATE SCHEMA IF NOT EXISTS similarity;
        CREATE EXTENSION IF NOT EXISTS vector;
        CREATE TABLE IF NOT EXISTS similarity.movies (
            id SERIAL PRIMARY KEY,
            title TEXT,
            genre TEXT,
            plot TEXT,
            embedding VECTOR(384)
        );
        """)
        connection.commit()

# 3. Filme einfügen (nur Metadaten und Plot, keine Embeddings)
def insert_movies(df, connection):
    for _, row in df.iterrows():
        with connection.cursor() as cursor:
            cursor.execute("""
                INSERT INTO similarity.movies (title, genre, plot)
                VALUES (%s, %s, %s);
            """, (row["title"], row["genre"], row["plot"]))
    connection.commit()

# Hauptfunktion zur Ausführung der Schritte
def main():
    df_movies = load_movies()

    # Verbindung zur Datenbank (Zugangsdaten anpassen)
    conn = psycopg2.connect(
        dbname="postgres",
        user="postgres",
        password="hshl",
        host="localhost",
        port="5432"
    )

    create_schema_and_table(conn)
    insert_movies(df_movies, conn)
    conn.close()

if __name__ == "__main__":
    main()
