"""Uplinks der Waermezaehler von ChirpStack per MQTT empfangen und in TimescaleDB speichern."""
import json

import paho.mqtt.client as mqtt
import psycopg

import zeitreihe as z

SQL = """
INSERT INTO messwerte (zeit, geraet, energie_kwh, vorlauf_c, ruecklauf_c)
VALUES (%s, %s, %s, %s, %s)
ON CONFLICT DO NOTHING
"""

conn = psycopg.connect(z.DB_URL, autocommit=True)


def on_connect(client, userdata, flags, reason_code, properties):
    client.subscribe("application/+/device/+/event/up", qos=1)
    print("Warte auf Uplinks ...")


def on_message(client, userdata, message):
    event = json.loads(message.payload)
    name = event["deviceInfo"]["deviceName"]
    werte = event["object"]
    conn.execute(SQL, (event["time"], name, werte["energie_kwh"],
                       werte["vorlauf_c"], werte["ruecklauf_c"]))
    print(event["time"], name, werte["energie_kwh"], "kWh gespeichert")


client = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
client.on_connect = on_connect
client.on_message = on_message
client.connect(z.MQTT_HOST, z.MQTT_PORT)
client.loop_forever()
