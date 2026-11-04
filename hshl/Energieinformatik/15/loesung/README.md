# Musterlösungen zur Übung Optimierung (Kapitel 15)

**Sehen Sie sich diese Lösungen erst an, wenn Sie die Aufgaben selbst gelöst haben.** Ein Modell versteht man erst, wenn man es selbst aufgeschrieben, gerechnet und gedeutet hat.

Die Python-Lösungen nutzen die Umgebung aus `14/optimierung` (einmal `./create_environment.sh` bzw. `create_environment.bat` dort ausführen). Start im Ordner `15/loesung`:

```bash
../../14/optimierung/.venv/bin/python heizzentrale_starts.py
../../14/optimierung/.venv/bin/python haushalt_loesung.py --binaer
```

Unter Windows:

```bat
..\..\14\optimierung\.venv\Scripts\python.exe heizzentrale_starts.py
..\..\14\optimierung\.venv\Scripts\python.exe haushalt_loesung.py --binaer
```

| Datei | Aufgabe |
|---|---|
| `aufgabe3-kraftwerke.md` | Folie 3: Das Kraftwerksmodell befragen |
| `aufgabe4-pumpspeicher.md` | Folie 4 und 5: Den Pumpspeicher variieren, ein Wintertag |
| `aufgabe6-heizzentrale.md` | Folie 6: Die Heizzentrale rechnen |
| `aufgabe7-kesselstarts.md` | Folie 7: Kesselstarts ergänzen |
| `aufgabe8-speichergroesse.md` | Folie 8: Die Speichergröße mit oemof.solph |
| `aufgabe10-modell.md` | Folie 10: Das Haushaltsmodell auf Papier |
| `aufgabe11-14-haushalt.md` | Folie 11 bis 14: Das Haushaltsmodell in Pyomo, Ergebnis, Mindestladeleistung, Heimspeicher |
| `heizzentrale_starts.py` | lauffähige Lösung zu Folie 7, Option `--startkosten EURO` |
| `haushalt_loesung.py` | lauffähige Lösung zu Folie 11 bis 14, Optionen `--binaer`, `--mindestladung`, `--heimspeicher`, `--grafik` |

Die Zahlen stammen aus Läufen vom 04.11.2026 mit HiGHS. Andere Solver-Versionen können bei gleichen Kosten andere, gleich gute Fahrpläne liefern.
