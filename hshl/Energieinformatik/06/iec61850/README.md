# IEC 61850/MMS: Windstatus und Schalter

Die Container bauen zwei offizielle libIEC61850-Beispielserver und die Python-Bindings aus einem festgelegten Quellstand. Beide Verbindungen nutzen echtes MMS über TCP.

```sh
cd ~/collateral/hshl/Energieinformatik/06/iec61850
docker compose up -d --build wind switch client
docker compose exec client python client.py wind
docker compose exec client python client.py switch on
docker compose exec client python client.py switch off
docker compose down
```

Der Windserver verwendet das IEC-61400-25-Modell `WINDWTG/WTUR1`; `TurSt.actSt.stVal` ist ein simulierter Zustandswert mit Functional Constraint `ST`. Der zweite offizielle Beispielserver stellt `simpleIOGenericIO/GGIO1.SPCSO1` als steuerbares Schalterobjekt bereit. `Operate` läuft als IEC-61850-Control über MMS; danach wird `SPCSO1.stVal` erneut gelesen.

Der Windserver wird nur an `127.0.0.1:8102` veröffentlicht; der Schalterserver ist nur im Docker-Netz erreichbar. Steuerbefehle gehören ausschließlich in diese lokale Übungsumgebung.

Quelle: [libIEC61850](https://github.com/mz-automation/libiec61850), Commit `96d69e9c9f7ee270ace1838bb2a5a202a67c78d2`.
