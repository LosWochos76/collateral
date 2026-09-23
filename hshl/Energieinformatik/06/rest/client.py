"""Legt einen Ladevorgang an, aktualisiert ihn und liest ihn erneut -- siehe Kapitel 6."""
import requests

url = "http://localhost:5050/ladevorgaenge"

neu = requests.post(url, json={"zaehlpunkt": "DE0001234"})
ladevorgang_id = neu.json()["id"]
print("Angelegt:", neu.status_code, neu.json())

update = requests.put(f"{url}/{ladevorgang_id}", json={"kwh": 12.5})
print("Aktualisiert:", update.status_code, update.json())

kontrolle = requests.get(f"{url}/{ladevorgang_id}")
print("Kontrolle:", kontrolle.status_code, kontrolle.json())
