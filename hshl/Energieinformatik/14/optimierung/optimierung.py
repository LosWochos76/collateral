"""Gemeinsame Funktionen fuer die Optimierungsbeispiele aus Kapitel 14.

- Daten laden (Day-Ahead-Preise, Waermebedarf der Testwoche aus Kapitel 12),
- ein Modell mit HiGHS loesen,
- Kennzahlen der Heizzentrale (Leistungszahl der Waermepumpe, Preise).
"""
from pathlib import Path

import numpy as np
import pandas as pd
import pyomo.environ as pyo

ORDNER = Path(__file__).resolve().parent
DATEN = ORDNER / "daten"
ERGEBNISSE = ORDNER / "ergebnisse"

# Annahmen fuer die Heizzentrale des Mehrfamilienhauses
WP_MAX_KW = 40          # hoechste Waermeleistung der Waermepumpe in kW
KESSEL_MAX_KW = 60      # hoechste Waermeleistung des Gaskessels in kW
KESSEL_MIN = 0.3        # Mindestlast des Kessels (nur im MILP)
KESSEL_ETA = 0.95       # Wirkungsgrad des Kessels
SPEICHER_KWH = 175      # 5 m3 Wasser, 30 K Spreizung: 5000 kg * 4,19 kJ/(kg K) * 30 K
SPEICHER_VERLUST = 0.005  # Verlust je Stunde (Anteil des Inhalts)
GASPREIS = 0.11         # Euro je kWh Gas
AUFSCHLAG = 0.20        # Euro je kWh Strom zusaetzlich zum Boersenpreis
LEISTUNGSPREIS = 100 * 7 / 365  # Euro je kW Spitzenleistung, 100 Euro/(kW a) anteilig fuer eine Woche


def lade_preise(datei="preise_testwoche.csv"):
    """Day-Ahead-Preise als Series in Euro/MWh, Index: Zeit in UTC."""
    df = pd.read_csv(DATEN / datei)
    df["zeit"] = pd.to_datetime(df["zeit"], utc=True)
    return df.set_index("zeit")["preis_eur_mwh"]


def lade_waermebedarf(datei="waermebedarf.csv"):
    """Waermebedarf der Testwoche: Prognose, Messung und Aussentemperatur."""
    df = pd.read_csv(DATEN / datei)
    df["zeit"] = pd.to_datetime(df["zeit"], utc=True)
    return df.set_index("zeit")


def cop(temperatur):
    """Leistungszahl der Luft-Wasser-Waermepumpe, linear in der Aussentemperatur."""
    return np.clip(3.2 + 0.07 * np.asarray(temperatur), 1.8, 4.5)


def daten_heizzentrale(spalte="prognose_kw", bedarf=None, preise=None):
    """Eine Tabelle mit allen Eingaben der Heizzentrale, je Stunde eine Zeile."""
    wb = lade_waermebedarf() if bedarf is None else bedarf
    p = lade_preise() if preise is None else preise
    d = pd.DataFrame({"bedarf_kw": wb[spalte],
                      "temperatur_c": wb["aussentemperatur_c"]})
    d["strompreis"] = p.reindex(d.index).to_numpy() / 1000 + AUFSCHLAG  # Euro/kWh
    d["cop"] = cop(d["temperatur_c"])
    return d


def loese(modell, mip_gap=None):
    """Loest ein Pyomo-Modell mit HiGHS und prueft, ob eine optimale Loesung vorliegt."""
    solver = pyo.SolverFactory("highs")
    optionen = {"mip_rel_gap": mip_gap} if mip_gap is not None else {}
    ergebnis = solver.solve(modell, options=optionen) if optionen else solver.solve(modell)
    status = ergebnis.solver.termination_condition
    if status != pyo.TerminationCondition.optimal:
        raise RuntimeError(f"Keine optimale Loesung gefunden: {status}")
    return ergebnis


def als_tabelle(modell, namen, index):
    """Liest indizierte Variablen aus dem Modell in ein DataFrame."""
    return pd.DataFrame({n: [pyo.value(getattr(modell, n)[t]) for t in modell.T] for n in namen},
                        index=index)
