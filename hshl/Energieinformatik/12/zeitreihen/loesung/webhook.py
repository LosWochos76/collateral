"""Musterloesung: Uplinks per Webhook (HTTP-Integration von ChirpStack) empfangen und speichern.

Start aus dem Ordner zeitreihen:   .venv/bin/python loesung/webhook.py
Test in einem zweiten Terminal:    ./daten_erzeugen.sh webhook 48 0.2
"""
import os
import sys
from pathlib import Path

import psycopg
from flask import Flask, request

sys.path.insert(0, str(Path(__file__).resolve().parent.parent))  # zeitreihe.py liegt eine Ebene hoeher
import zeitreihe as z

SQL = """
INSERT INTO messwerte (zeit, geraet, energie_kwh, vorlauf_c, ruecklauf_c)
VALUES (%s, %s, %s, %s, %s)
ON CONFLICT DO NOTHING
"""

app = Flask(__name__)


@app.post("/uplink")
def uplink():
    # ChirpStack sendet alle Ereignisse an dieselbe URL, die Art steht in ?event=
    if request.args.get("event") != "up":
        return "", 204
    event = request.get_json(force=True, silent=True)
    if not event or "object" not in event:
        return {"fehler": "kein Uplink-Ereignis"}, 400
    werte = event["object"]
    try:
        with psycopg.connect(z.DB_URL, autocommit=True) as conn:
            conn.execute(SQL, (event["time"], event["deviceInfo"]["deviceName"],
                               werte["energie_kwh"], werte["vorlauf_c"], werte["ruecklauf_c"]))
    except psycopg.OperationalError:
        # 503: voruebergehend nicht verfuegbar, der Absender darf es spaeter erneut versuchen
        return {"fehler": "Datenbank nicht erreichbar"}, 503
    print(event["time"], event["deviceInfo"]["deviceName"], werte["energie_kwh"], "kWh gespeichert")
    return "", 204


if __name__ == "__main__":
    app.run(host="127.0.0.1", port=int(os.environ.get("WEBHOOK_PORT", "5050")))
