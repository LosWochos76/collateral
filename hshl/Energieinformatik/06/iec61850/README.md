# IEC 61850/MMS: Windstatus und Schalter

Dieses Paket stellt zwei offizielle libIEC61850-Beispielserver lokal per Docker bereit und liest bzw. steuert sie über echtes MMS (TCP) mit einem Python-Client.

- **wind**: simuliert eine Windkraftanlage nach IEC 61400-25 (`WINDWTG/WTUR1`) -- nur lesbar.
- **switch**: ein generisches, steuerbares Schalterobjekt (`simpleIOGenericIO/GGIO1.SPCSO1`) -- zeigt einen echten MMS-Control-Vorgang (`Operate`).

`wind` und `switch` nutzen dasselbe vorgefertigte Image `stuckenholz/iec61850` von Docker Hub, nur mit unterschiedlichem Startkommando, und laufen dauerhaft im Hintergrund. Ein lokaler Build ist für Studierende nicht nötig.

## Schnellstart

```sh
cd ~/collateral/hshl/Energieinformatik/06/iec61850
./create_environment.sh
./client.sh wind
./client.sh switch on
./client.sh switch off
docker compose down
```

`create_environment.sh` zieht das Image von Docker Hub. `client.sh` startet `wind` und `switch` beim ersten Aufruf automatisch (falls noch nicht geschehen) und führt `client.py` danach in einem eigenen, kurzlebigen Container aus (`docker run --rm`, im selben Docker-Netz `iec61850-net`) -- ein Python-Interpreter auf dem eigenen Rechner wird nicht benötigt, da `client.py` die C-Bindings `pyiec61850` voraussetzt, die nur im Image vorhanden sind. Es läuft also kein dritter, dauerhaft leerlaufender Container mit -- nur die beiden echten MMS-Server.

## Was macht der Client?

`TurSt.actSt.stVal` ist ein simulierter Zustandswert der Turbine mit Functional Constraint `ST` (Read). `SPCSO1` ist ein steuerbares Objekt: `Operate` läuft als IEC-61850-Control über MMS, danach wird `SPCSO1.stVal` erneut gelesen und zeigt die Wirkung des Befehls.

Der Windserver wird nur an `127.0.0.1:8102` veröffentlicht; der Schalterserver ist nur im Docker-Netz erreichbar. Steuerbefehle gehören ausschließlich in diese lokale Übungsumgebung -- MMS bietet von sich aus weder Authentifizierung noch Verschlüsselung.

### Warum steuert der Client nicht direkt die Windkraftanlage?

Das Datenmodell der Turbine bringt mit `WTUR1.SetTurOp` bereits ein eigenes, im Server-Code verdrahtetes Steuerobjekt mit. In der Praxis lehnt der offizielle Beispielserver `Operate`-Aufrufe darauf jedoch mit `access-denied` ab -- reproduzierbar auch mit dem unveränderten Original-Beispiel, vermutlich eine Einschränkung von libIEC61850 bei der dort verschachtelten DO/SDO-Struktur (`SetTurOp.actSt`, IEC-61400-25-typisch) statt eines direkt am DO liegenden Steuerobjekts wie bei `SPCSO1`. Für die Übung wird deshalb weiterhin der generische, nachweislich funktionierende Schalter-Server für die MMS-Control-Demonstration genutzt.

## Für Betreuende: Image pflegen

Das Image wird aus `image/Dockerfile` gebaut, das aus einem festgelegten libIEC61850-Quellstand die zwei Beispielserver und die Python-Bindings kompiliert:

```sh
cd image
./build_and_push.sh          # Tag "latest"
./build_and_push.sh 1.0.0    # oder ein expliziter Tag
```

Voraussetzung ist ein erfolgreiches `docker login` für den Docker-Hub-Account `stuckenholz`.

## Struktur

```text
compose.yaml            wind/switch (Dauerbetrieb) auf stuckenholz/iec61850, Netz iec61850-net
client.py                MMS-Client für beide Beispielserver
create_environment.sh    zieht das Image von Docker Hub
client.sh                startet wind/switch bei Bedarf, ruft client.py in einem kurzlebigen Container auf
image/Dockerfile          baut libIEC61850 samt Python-Bindings aus dem Quellstand
image/build_and_push.sh   baut und veröffentlicht stuckenholz/iec61850
```

Quelle: [libIEC61850](https://github.com/mz-automation/libiec61850), Commit `96d69e9c9f7ee270ace1838bb2a5a202a67c78d2`.
