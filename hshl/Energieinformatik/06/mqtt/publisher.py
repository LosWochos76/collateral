"""Send one PV measurement to the local teaching broker."""
import json
from datetime import datetime, timezone
from uuid import uuid4
import paho.mqtt.client as mqtt

message = {
    "id": str(uuid4()),
    "timestamp": datetime.now(timezone.utc).isoformat(),
    "power_w": 1840,
}
client = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
client.connect("127.0.0.1", 1883)
client.loop_start()
result = client.publish("anlage/pv1/leistung", json.dumps(message), qos=0)
result.wait_for_publish()
client.loop_stop()
client.disconnect()
print(message)
