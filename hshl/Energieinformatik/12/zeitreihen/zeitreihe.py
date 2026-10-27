"""Gemeinsame Funktionen fuer das Beispiel "Waermeverbrauch eines Mehrfamilienhauses".

Die Daten sind synthetisch, aber plausibel: Tagesgang durch Warmwasser,
Wochengang, Heizlast abhaengig von der Aussentemperatur, Feiertage,
ein Kaelteeinbruch im Januar, einige Luecken und ein Ausreisser.
"""
import os
from pathlib import Path

import numpy as np
import pandas as pd

ORDNER = Path(__file__).resolve().parent
CSV_DATEI = ORDNER / "daten" / "waerme.csv"

START = "2025-10-01 00:00"
ENDE = "2026-03-31 23:00"
SEED = 12

# Verbindungsdaten, passend zur docker-compose.yaml
DB_URL = os.environ.get("DB_URL", "postgresql://postgres:geheim@localhost:5432/energie")
MQTT_HOST = os.environ.get("MQTT_HOST", "127.0.0.1")
MQTT_PORT = int(os.environ.get("MQTT_PORT", "1883"))
STICHTAG = pd.Timestamp("2026-01-08 00:00", tz="UTC")  # Beginn des Testzeitraums
HORIZONT = 7 * 24                                       # eine Woche in Stunden

GERAETE = {
    # Name: (DevEUI, Anteil an der Last des Hauses)
    "wmz-haupt": ("70b3d57ed0001000", 1.0),
    "wmz-whg-03": ("70b3d57ed0001234", 0.09),
}
FEIERTAGE = ["2025-10-03", "2025-11-01", "2025-12-24", "2025-12-25",
             "2025-12-26", "2025-12-31", "2026-01-01"]


def _glocke(stunde, mitte, breite):
    return np.exp(-0.5 * ((stunde - mitte) / breite) ** 2)


def erzeuge_daten():
    """Erzeugt die stuendlichen Rohdaten aller Zaehler als DataFrame."""
    rng = np.random.default_rng(SEED)
    zeit = pd.date_range(START, ENDE, freq="h", tz="UTC")
    lokal = zeit.tz_convert("Europe/Berlin")
    n = len(zeit)

    # Aussentemperatur: Jahresgang + mehrtaegige Wetterlagen + Tagesgang
    tag_im_jahr = np.asarray(lokal.dayofyear)
    jahresgang = 7.5 - 6.5 * np.cos(2 * np.pi * (tag_im_jahr - 20) / 365)
    tage = np.asarray((zeit - zeit[0]).days)
    wetter = np.zeros(tage.max() + 1)
    for i in range(1, len(wetter)):
        wetter[i] = 0.8 * wetter[i - 1] + rng.normal(0, 1.8)
    wetter_stuendlich = np.interp(np.arange(n) / 24, np.arange(len(wetter)), wetter)
    stunde = np.asarray(lokal.hour + lokal.minute / 60)
    tagesgang_t = 3 * np.sin(2 * np.pi * (stunde - 9) / 24)
    k_start = pd.Timestamp("2026-01-11 12:00", tz="UTC")
    k_ende = pd.Timestamp("2026-01-18 12:00", tz="UTC")
    h = np.asarray((zeit - k_start) / pd.Timedelta(hours=1))
    dauer = (k_ende - k_start) / pd.Timedelta(hours=1)
    kaelte = -8 * np.sqrt(np.sin(np.pi * np.clip(h / dauer, 0, 1)))
    temperatur = jahresgang + wetter_stuendlich + tagesgang_t + kaelte + rng.normal(0, 0.3, n)

    # Warmwasser: Spitzen morgens und abends, am Wochenende spaeter
    wochenende = np.asarray(lokal.dayofweek >= 5)
    feiertag = np.asarray(lokal.normalize().tz_localize(None).isin(pd.to_datetime(FEIERTAGE)))
    frei = wochenende | feiertag
    morgen = np.where(frei, 8.5, 7.0)
    warmwasser = (2.0 + np.where(frei, 4.0, 6.0) * _glocke(stunde, morgen, 1.2)
                  + 5.0 * _glocke(stunde, 19.5, 1.8) + np.where(frei, 2.0, 0) * _glocke(stunde, 13, 2.5))

    # Heizlast: Gebaeude reagiert traege, Nachtabsenkung von 22 bis 5 Uhr
    t_traege = pd.Series(temperatur).ewm(halflife=8).mean().to_numpy()
    absenkung = np.where((stunde >= 22) | (stunde < 5), 0.7, 1.0)
    heizlast = 1.6 * np.clip(15 - t_traege, 0, None) * absenkung
    heizlast = heizlast * np.where(feiertag, 0.9, 1.0)

    leistung = (warmwasser + heizlast) * rng.normal(1, 0.04, n) + rng.normal(0, 0.4, n)
    leistung = np.clip(leistung, 0.3, None)

    # Ausreisser: Legionellenschaltung mit sehr hoher Last in einer Stunde
    ausreisser = zeit.get_loc(pd.Timestamp("2025-11-25 13:00", tz="UTC"))
    leistung[ausreisser] += 45

    vorlauf = np.clip(45 + 1.0 * (15 - t_traege), 45, 70) + rng.normal(0, 0.3, n)

    zeilen = []
    for name, (dev_eui, anteil) in GERAETE.items():
        rauschen = rng.normal(1, 0.15 if anteil < 1 else 0.0, n)
        p = np.clip(leistung * anteil * rauschen, 0.02, None)
        # Zaehlerstand zum Zeitpunkt der Ablesung (ca. 3 s nach der vollen Stunde)
        stand = 250000 * anteil + np.concatenate([[0], np.cumsum(p)[:-1]])
        spreizung = 10 + 0.25 * p / anteil
        ruecklauf = vorlauf - spreizung + rng.normal(0, 0.5, n)
        zeilen.append(pd.DataFrame({
            "zeit": zeit + pd.Timedelta(seconds=3),
            "geraet": name,
            "energie_kwh": np.round(stand, 2),
            "vorlauf_c": np.round(vorlauf, 1),
            "ruecklauf_c": np.round(ruecklauf, 1),
            "aussentemperatur_c": np.round(temperatur, 1),
        }))
    daten = pd.concat(zeilen, ignore_index=True)

    # Luecken: verlorene Uplinks (fehlende Ablesungen)
    luecken = [("2025-11-18 03:00", "2025-11-18 07:00"), ("2025-12-09 14:00", "2025-12-09 14:00"),
               ("2026-02-03 01:00", "2026-02-03 02:00")]
    for von, bis in luecken:
        maske = daten["zeit"].between(pd.Timestamp(von, tz="UTC"), pd.Timestamp(bis, tz="UTC") + pd.Timedelta(minutes=1))
        daten = daten[~maske]
    return daten.reset_index(drop=True)


def schreibe_csv(daten, datei=CSV_DATEI):
    datei.parent.mkdir(exist_ok=True)
    aus = daten.copy()
    aus["zeit"] = aus["zeit"].dt.strftime("%Y-%m-%dT%H:%M:%SZ")
    aus.to_csv(datei, index=False)


def lade_csv(datei=CSV_DATEI):
    daten = pd.read_csv(datei)
    daten["zeit"] = pd.to_datetime(daten["zeit"], utc=True)
    return daten


def verbrauch_stuendlich(daten, geraet="wmz-haupt"):
    """Wie die Sicht verbrauch_stunde in der Datenbank: Differenz zweier
    Zaehlerstaende, geteilt durch die Stunden dazwischen, zum Beginn des Intervalls."""
    d = daten[daten["geraet"] == geraet].sort_values("zeit").copy()
    d["zeit"] = d["zeit"].dt.floor("h")
    stunden = d["zeit"].diff().shift(-1) / pd.Timedelta(hours=1)
    d["kw"] = d["energie_kwh"].diff().shift(-1) / stunden
    d = d.dropna(subset=["kw"]).set_index("zeit")
    return d[["kw", "vorlauf_c", "ruecklauf_c", "aussentemperatur_c"]]


# ---------------------------------------------------------------- Prognosen

def fehler(y, y_hat):
    """MAE in kW und MAPE in Prozent."""
    mae = (y - y_hat).abs().mean()
    mape = ((y - y_hat) / y).abs().mean() * 100
    return mae, mape


def bereite_vor(verbrauch):
    """Stuendliches Raster, kurze Luecken interpolieren."""
    v = verbrauch.asfreq("h")
    return v.interpolate(limit=6)


def saisonal_naiv(train, horizont=HORIZONT, periode=168):
    """Gleiche Stunde eine Periode vorher."""
    letzte = train["kw"].iloc[-periode:].to_numpy()
    werte = np.resize(letzte, horizont)
    index = pd.date_range(train.index[-1] + pd.Timedelta(hours=1), periods=horizont, freq="h")
    return pd.Series(werte, index=index)


def holt_winters(train, horizont=HORIZONT):
    from statsmodels.tsa.holtwinters import ExponentialSmoothing
    fit = ExponentialSmoothing(train["kw"], trend="add", seasonal="add",
                               seasonal_periods=24).fit()
    return fit.forecast(horizont)


def nach_prophet(v):
    """Prophet erwartet ds (Ortszeit ohne Zeitzone) und y."""
    return pd.DataFrame({"ds": v.index.tz_convert("Europe/Berlin").tz_localize(None),
                         "y": v["kw"].to_numpy(), "temp": v["aussentemperatur_c"].to_numpy()})


def prophet(train, test, mit_temperatur=False):
    """Prognose fuer den Zeitraum von test; liefert DataFrame mit yhat, yhat_lower, yhat_upper."""
    import logging
    logging.getLogger("prophet.plot").disabled = True  # Hinweis auf fehlendes plotly
    from prophet import Prophet
    m = Prophet(daily_seasonality=True, weekly_seasonality=True, yearly_seasonality=False)
    m.add_country_holidays(country_name="DE")
    if mit_temperatur:
        m.add_regressor("temp")
    m.fit(nach_prophet(train))
    future = nach_prophet(test)[["ds", "temp"]]
    forecast = m.predict(future)
    forecast.index = test.index
    return m, forecast


def sarima(train, test=None, wochen=4):
    """Saisonales ARIMA mit Tagessaison, trainiert auf den letzten Wochen.
    Mit test wird die Aussentemperatur als Einflussgroesse genutzt (SARIMAX)."""
    from statsmodels.tsa.statespace.sarimax import SARIMAX
    import warnings
    t = train.iloc[-wochen * 168:]
    exog = t[["aussentemperatur_c"]] if test is not None else None
    with warnings.catch_warnings():
        warnings.simplefilter("ignore")  # Hinweise zu Startwerten der Schaetzung
        fit = SARIMAX(t["kw"], exog=exog, order=(2, 0, 1),
                      seasonal_order=(1, 1, 1, 24)).fit(disp=False)
    if test is None:
        return fit.forecast(HORIZONT)
    return fit.forecast(len(test), exog=test[["aussentemperatur_c"]])


def merkmale(v):
    """Merkmale fuer die Regression: Temperatur, Stunde, Wochenende, Wert vor einer Woche."""
    lokal = v.index.tz_convert("Europe/Berlin")
    x = pd.DataFrame({"temp": v["aussentemperatur_c"].to_numpy(),
                      "wochenende": (lokal.dayofweek >= 5).astype(int),
                      "kw_vorwoche": v["kw"].shift(168).to_numpy()}, index=v.index)
    stunden = pd.get_dummies(lokal.hour, prefix="h", dtype=int)
    stunden.index = v.index
    return pd.concat([x, stunden], axis=1)


def regression(v, stichtag=STICHTAG, horizont=HORIZONT):
    """Lineare Regression mit scikit-learn; v enthaelt Training und Testzeitraum."""
    from sklearn.linear_model import LinearRegression
    x = merkmale(v)
    train = x.index < stichtag
    test = (x.index >= stichtag) & (x.index < stichtag + pd.Timedelta(hours=horizont))
    ok = train & x.notna().all(axis=1).to_numpy()
    modell = LinearRegression().fit(x[ok], v["kw"][ok])
    return modell, pd.Series(modell.predict(x[test]), index=x.index[test])
