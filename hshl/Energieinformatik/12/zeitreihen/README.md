# Zeitreihen speichern, anzeigen und prognostizieren

Dieses Paket enthält die Infrastruktur und die Skripte für die Praxisfolien der
Veranstaltung Energieinformatik, Kapitel 12. Die Zeitreihendatenbank
TimescaleDB, Grafana und ein MQTT-Broker laufen als Docker-Container, die
Auswertungen und Prognosen als Python-Skripte.

**Windows:** Zu jedem Skript `name.sh` gibt es ein gleichnamiges `name.bat`. Rufen Sie in der Eingabeaufforderung `name.bat` bzw. in der PowerShell `.\name.bat` auf, z. B. `.\create_environment.bat` statt `./create_environment.sh`.

## Das Beispiel

Der Wärmezähler aus Kapitel 8 misst den Wärmeverbrauch eines
Mehrfamilienhauses und sendet stündlich seinen **Zählerstand** sowie Vor- und
Rücklauftemperatur über LoRaWAN an ChirpStack. Es gibt zwei Zähler:
`wmz-haupt` für das ganze Haus und `wmz-whg-03` für eine Wohnung.

Die Daten sind **synthetisch**, aber plausibel: Oktober 2025 bis März 2026,
mit Tages- und Wochengang, Abhängigkeit von der Außentemperatur, Feiertagen,
einem Kälteeinbruch Mitte Januar, einigen Lücken und einem Ausreißer. Sie
liegen fertig in `daten/waerme.csv`, zusammen mit der Außentemperatur.

## 1. Container starten

```bash
docker compose up -d
```

Es starten drei Container:

| Dienst | Adresse | Zugang |
|---|---|---|
| TimescaleDB (PostgreSQL 17) | `localhost:5432`, Datenbank `energie` | Benutzer `postgres`, Passwort `geheim` |
| Grafana | `http://localhost:3000` | Benutzer `admin`, Passwort `admin` |
| Mosquitto (MQTT) | `localhost:1883` | ohne Anmeldung |

Die Zugangsdaten sind nur für den lokalen Betrieb gedacht. Beim ersten Start
legt TimescaleDB automatisch das Schema aus `sql/01_schema.sql` an:

- `messwerte`: Rohwerte der Zähler (Hypertable),
- `wetter`: Außentemperatur je Stunde (Hypertable),
- `prognosen`: Prognosen, die die Skripte zurückschreiben (Hypertable),
- `zaehler_stunde`: Continuous Aggregate mit dem letzten Zählerstand je Stunde,
- `verbrauch_stunde`: Sicht mit dem Verbrauch je Stunde in kW
  (Differenz zweier Zählerstände, Zeitstempel = Beginn der Stunde).

Datenbank und Grafana-Einstellungen liegen in Docker-Volumes und überleben
einen Neustart. Läuft auf Ihrem Rechner schon ein Dienst auf Port 5432, 3000
oder 1883 (z. B. der Broker aus `06/mqtt`), beenden Sie ihn vorher oder
ändern Sie den linken Port in der `docker-compose.yaml`.

Mit `psql` in der Datenbank arbeiten:

```bash
docker compose exec timescaledb psql -U postgres -d energie
```

## 2. Python-Umgebung anlegen

```bash
./create_environment.sh
```

Das Skript legt eine virtuelle Umgebung in `.venv` an und installiert die
Pakete aus `requirements.txt` (u. a. pandas, psycopg, statsmodels, scikit-learn, Prophet). Das
dauert beim ersten Mal einige Minuten.

In PyCharm können Sie den Ordner `zeitreihen` als Projekt öffnen und die
angebotene virtuelle Umgebung aus `requirements.txt` anlegen lassen.

## 3. Daten laden

```bash
./daten_erzeugen.sh db
```

lädt `daten/waerme.csv` in die Tabellen `messwerte` und `wetter` und
aktualisiert danach die Verdichtung `zaehler_stunde`.

Weitere Möglichkeiten:

```bash
./daten_erzeugen.sh csv              # Daten neu erzeugen (gleicher Zufalls-Seed, gleiche Daten)
./daten_erzeugen.sh uplinks 48 0.2   # 48 Zeilen als ChirpStack-Uplinks per MQTT senden
```

## 4. Uplinks empfangen und speichern

`importer.py` ist der MQTT-Subscriber aus Kapitel 8. Er abonniert
`application/+/device/+/event/up` und schreibt jeden Uplink in `messwerte`.
Schon vorhandene Werte werden dank `ON CONFLICT DO NOTHING` nicht doppelt
gespeichert.

In einem Terminal:

```bash
./importer.sh
```

In einem zweiten Terminal simulierte Uplinks senden:

```bash
./daten_erzeugen.sh uplinks 48 0.2
```

Mit einem echten ChirpStack-Server (Kapitel 8) tragen Sie dessen Broker über
die Umgebungsvariable `MQTT_HOST` ein, z. B. `MQTT_HOST=192.168.1.10 ./importer.sh`.
Ebenso lässt sich die Datenbank über `DB_URL` ändern.

## 5. Grafana

Unter `http://localhost:3000` ist die Datenquelle **TimescaleDB** schon
eingerichtet (`grafana/provisioning/datasources/timescaledb.yaml`). Im Ordner
*Energieinformatik* liegt das Dashboard **Wärmeverbrauch Mehrfamilienhaus**
mit Verbrauch, Verbrauch im Zeitraum, Rücklauftemperatur (Schwellwert 45 °C)
und Messung mit Prognosen. Der Zeitraum ist auf Januar 2026 eingestellt.
Oben wählen Sie über die Variablen *Gerät* und *Prognose* aus, welche Zähler
und welche Prognoseverfahren angezeigt werden. Die Prognosen erscheinen erst,
nachdem `./prognose.sh` gelaufen ist (Abschnitt 7).

Grafana erreicht die Datenbank unter dem Dienstnamen `timescaledb:5432`, nicht
unter `localhost`: `localhost` wäre im Container der Container selbst.

Eigene Dashboards und Änderungen speichert Grafana im Volume. Zum Weitergeben
exportieren Sie ein Dashboard als JSON (*Export* → *Export as JSON*).

## 6. Beispielabfragen

```bash
docker compose exec -T timescaledb psql -U postgres -d energie < abfragen/beispiele.sql
```

führt die Abfragen aus der Vorlesung aus: Verbrauch mit `LAG`, Tageswerte mit
`time_bucket`, Lücken mit `time_bucket_gapfill`, eine saisonal naive Prognose
direkt in SQL und ein Blick auf die Chunks der Hypertable. Unter Windows in der
PowerShell: `Get-Content abfragen\beispiele.sql | docker compose exec -T timescaledb psql -U postgres -d energie`.

## 7. Prognosen

```bash
./prognose.sh              # Prognosewoche ab 08.01.2026
./prognose.sh 2026-02-01   # anderer Stichtag
```

Das Skript lädt `verbrauch_stunde` und die Außentemperatur, trainiert bis zum
Stichtag und prognostiziert die folgenden 168 Stunden mit diesen Verfahren:

| Verfahren | Bibliothek |
|---|---|
| saisonal naiv (gleiche Stunde am Vortag) | pandas |
| Holt-Winters (exponentielles Glätten) | statsmodels |
| SARIMA und SARIMAX mit Außentemperatur | statsmodels |
| lineare Regression mit Außentemperatur | scikit-learn |
| Prophet, ohne und mit Außentemperatur | prophet |

Es gibt MAE und MAPE aus und schreibt die Prognosen in `prognosen`, sodass
Grafana sie neben der Messung zeigt.

Gemeinsame Funktionen (Datengenerator, Verbrauch berechnen, Fehlermaße,
Prognoseverfahren) stehen in `zeitreihe.py`. Dort lassen sich die Verfahren
auch einzeln ausprobieren, z. B. in einer Python-Konsole oder in PyCharm.

## 8. Übung

Für die Übung (Kapitel 13) gibt es ein Startskript, das den stündlichen
Verbrauch von `wmz-haupt` mit der Außentemperatur in den DataFrame `v` lädt.
Darunter schreiben Sie Ihre eigenen Auswertungen:

```bash
./uebung.sh
```

Ist die Datenbank nicht erreichbar, berechnet `uebung.py` dieselbe Reihe aus
`daten/waerme.csv` und sagt das in der Ausgabe.

Für den Empfang per Webhook (HTTP-Integration von ChirpStack) sendet der
Datengenerator dieselben Uplinks per HTTP `POST` an einen Empfänger:

```bash
./daten_erzeugen.sh webhook 48 0.2                                # an http://localhost:5050/uplink?event=up
./daten_erzeugen.sh webhook 48 0.2 http://localhost:8080/uplink   # andere Adresse
```

Musterlösungen liegen im Ordner `loesung` (siehe `loesung/README.md`).
Schauen Sie erst hinein, wenn Sie die Aufgaben selbst gelöst haben.

## 9. Beenden

```bash
docker compose down
```

Die Volumes bleiben dabei erhalten. Mit `docker compose down -v` werden sie
ebenfalls gelöscht; beim nächsten Start wird das Schema neu angelegt und Sie
laden die Daten erneut mit `./daten_erzeugen.sh db`.
