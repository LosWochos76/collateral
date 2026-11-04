# Musterlösung zu Aufgabe 4: Programm containerisieren

Schauen Sie hier erst hinein, wenn Sie die Aufgabe **selbst** gelöst haben.

Alle Dateien liegen fertig im Ordner `stromanalyse`. Getestet mit Docker Desktop und dem Image `python:3.14-slim`.

## Dockerfile

```dockerfile
FROM python:3.14-slim

WORKDIR /app
COPY . .

RUN pip install \
    -r requirements.txt

CMD ["python", "analysis.py"]
```

Das ist das `Dockerfile` aus der Vorlesung, nur mit `analysis.py` statt `main.py`.

Empfohlen, aber nicht verlangt: eine Datei `.dockerignore` mit `.venv/` und `.git/`. Sonst kopiert `COPY . .` die lokale virtuelle Umgebung mit ins Image. Das macht das Image unnötig groß und hilft im Container nicht, weil dort Linux läuft.

## Bauen und starten

```bash
docker build -t stromanalyse .
docker run --name stromanalyse stromanalyse
```

Der Container gibt dieselbe Auswertung aus wie in Aufgabe 2 und beendet sich danach.

## Container prüfen und aufräumen

```bash
docker ps
docker ps -a
docker rm stromanalyse
docker image ls
docker image rm stromanalyse
```

| Befehl | Ergebnis |
|---|---|
| `docker ps` | leer: Der Container ist nach der Ausgabe beendet und läuft nicht mehr |
| `docker ps -a` | zeigt den Container mit Status `Exited (0)`, also ohne Fehler beendet |
| `docker rm stromanalyse` | löscht den beendeten Container |
| `docker image rm stromanalyse` | löscht das Image (optional, es kann für spätere Läufe bleiben) |

Ohne `--name` vergibt Docker einen Zufallsnamen. Dann im `docker rm` diesen Namen oder die Container-ID aus `docker ps -a` verwenden.

Anschließend `Dockerfile` (und ggf. `.dockerignore`) committen und pushen:

```bash
git add Dockerfile .dockerignore
git commit -m "Add Dockerfile"
git push
```

## Zusatzaufgabe: Flask-Webservice im Container

Ein zweites Dockerfile, hier `Dockerfile.web`:

```dockerfile
FROM python:3.14-slim

WORKDIR /app
COPY . .

RUN pip install \
    -r requirements.txt

EXPOSE 5000
CMD ["python", "app.py"]
```

```bash
docker build -f Dockerfile.web -t stromanalyse-web .
docker run --name stromweb -p 5000:5000 stromanalyse-web
```

Aufruf im Browser: `http://localhost:5000/average-price`. Beenden mit Strg+C, dann `docker rm stromweb`.

Zwei Punkte entscheiden darüber, ob der Webservice von außen erreichbar ist:

- **Port-Freigabe:** `-p 5000:5000` verbindet Port 5000 des Rechners mit Port 5000 im Container (Schema `Host:Container` wie bei `-p 8080:80` in der Vorlesung).
- **`host="0.0.0.0"` in `app.run`:** Flask lauscht ohne diese Angabe nur auf `127.0.0.1`, also nur innerhalb des Containers. Dann antwortet der Browser trotz Port-Freigabe mit einem Verbindungsfehler. Wer das Vorlesungsbeispiel übernommen hat, hat den Parameter bereits.

Statt eines zweiten Dockerfiles ist es ebenso richtig, im vorhandenen `Dockerfile` das `CMD` auf `app.py` umzustellen.
