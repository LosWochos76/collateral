# n8n lokal betreiben

Dieses Paket enthält die Infrastruktur für die Praxisfolien der Veranstaltung
Energieinformatik, Kapitel 10. Die Workflow-Plattform n8n läuft als
Docker-Container.

**Windows:** Zu jedem Skript `name.sh` der Übung gibt es ein gleichnamiges `name.bat`. Rufen Sie in der Eingabeaufforderung `name.bat` bzw. in der PowerShell `.\name.bat` auf, z. B. `.\create_environment.bat` statt `./create_environment.sh`.

## 1. n8n starten

```bash
docker compose up -d
```

n8n ist danach im Browser unter `http://localhost:5678` erreichbar. Beim
ersten Start wird ein lokales Owner-Konto angelegt.

Workflows, Zugangsdaten und Einstellungen liegen im Docker-Volume `n8n_data`
und überleben damit einen Neustart des Containers.

Neben n8n startet ein zweiter Container `n8n-runners`. Darin laufen die
Code-Nodes in JavaScript und Python. Python läuft dort abgeschottet: Importe
sind nur erlaubt, wenn sie in `n8n-task-runners.json` freigegeben sind
(Eintrag `N8N_RUNNERS_STDLIB_ALLOW`, hier `datetime,time`). Die Versionen
von `n8n` und `runners` in der `docker-compose.yaml` müssen übereinstimmen.

## 2. Dienste auf dem eigenen Rechner erreichen

n8n läuft in einem Container. Die Adresse `127.0.0.1` zeigt dort auf den
Container selbst, nicht auf Ihren Rechner. Dienste auf dem eigenen Rechner
(z. B. die Ladevorgänge-API aus `06/rest` oder den MQTT-Broker aus
`06/mqtt`) erreichen Sie aus n8n unter `host.docker.internal`, also z. B.
`http://host.docker.internal:5050`.

## 3. Workflows zum Lieferantenwechsel importieren

Im Ordner `workflows/` liegen drei Workflows. Importieren Sie sie in n8n über
*Create workflow* und dort im Menü *...* -> *Import* -> *From file*:

1. `1-tabellen-anlegen.json` legt die Data Tables `marktlokationen`,
   `zuordnungen` und `klaerfaelle` an und füllt `marktlokationen` mit
   fiktiven Beispieldaten. Einmal mit *Execute workflow* ausführen.
2. `2-anmeldung-netzbetreiber.json` ist der Prozess des Netzbetreibers:
   Anmeldung annehmen, prüfen, bestätigen oder ablehnen, den bisherigen
   Lieferanten informieren und höchstens 2 Minuten auf dessen Antwort warten.
   Bleibt die Antwort aus, entsteht ein Eintrag in `klaerfaelle`.
3. `3-lieferant-alt-simulation.json` simuliert den bisherigen Lieferanten.
   Setzen Sie im Node *LFA antwortet?* den Wert auf `false`, um eine
   Fristverletzung zu provozieren.

Workflow 3 muss veröffentlicht sein (*Publish*), damit Workflow 2 ihn
erreicht.

## 4. Anmeldungen senden

Die genaue URL zeigt der Webhook-Node *Anmeldung*:

- **Test-URL** `http://localhost:5678/webhook-test/anmeldung`: nur aktiv,
  nachdem Sie im Editor *Execute workflow* geklickt haben, und nur für einen
  Aufruf. Der Weg der Daten ist dann direkt im Editor zu sehen.
- **Production-URL** `http://localhost:5678/webhook/anmeldung`: aktiv,
  sobald der Workflow veröffentlicht ist (*Publish*). Die Ausführungen stehen
  dann unter *Executions*.

### Weg 1: Python-Skript (Terminal oder PyCharm)

```bash
./create_environment.sh
./anmeldung_senden.sh                 # gültige Anmeldung
./anmeldung_senden.sh ohne_termin     # Pflichtangabe fehlt
./anmeldung_senden.sh termin_gestern  # Liefertermin zu früh
./anmeldung_senden.sh malo_unbekannt  # MaLo-ID unbekannt
./anmeldung_senden.sh konkurrierend   # zweiter LFN, gleiche MaLo, gleicher Termin
```

In PyCharm den Ordner `n8n` als Projekt öffnen, die angebotene virtuelle
Umgebung aus `requirements.txt` anlegen lassen und `anmeldung_senden.py`
ausführen (Fall als Parameter in der Run-Konfiguration). Die URL steht oben
im Skript in der Variablen `URL`.

### Weg 2: curl

```bash
curl -X POST http://localhost:5678/webhook-test/anmeldung \
  -H "Content-Type: application/json" -d @anmeldung.json
```

Unter Windows ist curl ab Windows 10 enthalten. In der PowerShell muss es als
`curl.exe` aufgerufen werden, weil `curl` dort ein anderer Befehl ist. Die
Beispielanmeldung liegt in `anmeldung.json`; das Datum `lieferbeginn` muss
mindestens einen Werktag in der Zukunft liegen.

## 5. Übung: Ladevorgänge als Prozess

In der Übung bauen Sie selbst einen Prozess, der die Ladevorgänge-API aus
`06/rest` als Fachsystem nutzt.

1. `workflows/4-laden-tabellen-anlegen.json` importieren und einmal
   ausführen. Er legt die Data Tables `ladekarten` (drei fiktive Karten,
   `K-1003` ist gesperrt) und `abrechnungen` an.
2. Die Ladevorgänge-API starten (`06/rest/server.sh`). Aus n8n ist sie unter
   `http://host.docker.internal:5050` erreichbar. Unter Linux muss der Server
   dafür in `server.py` an `0.0.0.0` statt `127.0.0.1` gebunden werden.
3. Die Ladesäule simuliert das Skript `ladesaeule.py`. Es ruft die Webhooks
   `ladung-start` und `ladung-ende` auf, die Sie selbst anlegen:

```bash
./ladesaeule.sh start K-1001          # Ladekarte vorhalten
./ladesaeule.sh start K-1001 DE0002   # an einem anderen Zählpunkt
./ladesaeule.sh ende 1 12.5           # Ladevorgang 1 mit 12,5 kWh beenden
./ladesaeule.sh --test start K-1001   # Test-URL statt Production-URL
```

Eine Musterlösung für die Basis-Aufgaben liegt in `workflows/loesung/`
(`5-ladung-starten.json`, `6-ladung-beenden.json`). Schauen Sie erst hinein,
wenn Sie die Workflows selbst gebaut haben.

## 6. n8n beenden

```bash
docker compose down
```

Das Volume bleibt dabei erhalten. Mit `docker compose down -v` wird es
ebenfalls gelöscht, alle Workflows sind dann weg.
