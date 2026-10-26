"""Simuliert eine Ladesäule, die Ihre n8n-Workflows aufruft.

Aufruf:
    python ladesaeule.py start K-1001             # Ladekarte vorhalten
    python ladesaeule.py start K-1001 DE0001      # ... an einem anderen Zählpunkt
    python ladesaeule.py ende 1 12.5              # Ladevorgang 1 mit 12,5 kWh beenden
    python ladesaeule.py --test start K-1001      # Test-URL statt Production-URL
"""
import sys

import requests

BASIS = "http://localhost:5678"
ZAEHLPUNKT = "DE0000000000000000000000000000001"

args = sys.argv[1:]
# Test-URL (".../webhook-test/...") nach "Execute workflow",
# Production-URL (".../webhook/...") nach dem Veröffentlichen des Workflows.
pfad = "webhook"
if args and args[0] == "--test":
    pfad = "webhook-test"
    args = args[1:]

if len(args) >= 2 and args[0] == "start":
    url = f"{BASIS}/{pfad}/ladung-start"
    nachricht = {"ladekarte": args[1], "zaehlpunkt": args[2] if len(args) > 2 else ZAEHLPUNKT}
elif len(args) == 3 and args[0] == "ende":
    url = f"{BASIS}/{pfad}/ladung-ende"
    nachricht = {"id": int(args[1]), "kwh": float(args[2])}
else:
    print(__doc__)
    sys.exit(1)

print("Sende an", url, ":", nachricht)
antwort = requests.post(url, json=nachricht, timeout=30)
print("Status:", antwort.status_code)
print("Antwort:", antwort.text)
