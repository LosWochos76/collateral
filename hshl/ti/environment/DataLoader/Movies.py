import Database
import pandas as pd
from sentence_transformers import SentenceTransformer
import numpy as np
from flask import Flask, request, render_template_string, render_template

def load_movies():
    df = pd.read_csv("./databases/movies.csv")
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

def create_schema_and_table():
    connection = Database.get_connection()
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

def insert_movies(df):
    connection = Database.get_connection()
    for _, row in df.iterrows():
        with connection.cursor() as cursor:
            cursor.execute("""
                INSERT INTO similarity.movies (title, genre, plot)
                VALUES (%s, %s, %s);
            """, (row["title"], row["genre"], row["plot"]))
    connection.commit()

def import_data():
    create_schema_and_table()
    df_movies = load_movies()
    insert_movies(df_movies)

def update_embeddings():
    connection = Database.get_connection()
    model = SentenceTransformer('paraphrase-multilingual-MiniLM-L12-v2')

    with connection.cursor() as cursor:
        cursor.execute("""
            SELECT id, title, plot FROM similarity.movies
            WHERE embedding IS NULL;
        """)
        rows = cursor.fetchall()

        total = len(rows)
        for index, (movie_id, title, plot) in enumerate(rows, 1):
            full_text = f"{title}. {plot}"
            embedding = model.encode(full_text).astype(np.float32).tolist()
            cursor.execute("""
                UPDATE similarity.movies
                SET embedding = %s
                WHERE id = %s;
            """, (embedding, movie_id))
            print(f"Embedding gespeichert für ID {movie_id}. Verbleibend: {total - index}")

def movies_search():
    model = SentenceTransformer('paraphrase-multilingual-MiniLM-L12-v2')
    connection = Database.get_connection()
    results = []
    query = ""
    query_embedding = None

    if request.method == 'POST':
        query = request.form['query']
        embedding = model.encode(query).astype(np.float32).tolist()
        query_embedding = embedding  # <-- Speichern für Anzeige

        # SQL-Befehl für Debug-Zwecke aufbereiten
        vector_str = str(embedding).replace('[', '[').replace(']', ']')
        debug_sql = f"""
        SELECT title, genre, embedding <#> '{vector_str}'::vector AS distance
        FROM similarity.movies
        ORDER BY distance ASC
        LIMIT 5;
        """
        print("DEBUG: Auszuführender SQL-Befehl:")
        print(debug_sql)

        conn = Database.get_connection()
        with conn.cursor() as cursor:
            cursor.execute("""
                SELECT title, genre, embedding <#> %s::vector AS distance
                FROM similarity.movies
                ORDER BY distance ASC
                LIMIT 5;
            """, (embedding,))
            rows = cursor.fetchall()
            results = [{"title": r[0], "genre": r[1], "distance": r[2]} for r in rows]

    return render_template(
        "movies_search.html",
        results=results,
        query=query,
        query_embedding=query_embedding  # <-- Übergabe an Template
    )