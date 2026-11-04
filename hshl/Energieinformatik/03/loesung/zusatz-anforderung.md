# Musterlösung zur Zusatzaufgabe: Anforderung für einen KI-Coding-Agenten

Schauen Sie hier erst hinein, wenn Sie die Aufgabe **selbst** gelöst haben.

Eine mögliche Anforderung nach dem Schema der Vorlesung (Aufgabe, Randbedingungen, erwartete Ergebnisse):

```markdown
# Aufgabe
Erweitere das Programm `analysis.py` so, dass es zusätzlich den niedrigsten
und den höchsten Preis aus der Spalte `price` der Datei `preise.csv` ausgibt,
jeweils zusammen mit der Stunde aus der Spalte `hour`.

## Randbedingungen
- Programmiersprache: Python, Datenauswertung mit pandas
- Bestehende Ausgaben (Mittelwert, Ladeempfehlung je Stunde) bleiben unverändert.
- Keine neuen Abhängigkeiten in `requirements.txt`.
- Einheit in der Ausgabe: EUR/MWh

## Erwartete Ergebnisse
- Geänderte Datei `analysis.py`
- Ausgabe z. B.: `Minimum: 65.0 EUR/MWh in Stunde 3` und `Maximum: 124.7 EUR/MWh in Stunde 8`
- Das Dockerfile funktioniert weiterhin unverändert.
```

Worauf es ankommt:

- Die Anforderung ist **eindeutig**: Datei, Spalten, Ausgabeformat und Einheit sind benannt.
- Sie sagt, was sich **nicht** ändern darf.
- Das Ergebnis ist **prüfbar**: Mit den Beispieldaten muss Minimum 65.0 (Stunde 3) und Maximum 124.7 (Stunde 8) herauskommen.

Danach das Ergebnis des Agenten prüfen: Programm laufen lassen, Ausgabe mit den Daten vergleichen, Änderung mit `git diff` ansehen und erst dann committen.
