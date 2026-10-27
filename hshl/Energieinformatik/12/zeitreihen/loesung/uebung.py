"""Musterloesung zu den Python-Aufgaben der Uebung (Glaetten, Prophet, saisonal naiv).

Start aus dem Ordner zeitreihen:   .venv/bin/python loesung/uebung.py
"""
import logging
import sys
import warnings
from pathlib import Path

import matplotlib.pyplot as plt
import numpy as np
import pandas as pd
from prophet import Prophet

sys.path.insert(0, str(Path(__file__).resolve().parent.parent))  # zeitreihe.py und uebung.py
import zeitreihe as z
from uebung import HORIZONT, STICHTAG, v  # laedt den Verbrauch wie das Startskript

logging.getLogger("cmdstanpy").setLevel(logging.WARNING)
logging.getLogger("prophet.plot").disabled = True
warnings.filterwarnings("ignore")

# Aufgabe 13: gleitende Mittelwerte und exponentielles Glaetten
januar = v.loc["2026-01-01":"2026-01-31", "kw"]
plt.figure(figsize=(10, 4))
plt.plot(januar, color="lightgray", label="Messung")
plt.plot(v["kw"].rolling(24).mean().loc[januar.index], label="gleitender Mittelwert 24 h")
plt.plot(v["kw"].rolling(168).mean().loc[januar.index], label="gleitender Mittelwert 168 h")
plt.plot(v["kw"].ewm(alpha=0.1).mean().loc[januar.index], "--", label="exponentiell, alpha = 0,1")
plt.ylabel("Wärmeleistung in kW")
plt.legend()
plt.tight_layout()
plt.show()

# Aufgabe 14: eigene Prognose mit Prophet
train = v[v.index < STICHTAG]
test = v[v.index >= STICHTAG].iloc[:HORIZONT]

m = Prophet(daily_seasonality=True, weekly_seasonality=True, yearly_seasonality=False)
m.fit(z.nach_prophet(train))
forecast = m.predict(z.nach_prophet(test)[["ds"]])
mae, mape = z.fehler(test["kw"], pd.Series(forecast["yhat"].to_numpy(), index=test.index))
print(f"Prophet ohne Temperatur:  MAE {mae:5.2f} kW, MAPE {mape:4.1f} %")

# Vertiefung: Aussentemperatur und Feiertage
m = Prophet(daily_seasonality=True, weekly_seasonality=True, yearly_seasonality=False)
m.add_regressor("temp")
m.add_country_holidays(country_name="DE")
m.fit(z.nach_prophet(train))
forecast = m.predict(z.nach_prophet(test)[["ds", "temp"]])
mae, mape = z.fehler(test["kw"], pd.Series(forecast["yhat"].to_numpy(), index=test.index))
print(f"Prophet mit Temperatur:   MAE {mae:5.2f} kW, MAPE {mape:4.1f} %")
m.plot_components(forecast)
plt.show()


# Herausforderung: saisonal naiv selbst geschrieben
def saisonal_naiv(train, periode, horizont=HORIZONT):
    letzte = train["kw"].iloc[-periode:].to_numpy()
    wiederholt = np.tile(letzte, horizont // periode + 1)[:horizont]
    return pd.Series(wiederholt, index=test.index)


for periode in (24, 168):
    mae, mape = z.fehler(test["kw"], saisonal_naiv(train, periode))
    print(f"saisonal naiv, {periode:3} h:   MAE {mae:5.2f} kW, MAPE {mape:4.1f} %")
