# Ladevorgänge-API (REST-Beispiel)

Dieses Paket macht das REST-Beispiel aus Kapitel 6 lauffähig: eine fiktive API für Ladevorgänge, die per HTTP `POST`, `PUT` und `GET` angesprochen wird. Der Server hält die Daten nur im Arbeitsspeicher -- ein Neustart löscht alle Ladevorgänge.

## Schnellstart

In zwei Terminalfenstern, jeweils im Verzeichnis `rest`:

```sh
cd ~/collateral/hshl/Energieinformatik/06/rest
./create_environment.sh
./server.sh
```

```sh
./client.sh
```

Der Client legt einen Ladevorgang an (`POST`), aktualisiert die geladene Energiemenge (`PUT`) und liest den Ladevorgang zur Kontrolle erneut (`GET`).

## Endpunkte

| Methode | Pfad | Bedeutung |
|---|---|---|
| `GET` | `/ladevorgaenge` | alle Ladevorgänge auflisten |
| `POST` | `/ladevorgaenge` | neuen Ladevorgang anlegen, Body: `{"zaehlpunkt": "..."}` |
| `GET` | `/ladevorgaenge/<id>` | einen Ladevorgang lesen |
| `PUT` | `/ladevorgaenge/<id>` | einen Ladevorgang ändern, Body: `{"kwh": ...}` |

Unbekannte IDs liefern `404`, ein `POST` ohne `zaehlpunkt` liefert `400`.

## Struktur

```text
server.py               Flask-App mit den vier Endpunkten
client.py                Beispiel-Client: legt an, ändert, liest
requirements.txt         Flask, requests
create_environment.sh    legt .venv an und installiert requirements.txt
server.sh                startet server.py in .venv
client.sh                startet client.py in .venv
```
