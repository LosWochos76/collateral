# Einfaches Energiesystemmodell mit oemof.solph

Diese kleine Python-App erzeugt ein fiktives, aber plausibles deutsches Stromsystem fuer ein ganzes Jahr mit stuendlicher Aufloesung. Sie simuliert den Kraftwerkseinsatz nach Merit Order mit oemof.solph und HiGHS. Das Unit-Commitment wird ueber thermische Ein/Aus-Entscheidungen und Mindestlasten modelliert.

## Enthaltene Komponenten

- synthetische Stromnachfrage fuer Deutschland
- feste Einspeiseprofile fuer PV und Wind
- regelbare Kraftwerke fuer Braunkohle, Steinkohle und Gas
- optionaler Import/Lastabwurf mit sehr hohen Kosten als Sicherheitsventil
- Ergebnisexport fuer Dispatch, CO2-Emissionen und Gesamtkosten

## Installation

Falls oemof.solph und HiGHS in deiner aktiven Python-Umgebung noch nicht verfuegbar sind:

```bash
python3 -m pip install -r requirements.txt
```

## Ausfuehren

```bash
python3 app.py
```

## PyCharm

Das Projekt enthaelt PyCharm-Projektdateien im Ordner `.idea/` und eine Run Configuration `Energiesystemmodell`.

Beim Oeffnen des Ordners in PyCharm sollte die lokale virtuelle Umgebung automatisch verwendet werden:

```text
.venv/bin/python
```

Die mitgelieferte Umgebung wurde mit Python 3.12 erstellt.

Falls PyCharm den Interpreter nicht automatisch setzt:

1. Settings / Preferences -> Project -> Python Interpreter
2. Add Interpreter -> Existing
3. `.venv/bin/python` auswaehlen
4. Run Configuration `Energiesystemmodell` starten

Die Ergebnisse landen standardmaessig im Ordner `results/`:

- `timeseries.csv`: Last, erneuerbare Einspeisung und thermischer Dispatch
- `summary.csv`: Kosten, CO2 und Energiemengen je Kraftwerk
- `dispatch_plot.svg`: Wochenansicht des Kraftwerkseinsatzes als SVG
- `dispatch_plot.pdf`: Wochenansicht des Kraftwerkseinsatzes als PDF

Nuetzliche Optionen:

```bash
python3 app.py --year 2026 --solver highs --output results
python3 app.py --plot-week 3
python3 app.py --chunk-days 0
```

Standardmaessig loest die App den Jahreslauf in 31-Tage-Bloecken. Das ist fuer lokale HiGHS-Installationen robuster als ein einziges grosses Jahres-MILP. Mit `--chunk-days 0` kannst du das ganze Jahr in einem Modell loesen, falls dein Setup genug stabil ist.
