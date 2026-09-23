# Modbus-Wetterstation

Dieses Paket enthält die technische Infrastruktur für die Modbus-Übung der
Veranstaltung Energieinformatik. Die Aufgabenstellung steht ausschließlich in
den Vorlesungsfolien zu Kapitel 07.

Simuliert wird das **echte Datenmodell der Wetterstation auf dem Dach der
HSHL**: Strahlungswerte (GHI/DHI/DNI), Lufttemperatur, Luftfeuchte, Luftdruck,
Wind, Niederschlag sowie Logger-Diagnosedaten.

Der Client nutzt bewusst nur `pymodbus` (Standardpaket für Modbus in Python,
siehe Kapitel 06) für die TCP-Verbindung und `struct` aus der
Python-Standardbibliothek für die Interpretation der Werte -- keine eigene,
vorgefertigte Abstraktionsschicht. Das Protokoll-Detailwissen (MBAP-Header,
PDU) ist damit weder nötig noch Teil der Übung; im Vordergrund steht, Werte
korrekt aus dem in REGISTERPLAN.md beschriebenen Registerplan zu extrahieren.

## Wetterstation lokal starten

```bash
docker compose up -d
```

Die Station ist danach unter `127.0.0.1:1502` mit Unit-ID `1` erreichbar. Port
1502 wird bewusst anstelle des privilegierten Modbus-Ports 502 verwendet.
Alle Messwerte liegen in Holding Registers und werden mit Funktionscode `0x03`
gelesen.

Die Simulation läuft beschleunigt (ein simulierter Tag dauert real nur wenige
Minuten), damit sich auch der Tag-Nacht-Verlauf der Strahlungswerte im
Unterricht beobachten lässt. Zum Beenden: `docker compose down`.

## Client-Beispiel

```bash
./create_environment.sh
./client.sh
```

`client.py` zeigt vollständig und lauffähig, wie ein einzelner Messwert
(Lufttemperatur) über `pymodbus` gelesen und mit `struct` interpretiert wird.
Alle weiteren Messwerte aus [REGISTERPLAN.md](REGISTERPLAN.md) selbst zu lesen
und zu interpretieren ist Teil der Übung; die Aufgabenstellung steht in den
Folien.

## Docker-Image bauen und veröffentlichen

Das Skript baut das Image (Kontext: `image/`, dort liegt auch die
Server-Implementierung `weather_station/`) und überträgt den angegebenen Tag
nach Docker Hub:

```bash
cd image
./build_and_push.sh 1.0.0
```

Ohne Argument verwendet das Skript den Tag `latest`. Voraussetzung ist ein
erfolgreiches `docker login` für den Docker-Hub-Account `stuckenholz`.

## Struktur

```text
client.py                        Beispiel: liest einen einzelnen Messwert (pymodbus + struct)
requirements.txt                 pymodbus
create_environment.sh            legt .venv an und installiert requirements.txt
client.sh                        startet client.py in .venv
compose.yaml                     startet den Server aus stuckenholz/modbus-server
image/Dockerfile                 baut das Server-Image
image/build_and_push.sh          baut und veröffentlicht stuckenholz/modbus-server
image/weather_station/           Protokoll, Simulation und Server -- nur im Image, kein Client-Code
```

Modbus TCP bietet von sich aus weder Authentifizierung noch Verschlüsselung.
Der Container sollte deshalb nur in einer kontrollierten Laborumgebung oder
auf dem eigenen Rechner verwendet werden.
