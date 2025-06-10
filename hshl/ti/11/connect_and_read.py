import psycopg2

# Verbindung zur Datenbank herstellen
conn = psycopg2.connect(
    host="172.25.200.52", port=5432, dbname="postgres", user="postgres", password="hshl"
)

# SQL-Abfrage ausführen
sql = "select * from bundesliga.spiele;"
with conn.cursor() as cur:
    cur.execute(sql)
    rows = cur.fetchall()

# Iterieren über die Ergebnisse
for row in rows:
    print(row) # Jeder Datensatz ist ein Tupel
    # Beispiel: (72564, 15, datetime.datetime(2024, 12, 8, 13, 30), 181, 65, 0, 1)

conn.close()