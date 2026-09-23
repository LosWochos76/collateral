# MQTT lokal ausprobieren

Dieses Paket enthält die technische Infrastruktur für die MQTT-Praxisfolie
der Veranstaltung Energieinformatik, Kapitel 06. Ein lokaler Mosquitto-Broker
läuft als Docker-Container, dazu gibt es einen Publisher und einen
Subscriber in Python.

Der Publisher sendet eine simulierte PV-Leistung als JSON an das Topic
`anlage/pv1/leistung`. Der Subscriber abonniert `anlage/#` und zeigt Topic,
Zeitstempel und Wert jeder empfangenen Nachricht an.

Für die Python-Skripte ist Python 3.10 oder neuer erforderlich.

## 1. Broker starten

```bash
docker compose up
```

Der Broker ist danach unter `127.0.0.1:1883` (MQTT) sowie `127.0.0.1:9001`
(MQTT über WebSockets) erreichbar. Konfiguration, Daten und Logs liegen in
`config/`, `data/` und `logs/` und werden per Bind-Mount in den Container
eingebunden. Zum Beenden genügt `Ctrl+C` oder `docker compose down`.

## 2. Python-Umgebung einrichten

### Weg 1: Terminal

```bash
./create_environment.sh
```

Das Skript legt eine virtuelle Umgebung in `.venv` an und installiert die in
`requirements.txt` gelistete Abhängigkeit (`paho-mqtt`).

### Weg 2: PyCharm

1. Ordner `mqtt` als Projekt in PyCharm öffnen.
2. Falls PyCharm beim Öffnen anbietet, anhand von `requirements.txt` eine
   virtuelle Umgebung einzurichten: Angebot annehmen. PyCharm legt dann
   automatisch eine `.venv` an und installiert `paho-mqtt`.
3. Andernfalls manuell: *Settings/Preferences* -> *Project* ->
   *Python Interpreter* -> *Add Interpreter* -> *Add Local Interpreter* ->
   *Virtualenv Environment*. Entweder eine neue Umgebung im Projektordner
   anlegen lassen (Basisinterpreter: Python 3.10+) oder die bereits per
   `create_environment.sh` erzeugte `.venv` auswählen.
4. Wurde die Umgebung neu angelegt, anschließend `paho-mqtt` installieren:
   Terminal in PyCharm öffnen und `pip install -r requirements.txt`
   ausführen, oder die gelbe Hinweisleiste über `requirements.txt` nutzen.

## 3. Subscriber und Publisher starten

Zuerst den Subscriber, danach den Publisher starten, damit die Nachricht
sicher ankommt.

### Weg 1: Terminal

In zwei separaten Terminalfenstern, jeweils im Verzeichnis `mqtt`:

```bash
./run_subscriber.sh
```

```bash
./run_publisher.sh
```

Beide Skripte nutzen automatisch den Python-Interpreter aus `.venv` -- ein
vorheriges Aktivieren der Umgebung ist nicht nötig.

### Weg 2: PyCharm

1. Sicherstellen, dass der Projekt-Interpreter auf die `.venv` aus Schritt 2
   zeigt (unten rechts im PyCharm-Fenster sichtbar).
2. `subscriber.py` im Projektbaum öffnen und mit Rechtsklick ->
   *Run 'subscriber'* starten.
3. Anschließend `publisher.py` öffnen und ebenfalls mit Rechtsklick ->
   *Run 'publisher'* starten.
4. Die empfangene Nachricht erscheint im Run-Fenster des Subscribers.

## Untersuchen und variieren

Das Topic im Publisher (`anlage/pv1/leistung`) lässt sich ändern, um das
Verhalten von Wildcard-Subscriptions wie `anlage/#` zu beobachten. Mit
mehreren gleichzeitig laufenden Subscribern oder einer anderen QoS-Stufe im
Publisher lassen sich die in der Vorlesung besprochenen Zustellgarantien
nachvollziehen.

## Struktur

```text
docker-compose.yml     Mosquitto-Broker als Container
config/mosquitto.conf  Broker-Konfiguration (anonymer Zugriff, nur für Lehrzwecke)
subscriber.py          Empfängt und zeigt PV-Messwerte an
publisher.py           Sendet eine simulierte PV-Leistung
requirements.txt       Python-Abhängigkeiten
create_environment.sh  Legt .venv an und installiert requirements.txt
run_subscriber.sh      Startet subscriber.py in .venv
run_publisher.sh       Startet publisher.py in .venv
```

Der Broker erlaubt anonymen Zugriff ohne Authentifizierung oder
Verschlüsselung (`allow_anonymous true`). Das ist für die lokale Übung
gewollt, aber ausdrücklich nicht für den produktiven Einsatz geeignet.
