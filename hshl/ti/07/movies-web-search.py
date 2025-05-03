from flask import Flask, request, render_template_string
from sentence_transformers import SentenceTransformer
import psycopg2
import numpy as np

app = Flask(__name__)
model = SentenceTransformer('paraphrase-multilingual-MiniLM-L12-v2')

HTML_TEMPLATE = """
<!doctype html>
<title>Semantische Filmsuche</title>
<h1>Filmsuche</h1>
<form method=post>
  <input type=text name=query placeholder="Suchbegriff" style="width: 300px" value="{{ query or '' }}">
  <input type=submit value=Suchen>
</form>
{% if results %}
  <h2>Ergebnisse für: "{{ query }}"</h2>
  <ul>
  {% for row in results %}
    <li><strong>{{ row.title }}</strong> (Genres: {{ row.genre }}, Distanz: {{ row.distance | round(4) }})</li>
  {% endfor %}
  </ul>
{% endif %}
"""

def get_connection():
    return psycopg2.connect(
        dbname="postgres",
        user="postgres",
        password="hshl",
        host="localhost",
        port="5432"
    )

@app.route('/', methods=['GET', 'POST'])
def search():
    results = []
    query = ""
    if request.method == 'POST':
        query = request.form['query']
        embedding = model.encode(query).astype(np.float32).tolist()

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

        conn = get_connection()
        with conn.cursor() as cursor:
            cursor.execute("""
                SELECT title, genre, embedding <#> %s::vector AS distance
                FROM similarity.movies
                ORDER BY distance ASC
                LIMIT 5;
            """, (embedding,))
            rows = cursor.fetchall()
            results = [{"title": r[0], "genre": r[1], "distance": r[2]} for r in rows]
        conn.close()

    return render_template_string(HTML_TEMPLATE, results=results, query=query)

if __name__ == '__main__':
    app.run(debug=True)