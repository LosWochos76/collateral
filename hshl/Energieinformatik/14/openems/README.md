# OpenEMS mit simuliertem Haushalt

Dieses Paket enthält die Infrastruktur für die Praxisfolien der Veranstaltung
Energieinformatik, Kapitel 14. Das Energiemanagementsystem
[OpenEMS](https://openems.io) läuft als Docker-Container: die **OpenEMS Edge**
mit simulierten Geräten und die Weboberfläche **OpenEMS UI**.

Der simulierte Haushalt hat eine PV-Anlage, einen Batteriespeicher
(10 kWh, 5 kW) und einen Netzanschluss. Der Controller
„Eigenverbrauchsoptimierung“ lädt und entlädt den Speicher so, dass möglichst
kein Strom aus dem Netz bezogen oder eingespeist wird. Die Profile für
Verbrauch und PV laufen im Zeitraffer: Ein simulierter Tag dauert 24 Minuten.

## 1. Container starten

```bash
docker compose up -d
```

Es starten zwei Container:

| Dienst | Adresse | Zugang |
|---|---|---|
| OpenEMS UI | `http://localhost:4200` | Passwort `admin` (oder leer lassen für den Gast-Zugang) |
| Apache Felix Web Console | `http://localhost:8080/system/console/configMgr` | Benutzer `admin`, Passwort `admin` |
| REST-Schnittstelle | `http://localhost:8084/rest/...` | Benutzer beliebig, z. B. `x`, Passwort `admin` |

Die Zugangsdaten sind nur für den lokalen Betrieb gedacht. Der erste Start
dauert etwa eine Minute, bis die Edge alle Komponenten geladen hat. Läuft auf
Ihrem Rechner schon ein Dienst auf Port 4200, 8080, 8084 oder 8085, ändern Sie
den linken Port in der `docker-compose.yaml`.

## 2. Die Oberfläche öffnen

Öffnen Sie `http://localhost:4200` und melden Sie sich mit dem Passwort
`admin` an. Unter **Live** sehen Sie den Energiemonitor mit PV-Erzeugung,
Verbrauch, Speicher und Netz. Nach einigen Minuten beginnt die simulierte
PV-Anlage zu erzeugen, und der Speicher lädt.

## 3. Die Konfiguration ansehen und ändern

Beim **ersten Start** kopiert die Edge die Dateien aus dem Ordner `config/`
in das Docker-Volume `openems-edge-conf`. Danach gilt nur noch die
Konfiguration im Volume; Änderungen nehmen Sie in der Apache Felix Web
Console vor (Menü „OSGi → Configuration“).

Konfigurierte Komponenten:

| Komponente | ID | Aufgabe |
|---|---|---|
| Simulator DataSource: CSV Predefined | `datasource0` | Lastprofil eines Haushalts |
| Simulator DataSource: CSV Predefined | `datasource1` | PV-Erzeugung eines Sommertags |
| Simulator NRCMeter Acting | `meter1` | Verbrauch aus `datasource0` |
| Simulator ProductionMeter Acting | `meter2` | PV-Anlage aus `datasource1` |
| Simulator GridMeter Reacting | `meter0` | Netzanschluss, ergibt sich aus der Bilanz |
| Simulator EssSymmetric Reacting | `ess0` | Batteriespeicher |
| Controller Ess Balancing | `ctrlBalancing0` | Eigenverbrauchsoptimierung |
| Controller Api REST/JSON Read-Write | `ctrlApiRest0` | REST-Schnittstelle auf Port 8084 |
| Controller Api Websocket | `ctrlApiWebsocket0` | Verbindung zur UI auf Port 8085 |
| Controller Debug Log | `ctrlDebugLog0` | schreibt jede Sekunde die wichtigsten Werte ins Log |
| Scheduler All Alphabetically | `scheduler0` | führt alle Controller in alphabetischer Reihenfolge aus |

Das Log der Edge sehen Sie mit

```bash
docker compose logs -f openems-edge
```

Um wieder mit der mitgelieferten Konfiguration zu beginnen, löschen Sie die
Volumes (siehe Schritt 5) und starten neu.

## 4. Werte über die REST-Schnittstelle lesen

```bash
curl http://x:admin@localhost:8084/rest/channel/_sum/EssSoc
curl "http://x:admin@localhost:8084/rest/channel/meter.*/ActivePower"
```

Die Antwort ist JSON, z. B.

```json
{"address":"_sum/EssSoc","type":"INTEGER","accessMode":"RO","text":"Range 0..100","unit":"%","value":50}
```

`_sum` fasst alle Geräte zusammen, z. B. `_sum/ProductionActivePower`,
`_sum/ConsumptionActivePower` und `_sum/GridActivePower`. Unter Windows ist
`curl` ab Windows 10 enthalten; in der PowerShell `curl.exe` statt `curl`
verwenden.

## 5. Aufräumen

```bash
docker compose down        # Container stoppen
docker compose down -v     # zusätzlich die Konfiguration und Daten löschen
```

## Hinweise

- Verwendet werden die Images `openems/edge` und `openems/ui-edge` in der
  Version 2026.10.0 (amd64 und arm64, also auch für Apple-Rechner mit
  M-Prozessor).
- Die Dateien in `config/` haben das Format der Apache Felix
  Configuration Admin. Neben den Simulator-Komponenten enthalten sie die
  Grundkonfiguration aus dem offiziellen Image.
- Dokumentation: <https://openems.github.io/openems.io/openems/latest/>
