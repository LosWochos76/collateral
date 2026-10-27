"""Beispieldaten erzeugen und laden.

    daten_erzeugen.py csv               Daten neu erzeugen und in daten/waerme.csv schreiben
    daten_erzeugen.py db                daten/waerme.csv in TimescaleDB laden
    daten_erzeugen.py uplinks [N] [S]   N Zeilen als ChirpStack-Uplinks per MQTT senden,
                                        S Sekunden Pause dazwischen (Vorgabe: 48 und 0.2)
    daten_erzeugen.py webhook [N] [S] [URL]
                                        dieselben Uplinks per HTTP POST an URL?event=up senden,
                                        wie die HTTP-Integration von ChirpStack
                                        (Vorgabe: 48, 0.2 und http://localhost:5050/uplink)
"""
import base64
import json
import sys
import time
import urllib.error
import urllib.request

import paho.mqtt.client as mqtt
import psycopg

import zeitreihe as z

APPLICATION_ID = "5b0e6a3c-1f2d-4c1e-9a77-3d2f8e6b1a90"
WEBHOOK_URL = "http://localhost:5050/uplink"


def nach_csv():
    daten = z.erzeuge_daten()
    z.schreibe_csv(daten)
    print(f"{len(daten)} Zeilen nach {z.CSV_DATEI} geschrieben")


def in_datenbank():
    daten = z.lade_csv()
    wetter = daten[daten["geraet"] == "wmz-haupt"][["zeit", "aussentemperatur_c"]]
    with psycopg.connect(z.DB_URL, autocommit=True) as conn:
        with conn.cursor() as cur:
            with cur.copy("COPY messwerte (zeit, geraet, energie_kwh, vorlauf_c, ruecklauf_c) FROM STDIN") as copy:
                for zeile in daten.itertuples():
                    copy.write_row((zeile.zeit, zeile.geraet, zeile.energie_kwh,
                                    zeile.vorlauf_c, zeile.ruecklauf_c))
            with cur.copy("COPY wetter (zeit, temperatur_c) FROM STDIN") as copy:
                for zeile in wetter.itertuples():
                    copy.write_row((zeile.zeit.floor("h"), zeile.aussentemperatur_c))
            # Verdichtung sofort aktualisieren, statt auf die Policy zu warten
            cur.execute("CALL refresh_continuous_aggregate('zaehler_stunde', NULL, NULL)")
    print(f"{len(daten)} Messwerte und {len(wetter)} Temperaturwerte geladen")


def uplink(zeile, dev_eui, f_cnt):
    """Ein gekuerztes Uplink-Ereignis, aufgebaut wie bei ChirpStack (Kapitel 8)."""
    werte = {"energie_kwh": zeile.energie_kwh, "vorlauf_c": zeile.vorlauf_c,
             "ruecklauf_c": zeile.ruecklauf_c, "batterie_schwach": False, "leckage": False}
    return {
        "time": zeile.zeit.strftime("%Y-%m-%dT%H:%M:%S.000Z"),
        "deviceInfo": {"applicationId": APPLICATION_ID, "applicationName": "mfh-waerme",
                       "deviceName": zeile.geraet, "devEui": dev_eui},
        "fCnt": f_cnt,
        "fPort": 1,
        "data": base64.b64encode(json.dumps(werte).encode()).decode(),
        "object": werte,
    }


def uplinks_senden(anzahl=48, pause=0.2):
    daten = z.lade_csv().sort_values("zeit").head(anzahl)
    client = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
    client.connect(z.MQTT_HOST, z.MQTT_PORT)
    client.loop_start()
    for f_cnt, zeile in enumerate(daten.itertuples()):
        dev_eui = z.GERAETE[zeile.geraet][0]
        topic = f"application/{APPLICATION_ID}/device/{dev_eui}/event/up"
        client.publish(topic, json.dumps(uplink(zeile, dev_eui, f_cnt)), qos=1).wait_for_publish()
        print(zeile.zeit, zeile.geraet, zeile.energie_kwh, "kWh")
        time.sleep(pause)
    client.loop_stop()
    client.disconnect()


def webhook_senden(anzahl=48, pause=0.2, url=WEBHOOK_URL):
    """Wie die HTTP-Integration von ChirpStack: ein POST je Ereignis, Art des Ereignisses in ?event=."""
    daten = z.lade_csv().sort_values("zeit").head(anzahl)
    for f_cnt, zeile in enumerate(daten.itertuples()):
        dev_eui = z.GERAETE[zeile.geraet][0]
        body = json.dumps(uplink(zeile, dev_eui, f_cnt)).encode()
        anfrage = urllib.request.Request(f"{url}?event=up", data=body, method="POST",
                                         headers={"Content-Type": "application/json"})
        try:
            with urllib.request.urlopen(anfrage, timeout=5) as antwort:
                status = antwort.status
        except urllib.error.HTTPError as fehler:
            status = fehler.code
        except urllib.error.URLError as fehler:
            print(f"Empfaenger {url} nicht erreichbar: {fehler.reason}")
            return
        print(zeile.zeit, zeile.geraet, zeile.energie_kwh, "kWh, Antwort", status)
        time.sleep(pause)


if __name__ == "__main__":
    befehl = sys.argv[1] if len(sys.argv) > 1 else ""
    if befehl == "csv":
        nach_csv()
    elif befehl == "db":
        in_datenbank()
    elif befehl == "uplinks":
        anzahl = int(sys.argv[2]) if len(sys.argv) > 2 else 48
        pause = float(sys.argv[3]) if len(sys.argv) > 3 else 0.2
        uplinks_senden(anzahl, pause)
    elif befehl == "webhook":
        anzahl = int(sys.argv[2]) if len(sys.argv) > 2 else 48
        pause = float(sys.argv[3]) if len(sys.argv) > 3 else 0.2
        url = sys.argv[4] if len(sys.argv) > 4 else WEBHOOK_URL
        webhook_senden(anzahl, pause, url)
    else:
        print(__doc__)
