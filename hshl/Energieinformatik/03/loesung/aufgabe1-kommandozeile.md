# Musterlösung zu Aufgabe 1: Projektverzeichnis anlegen

Schauen Sie hier erst hinein, wenn Sie die Aufgabe **selbst** gelöst haben.

## Vorbereitung prüfen

```bash
python --version
git --version
```

Unter macOS heißt der Befehl oft `python3`. Wenn ein Befehl „nicht gefunden“ wird, ist das Programm nicht installiert oder nicht im Suchpfad. Bei Windows hilft oft ein Neustart des Terminals nach der Installation.

## Verzeichnis und Datei anlegen

**PowerShell (Windows):**

```powershell
mkdir stromanalyse
cd stromanalyse
@"
hour,price
0,78.4
1,72.1
2,68.9
3,65.0
4,66.3
5,74.8
6,92.5
7,118.2
8,124.7
9,105.3
"@ | Set-Content preise.csv
```

**Terminal (macOS/Linux):**

```bash
mkdir stromanalyse
cd stromanalyse
cat > preise.csv <<'END'
hour,price
0,78.4
1,72.1
2,68.9
3,65.0
4,66.3
5,74.8
6,92.5
7,118.2
8,124.7
9,105.3
END
```

Ebenso richtig: die Datei mit einem Editor aus der Konsole heraus anlegen, z. B. `notepad preise.csv` (Windows) oder `nano preise.csv` (macOS/Linux).

Die Beispieldaten liegen auch fertig unter `stromanalyse/preise.csv`.

Wichtig für Aufgabe 2:

- Die erste Zeile enthält die Spaltennamen `hour` und `price`, genau so geschrieben.
- Dezimaltrennzeichen ist der **Punkt**, Feldtrenner das **Komma**. Ein Dezimalkomma würde mit dem Feldtrenner kollidieren.

## Prüfen

| Zweck | PowerShell | macOS/Linux |
|---|---|---|
| Existiert die Datei? | `dir` | `ls -l` |
| Inhalt anzeigen | `Get-Content preise.csv` (oder `cat preise.csv`) | `cat preise.csv` |

## Kontrollfrage

`pwd` bzw. `Get-Location` liefert den **absoluten Pfad** des aktuellen Verzeichnisses, z. B.:

- Windows: `C:\Users\Anna\energieinformatik\stromanalyse`
- macOS: `/Users/anna/energieinformatik/stromanalyse`

Der Pfad hängt davon ab, wo Sie das Verzeichnis angelegt haben. Entscheidend ist, dass er mit `stromanalyse` endet. Andernfalls haben Sie `cd stromanalyse` vergessen.
