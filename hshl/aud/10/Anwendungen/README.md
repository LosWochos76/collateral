# Anwendungen von Dictionary und HashSet

Begleitprojekt zum Abschnitt „Wörterbücher und Mengen anwenden“ in Veranstaltung 10.

| Datei | Inhalt |
|---|---|
| `Textanalyse.cs` | Verschiedene Wörter zählen (Menge), Worthäufigkeiten (Zählen), Anagramme (Gruppieren) |
| `Muster.cs` | Duplikate finden, Kunden nachschlagen (Join), Two-Sum |
| `Fallstricke.cs` | Eigene Klassen als Schlüssel, veränderliche Schlüssel, `KeyNotFoundException` |
| `Messung.cs` | Laufzeitvergleich `List` gegen `HashSet` bzw. `Dictionary` |

## Ausführen

Im Verzeichnis `Anwendungen`:

```
dotnet run -c Release
```

Ohne Argument wird `beispiel.txt` analysiert. Interessanter wird es mit einem ganzen Buch, z. B. einem gemeinfreien Text von [Projekt Gutenberg](https://www.gutenberg.org/) als `.txt`-Datei:

```
dotnet run -c Release -- pfad/zum/buch.txt
```

Die Laufzeitmessungen am Ende arbeiten mit Zufallsdaten. Die Größen lassen sich über die Parameter der Methoden in `Messung.cs` verändern.
