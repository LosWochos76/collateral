"""Receive PV measurements until interrupted."""
import json
import paho.mqtt.client as mqtt


def on_connect(client, userdata, flags, reason_code, properties):
    if reason_code == 0:
        client.subscribe("anlage/#", qos=1)
    else:
        print("Verbindung fehlgeschlagen:", reason_code)


def on_message(client, userdata, message):
    data = json.loads(message.payload.decode("utf-8"))
    print(message.topic, data["timestamp"], data["power_w"], "W", data["id"])


client = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
client.on_connect = on_connect
client.on_message = on_message
client.connect("127.0.0.1", 1883)
client.loop_forever()
