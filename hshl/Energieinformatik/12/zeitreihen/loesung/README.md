# Musterlösungen zur Übung

Schauen Sie hier erst hinein, wenn Sie die Aufgaben **selbst** gelöst haben.

| Datei | Inhalt |
|---|---|
| `abfragen.sql` | SQL-Aufgaben: eigene Abfragen, Lücken, Fehler der Messlatte, saisonal naiv mit dem Vortag |
| `uebung.py` | Python-Aufgaben: Glätten, Prophet ohne und mit Außentemperatur, saisonal naiv selbst geschrieben |
| `webhook.py` | Empfänger für Uplinks per Webhook mit Flask auf Port 5050 |

Starten Sie die Lösungen im Ordner `zeitreihen`, nicht in `loesung`:

```bash
docker compose exec -T timescaledb psql -U postgres -d energie < loesung/abfragen.sql
.venv/bin/python loesung/uebung.py
.venv/bin/python loesung/webhook.py
```

Unter Windows heißt der Python-Aufruf `.venv\Scripts\python.exe loesung\uebung.py`,
die SQL-Datei übergeben Sie in der PowerShell mit
`Get-Content loesung\abfragen.sql | docker compose exec -T timescaledb psql -U postgres -d energie`.
