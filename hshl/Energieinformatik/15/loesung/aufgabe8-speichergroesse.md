# Folie 8: Die Speichergröße mit oemof.solph

| Befehl | Zielfunktion | Optimale Speichergröße |
|---|---|---|
| `./heizzentrale_oemof.sh` | 594,13 Euro (gleich wie Pyomo) | – (vorhandene 175 kWh) |
| `./heizzentrale_oemof.sh --investition` | 597,72 Euro | 41 kWh |
| `./heizzentrale_oemof.sh --investition --heizperiode` | 8029,27 Euro | 63 kWh |

- oemof.solph erzeugt ein Pyomo-Modell und kommt auf denselben Cent wie das Pyomo-Modell von Hand.
- In der Testwoche lohnt nur ein kleiner Speicher: Bei Kälte läuft die Wärmepumpe ohnehin an ihrer Grenze, der Speicher kann wenig verschieben.
- Über die Heizperiode (Oktober bis März) gibt es mehr Stunden, in denen sich Verschieben lohnt, deshalb ein größerer Speicher. Beide liegen deutlich unter den vorhandenen 175 kWh.
- Zur Diskussion: Das Modell kennt Preise und Wetter des ganzen Zeitraums im Voraus (vollkommene Voraussicht), rechnet mit einer anteiligen Annuität und nur einem Winter. Für eine Kaufentscheidung braucht man mehrere Jahre, Unsicherheit und Angebote mit echten Preisen.
