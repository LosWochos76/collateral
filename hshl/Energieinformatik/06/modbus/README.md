# Modbus-Smart-Meter

Dieses Paket enthält die technische Infrastruktur für die Modbus-Übung der
Veranstaltung Energieinformatik. Die Aufgabenstellung steht ausschließlich in
den Vorlesungsfolien zu Kapitel 07.

Ein simulierter Zweirichtungszähler liefert plausible Messwerte für Spannung,
Strom, Leistung, Energie, Frequenz und Leistungsfaktor. Dazu gibt es einen
unfertigen Lern-Client, eine Musterlösung, einen Rohtelegramm-Inspektor und
automatisierte Tests.

Für die Python-Clients ist Python 3.10 oder neuer erforderlich. Die Clients
verwenden nur die Standardbibliothek und benötigen keine Installation weiterer
Pakete.

## Smart Meter als Docker-Container starten

Die Studierenden starten den Modbus-Server als bereitgestellten Container:

```bash
docker pull stuckenholz/modbus-server:latest
docker run --rm --name modbus-meter -p 1502:1502 stuckenholz/modbus-server:latest
```

Der Zähler ist danach unter `127.0.0.1:1502` mit Unit-ID `1` erreichbar. Port
1502 wird bewusst anstelle des privilegierten Modbus-Ports 502 verwendet.

Das bereitgestellte Image verwendet das Profil `pv` und eine beschleunigte
Simulation. Zum Beenden genügt `Ctrl+C`.

## Client-Arbeitsvorlage

Vom Verzeichnis `modbus` aus:

```bash
python3 -m student.client
```

Die fachliche Beschreibung der Register steht in
[REGISTERPLAN.md](REGISTERPLAN.md). Die Aufgabenstellung und der vorgesehene
Umfang stehen in den Folien.

## Rohes Modbus-Telegramm untersuchen

```bash
python3 -m tools.raw_request --address 0 --count 13
```

Das Tool zeigt Anfrage und Antwort hexadezimal sowie die einzelnen Felder des
MBAP-Headers. Mit `--function 3` oder `--address 100` lassen sich
Exception-Responses auslösen.

## Musterlösung

Ein einzelner Snapshot:

```bash
python3 -m solution.client
```

Zehn Messungen im Abstand von zwei Sekunden mit CSV-Export:

```bash
python3 -m solution.client --interval 2 --count 10 --csv messwerte.csv
```

## Tests

```bash
python3 -m unittest discover -s tests -v
```

Die Integrationstests starten einen Zähler auf einem freien lokalen Port und
prüfen auch Modbus-Exception-Responses.

## Docker-Image bauen und veröffentlichen

Das Skript baut das Image und überträgt den angegebenen Tag nach Docker Hub:

```bash
./build_and_push.sh 1.0.0
```

Ohne Argument verwendet das Skript den Tag `latest`. Voraussetzung ist ein
erfolgreiches `docker login` für den Docker-Hub-Account `stuckenholz`.

## Struktur

```text
modbus_meter/          Protokoll, Clientbibliothek, Simulation und Server
student/client.py      Arbeitsvorlage mit TODOs
solution/client.py     lauffähige Musterlösung
tools/raw_request.py   Telegramme byteweise sichtbar machen
tests/                 Unit- und Integrationstests
```

Das Projekt implementiert bewusst nur den für die Übung benötigten Teil von
Modbus TCP. Modbus TCP bietet von sich aus weder Authentifizierung noch
Verschlüsselung. Der Container sollte deshalb nur in einer kontrollierten
Laborumgebung oder auf dem eigenen Rechner verwendet werden.
