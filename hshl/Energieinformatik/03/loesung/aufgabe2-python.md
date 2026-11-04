# Musterlösung zu Aufgabe 2: Strompreise auswerten

Schauen Sie hier erst hinein, wenn Sie die Aufgabe **selbst** gelöst haben.

Alle Dateien liegen fertig im Ordner `stromanalyse`.

## requirements.txt

```text
pandas
flask
```

`flask` ist nur für die Zusatzaufgabe nötig.

## Virtuelle Umgebung

PyCharm legt beim Öffnen des Projekts eine virtuelle Umgebung `.venv` an und bietet an, die Pakete aus `requirements.txt` zu installieren. Ohne PyCharm geht es auf der Kommandozeile im Verzeichnis `stromanalyse` so:

| Schritt | PowerShell | macOS/Linux |
|---|---|---|
| Umgebung anlegen | `python -m venv .venv` | `python3 -m venv .venv` |
| Pakete installieren | `.venv\Scripts\python.exe -m pip install -r requirements.txt` | `.venv/bin/python -m pip install -r requirements.txt` |
| Programm starten | `.venv\Scripts\python.exe analysis.py` | `.venv/bin/python analysis.py` |

## analysis.py

```python
import pandas as pd


def charging_allowed(price, limit=80):
    return price < limit


data = pd.read_csv("preise.csv")

average = data["price"].mean()
print(f"Mittlerer Preis: {average:.2f} EUR/MWh")

for hour, price in zip(data["hour"], data["price"]):
    if charging_allowed(price):
        print(f"Stunde {hour}: {price} EUR/MWh -> laden")
    else:
        print(f"Stunde {hour}: {price} EUR/MWh -> nicht laden")
```

Ausgabe mit den Beispieldaten:

```text
Mittlerer Preis: 86.62 EUR/MWh
Stunde 0: 78.4 EUR/MWh -> laden
Stunde 1: 72.1 EUR/MWh -> laden
...
Stunde 6: 92.5 EUR/MWh -> nicht laden
...
```

Bewertung:

- `pd.read_csv` liest die Datei, `data["price"]` ist die Spalte, `.mean()` der Mittelwert.
- Der Grenzwert ist ein **Parameter** der Funktion `charging_allowed` mit Standardwert 80, wie im Beispiel der Vorlesung. Ein anderer Grenzwert lässt sich mit `charging_allowed(price, 90)` übergeben.
- Statt der Schleife mit `zip` sind auch `data.iterrows()` oder eine Schleife über `range(len(data))` richtig.
- Eine sinnvolle Variante: den Mittelwert selbst als Grenzwert nehmen, also `charging_allowed(price, average)`.

Häufiger Fehler: `FileNotFoundError` für `preise.csv`. Der Pfad ist relativ zum **Arbeitsverzeichnis**, aus dem das Programm gestartet wird. In PyCharm ist das das Projektverzeichnis, auf der Kommandozeile das Verzeichnis, in dem Sie gerade stehen.

## Zusatzaufgabe: Flask-Webservice (app.py)

```python
import pandas as pd
from flask import Flask

app = Flask(__name__)


@app.get("/average-price")
def average_price():
    data = pd.read_csv("preise.csv")
    return {"average": round(data["price"].mean(), 2), "unit": "EUR/MWh"}


app.run(host="0.0.0.0", port=5000)
```

Aufruf im Browser: `http://localhost:5000/average-price`, Antwort:

```json
{"average": 86.62, "unit": "EUR/MWh"}
```

Gibt die Funktion ein Python-Dictionary zurück, macht Flask daraus automatisch JSON. `host="0.0.0.0"` steht wie im Beispiel der Vorlesung im Aufruf. Lokal ginge es auch ohne, in Aufgabe 4 im Container ist es aber nötig (siehe dort).
