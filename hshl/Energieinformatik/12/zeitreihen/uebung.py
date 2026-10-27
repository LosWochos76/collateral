"""Startskript fuer die Uebung: Waermeverbrauch laden und selbst auswerten.

    uebung.py   laedt den stuendlichen Verbrauch von wmz-haupt in den DataFrame v
"""
import logging
import warnings

import pandas as pd
from sqlalchemy import create_engine

import zeitreihe as z
from zeitreihe import HORIZONT, STICHTAG

logging.getLogger("cmdstanpy").setLevel(logging.WARNING)
logging.getLogger("prophet.plot").disabled = True  # Hinweis auf fehlendes plotly
warnings.filterwarnings("ignore")

SQL = """
SELECT v.zeit, v.kw, w.temperatur_c AS aussentemperatur_c
FROM verbrauch_stunde v JOIN wetter w ON w.zeit = v.zeit
WHERE v.geraet = 'wmz-haupt'
ORDER BY v.zeit
"""


def lade_verbrauch():
    """Verbrauch je Stunde in kW mit Aussentemperatur, Index zeit (UTC).
    Ist die Datenbank nicht erreichbar, wird dieselbe Reihe aus daten/waerme.csv berechnet."""
    try:
        engine = create_engine(z.DB_URL.replace("postgresql://", "postgresql+psycopg://"))
        df = pd.read_sql(SQL, engine, index_col="zeit", parse_dates=["zeit"])
        df.index = pd.to_datetime(df.index, utc=True)
        if df.empty:
            raise ValueError("keine Daten in verbrauch_stunde")
    except Exception as fehler:
        print(f"Datenbank nicht nutzbar ({type(fehler).__name__}), lade daten/waerme.csv")
        df = z.verbrauch_stuendlich(z.lade_csv())[["kw", "aussentemperatur_c"]]
    return z.bereite_vor(df)


v = lade_verbrauch()
print(v.head())
print(f"{len(v)} Stunden, Stichtag {STICHTAG:%d.%m.%Y}, Horizont {HORIZONT} Stunden")

# Aufgabe 13: gleitende Mittelwerte und exponentielles Glaetten


# Aufgabe 14: eigene Prognose mit Prophet

