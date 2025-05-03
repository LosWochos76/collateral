import psycopg2
from sentence_transformers import SentenceTransformer
import numpy as np

# Embeddings für Filme erzeugen, bei denen embedding IS NULL
def update_embeddings():
    model = SentenceTransformer('paraphrase-multilingual-MiniLM-L12-v2')

    # Verbindung zur Datenbank (Zugangsdaten anpassen)
    conn = psycopg2.connect(
        dbname="postgres",
        user="postgres",
        password="hshl",
        host="localhost",
        port="5432"
    )

    with conn.cursor() as cursor:
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
            conn.commit()

    conn.close()

if __name__ == "__main__":
    update_embeddings()
