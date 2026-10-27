"""Waermeverbrauch des Hauses fuer eine Woche prognostizieren und in TimescaleDB zurueckschreiben.

    prognose.py [STICHTAG]   Vorgabe: 2026-01-08 (Beginn der Prognosewoche, UTC)
"""
import logging
import sys
import warnings

import pandas as pd
from sqlalchemy import create_engine, text

import zeitreihe as z

logging.getLogger("cmdstanpy").setLevel(logging.WARNING)
warnings.filterwarnings("ignore")

SQL = """
SELECT v.zeit, v.kw, w.temperatur_c AS aussentemperatur_c
FROM verbrauch_stunde v JOIN wetter w ON w.zeit = v.zeit
WHERE v.geraet = 'wmz-haupt'
ORDER BY v.zeit
"""


def lade_verbrauch(engine):
    df = pd.read_sql(SQL, engine, index_col="zeit", parse_dates=["zeit"])
    df.index = pd.to_datetime(df.index, utc=True)
    return z.bereite_vor(df)


def speichere(engine, verfahren, werte, unten=None, oben=None):
    df = pd.DataFrame({"zeit": werte.index, "verfahren": verfahren, "wert": werte.to_numpy(),
                       "unten": unten, "oben": oben}).astype({"unten": float, "oben": float})
    with engine.begin() as conn:
        conn.execute(text("DELETE FROM prognosen WHERE verfahren = :v"), {"v": verfahren})
        df.to_sql("prognosen", conn, if_exists="append", index=False)


if __name__ == "__main__":
    stichtag = pd.Timestamp(sys.argv[1] if len(sys.argv) > 1 else "2026-01-08", tz="UTC")
    engine = create_engine(z.DB_URL.replace("postgresql://", "postgresql+psycopg://"))
    v = lade_verbrauch(engine)
    train = v[v.index < stichtag]
    test = v[v.index >= stichtag].iloc[:z.HORIZONT]

    prognosen = {
        "saisonal naiv (Tag)": z.saisonal_naiv(train, periode=24),
        "Holt-Winters": z.holt_winters(train),
        "SARIMA": z.sarima(train),
        "SARIMAX mit Temperatur": z.sarima(train, test),
        "Regression mit Temperatur": z.regression(v, stichtag)[1],
    }
    _, f = z.prophet(train, test)
    prognosen["Prophet"] = f["yhat"]
    _, f_temp = z.prophet(train, test, mit_temperatur=True)
    prognosen["Prophet mit Temperatur"] = f_temp["yhat"]

    print(f"Training bis {stichtag:%d.%m.%Y}, Prognose fuer {len(test)} Stunden\n")
    print(f"{'Verfahren':28} {'MAE in kW':>10} {'MAPE in %':>10}")
    for name, p in prognosen.items():
        p.index = test.index
        mae, mape = z.fehler(test["kw"], p)
        print(f"{name:28} {mae:10.2f} {mape:10.1f}")
        if name.startswith("Prophet"):
            band = f_temp if "Temperatur" in name else f
            speichere(engine, name, p, band["yhat_lower"].to_numpy(), band["yhat_upper"].to_numpy())
        else:
            speichere(engine, name, p)
    print("\nPrognosen in Tabelle prognosen gespeichert")
