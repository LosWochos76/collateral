import psycopg2
import folium
import numpy as np

conn = psycopg2.connect(
    #host="172.25.200.52", port=5432, dbname="postgres", user="postgres", password="hshl"
    host="localhost", port=5432, dbname="postgres", user="postgres", password="hshl"
)


with conn.cursor() as cur:
    cur.execute("SELECT stations_id, name,  ST_X(location::geometry) AS lon, ST_Y(location::geometry) AS lat  FROM wetter.stationen;")
    stationen = cur.fetchall()

lat_mittel = np.mean([s[3] for s in stationen])
lon_mittel = np.mean([s[2] for s in stationen])
karte = folium.Map(location=[lat_mittel, lon_mittel], zoom_start=7)
for id, name, lon, lat in stationen:
    marker = folium.Marker(location=[lat, lon], popup=name, icon=folium.Icon(color="blue", icon="cloud"))
    marker.add_to(karte)

karte.save("wetterstationen_karte.html")
print("Karte erfolgreich exportiert.")