# Musterlösung zu Aufgabe 3: Ergebnis versionieren

Schauen Sie hier erst hinein, wenn Sie die Aufgabe **selbst** gelöst haben.

## Repository auf GitHub anlegen

Auf GitHub über **New repository** ein Repository `stromanalyse` anlegen, **ohne** README, `.gitignore` oder Lizenz. Dann ist es wirklich leer, und der erste Push klappt ohne Konflikt.

## Virtuelle Umgebung ausschließen: .gitignore

Im Verzeichnis `stromanalyse` eine Datei `.gitignore` anlegen:

```text
.venv/
.idea/
```

Git ignoriert alle Dateien und Verzeichnisse, die hier stehen. `.idea/` enthält die persönlichen Einstellungen von PyCharm und gehört ebenfalls nicht ins Repository. Die Datei `.gitignore` selbst wird mitversioniert.

## Lokales Verzeichnis verbinden und ersten Commit übertragen

**Variante A: lokales Verzeichnis zum Repository machen**, wie auf der Folie „Eigenes Projekt zu GitHub übertragen“:

```bash
git init -b main
git add .
git commit -m "Add price analysis"
git remote add origin https://github.com/<benutzername>/stromanalyse.git
git push -u origin main
```

**Variante B: mit `git clone`.** Das leere Repository mit `git clone` holen, die Dateien in das neue Verzeichnis kopieren, dann `git add .`, `git commit` und `git push`.

Vor dem `git add` mit `git status` prüfen: `.venv` darf in der Liste **nicht** auftauchen.

Typische Stolpersteine:

- Beim ersten Commit fragt Git nach Name und E-Mail. Einmalig setzen mit `git config --global user.name "Anna Muster"` und `git config --global user.email "anna@example.org"`.
- GitHub akzeptiert beim Push kein Passwort. Unter Windows öffnet Git ein Anmeldefenster im Browser. Sonst braucht man ein *Personal Access Token* oder die Anmeldung über PyCharm.
- Wurde `.venv` schon committet, reicht eine nachträgliche `.gitignore` nicht. Dann zusätzlich `git rm -r --cached .venv` ausführen und neu committen.

## Zweiter Commit

Grenzwert in `analysis.py` ändern, z. B. `limit=80` zu `limit=90`, dann:

```bash
git add analysis.py
git commit -m "Raise charging limit to 90 EUR/MWh"
git push
```

## Kontrollfrage

Auf der Startseite des Repositorys steht über der Dateiliste die Anzahl der Commits, z. B. **2 Commits**. Ein Klick darauf zeigt die Historie mit beiden Commit-Nachrichten. Die Datei `analysis.py` zeigt in ihrer Zeile die Nachricht des letzten Commits, der sie geändert hat. Lokal zeigt `git log --oneline` dasselbe.
