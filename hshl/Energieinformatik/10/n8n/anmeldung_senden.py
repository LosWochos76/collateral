"""Sendet eine Anmeldung an den n8n-Workflow des Netzbetreibers.

Aufruf:
    python anmeldung_senden.py                 # gültige Anmeldung
    python anmeldung_senden.py ohne_termin     # Pflichtangabe fehlt
    python anmeldung_senden.py termin_gestern  # Liefertermin zu früh
    python anmeldung_senden.py malo_unbekannt  # MaLo-ID unbekannt
"""
import sys
from datetime import date, timedelta

import requests

# Die genaue URL steht im Webhook-Node von n8n:
# Test-URL (".../webhook-test/...") nach "Execute workflow",
# Production-URL (".../webhook/...") nach dem Veröffentlichen des Workflows.
URL = "http://localhost:5678/webhook/anmeldung"


def naechster_werktag(tage: int) -> str:
    """Ein Werktag mindestens `tage` Tage in der Zukunft als ISO-Datum."""
    tag = date.today() + timedelta(days=tage)
    while tag.weekday() >= 5:
        tag += timedelta(days=1)
    return tag.isoformat()


anmeldung = {
    "nachricht_id": "A-0001",
    "malo_id": "51238696781",
    "lieferbeginn": naechster_werktag(3),
    "lfn": "9900000000001",
}

fall = sys.argv[1] if len(sys.argv) > 1 else "gueltig"
if fall == "ohne_termin":
    del anmeldung["lieferbeginn"]
elif fall == "termin_gestern":
    anmeldung["lieferbeginn"] = (date.today() - timedelta(days=1)).isoformat()
elif fall == "malo_unbekannt":
    anmeldung["malo_id"] = "51000000045"

print("Sende:", anmeldung)
antwort = requests.post(URL, json=anmeldung, timeout=10)
print("Status:", antwort.status_code)
print("Antwort:", antwort.text)
