# Optimierung mit Pyomo und oemof.solph

Dieses Paket enthält die Skripte für die Praxisfolien der Veranstaltung
Energieinformatik, Kapitel 14. Die Optimierungsmodelle sind mit
[Pyomo](https://www.pyomo.org/) und [oemof.solph](https://oemof-solph.readthedocs.io/)
formuliert und werden mit dem freien Solver [HiGHS](https://highs.dev/) gelöst.
Alles läuft in Python, Docker wird nicht gebraucht.

**Windows:** Zu jedem Skript `name.sh` gibt es ein gleichnamiges `name.bat`. Rufen Sie in der Eingabeaufforderung `name.bat` bzw. in der PowerShell `.\name.bat` auf, z. B. `.\create_environment.bat` statt `./create_environment.sh`.

## Die Beispiele

| Skript | Beispiel | Modell |
|---|---|---|
| `kraftwerke.py` | Zwei Kraftwerke liefern 300 MWh bei höchstens 230 t CO₂ | LP mit zwei Variablen, Dualwerte |
| `pumpspeicher.py` | Pumpspeicherkraftwerk Herdecke am 1. Mai 2026 | LP über 24 Stunden, mit `--milp` Binärvariable |
| `kraftwerkseinsatz.py` | Drei Kraftwerke decken die Last eines Tages | MILP (Unit Commitment) |
| `heizzentrale.py` | Wärmepumpe, Gaskessel und Speicher des Mehrfamilienhauses | LP über eine Woche, Erweiterungen |
| `heizzentrale_oemof.py` | dieselbe Heizzentrale mit oemof.solph | Graph aus Bussen und Flows, Investition |

Die Heizzentrale setzt das Beispiel aus Kapitel 12 fort: Eingabe ist die
Wärmebedarfsprognose der Testwoche vom 8. bis 14. Januar 2026.

## 1. Python-Umgebung anlegen

```bash
./create_environment.sh
```

Das Skript legt eine virtuelle Umgebung in `.venv` an und installiert die
Pakete aus `requirements.txt`: Pyomo, oemof.solph, pandas, matplotlib und
`highspy`. `highspy` bringt den Solver HiGHS mit, er muss nicht extra
installiert werden.

In PyCharm können Sie den Ordner `optimierung` als Projekt öffnen und die
angebotene virtuelle Umgebung aus `requirements.txt` anlegen lassen.

## 2. Die Lehrbeispiele rechnen

```bash
./kraftwerke.sh
./pumpspeicher.sh
./pumpspeicher.sh --milp
./kraftwerkseinsatz.sh
./kraftwerkseinsatz.sh --rampen
```

Jedes Skript gibt den Status des Solvers, den Wert der Zielfunktion und die
Ergebnisse aus. Fahrpläne werden zusätzlich als CSV in `ergebnisse/`
gespeichert.

## 3. Die Heizzentrale optimieren

```bash
./heizzentrale.sh                  # optimaler Fahrplan mit der Prognose
./heizzentrale.sh --referenz       # einfache Regelung: Wärmepumpe zuerst, kein Speicher
./heizzentrale.sh --milp           # Kessel mit Mindestlast 30 %
./heizzentrale.sh --lastspitze     # Leistungspreis für die höchste Strombezugsleistung
./heizzentrale.sh --nachrechnen    # Plan mit dem gemessenen Bedarf bewerten
./heizzentrale.sh --nachrechnen --prognose naiv   # dasselbe mit der saisonal naiven Prognose
```

Die Annahmen (Leistungen, Wirkungsgrade, Preise) stehen oben in
`optimierung.py` und lassen sich dort ändern. Der Strompreis ist der
Day-Ahead-Preis plus ein fester Aufschlag von 20 ct/kWh für Netzentgelt,
Abgaben und Steuern.

## 4. Die Heizzentrale mit oemof.solph

```bash
./heizzentrale_oemof.sh                          # dieselben Kosten wie mit Pyomo
./heizzentrale_oemof.sh --lp-datei               # erzeugtes Modell als ergebnisse/heizzentrale.lp
./heizzentrale_oemof.sh --investition            # Speichergröße für die Testwoche
./heizzentrale_oemof.sh --investition --heizperiode   # Speichergröße für Oktober bis März
```

## 5. Daten

Alle Daten liegen in `daten/`:

| Datei | Inhalt |
|---|---|
| `preise_testwoche.csv`, `preise_testwoche_15min.csv` | Day-Ahead-Preise DE-LU, 8. bis 14. Januar 2026, Stundenmittel bzw. 15 Minuten |
| `preise_2026-05-01.csv`, `preise_2026-05-01_15min.csv` | Day-Ahead-Preise DE-LU am 1. Mai 2026 (Ortszeit), mit stark negativen Preisen |
| `preise_heizperiode.csv` | Stundenmittel der Day-Ahead-Preise DE-LU, Oktober 2025 bis März 2026 |
| `waermebedarf.csv` | Testwoche aus Kapitel 12: Prognose (lineare Regression mit Außentemperatur), saisonal naive Prognose, Messung, Außentemperatur |
| `waermebedarf_heizperiode.csv` | gemessener Wärmebedarf und Außentemperatur der ganzen Heizperiode |

Alle Zeiten sind in UTC. Die Preise stammen von
[Energy-Charts](https://energy-charts.info) (Fraunhofer ISE), Datenquelle
Bundesnetzagentur | SMARD.de, Lizenz CC BY 4.0. Der Wärmebedarf ist
synthetisch und wurde mit `zeitreihe.py` aus `12/zeitreihen` erzeugt
(Funktionen `regression` und `saisonal_naiv`).

Weitere Preise laden Sie mit

```bash
./preise_laden.sh --von 2026-06-01 --bis 2026-06-08 --stuendlich --datei preise_juni.csv
```

Das Skript fragt die Energy-Charts-API ab und braucht deshalb eine
Internetverbindung.
