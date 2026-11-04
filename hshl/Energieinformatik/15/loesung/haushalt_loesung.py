"""Musterloesung zu den Folien 11 bis 14 der Uebung: das Haushaltsmodell in Pyomo.

Erst ansehen, wenn Sie die Aufgaben selbst geloest haben.

Start (im Ordner 15/loesung):
  ../../14/optimierung/.venv/bin/python haushalt_loesung.py                  # LP
  ../../14/optimierung/.venv/bin/python haushalt_loesung.py --binaer         # Folie 12
  ../../14/optimierung/.venv/bin/python haushalt_loesung.py --binaer --mindestladung   # Folie 13
  ../../14/optimierung/.venv/bin/python haushalt_loesung.py --binaer --heimspeicher    # Folie 14
Unter Windows: ..\\..\\14\\optimierung\\.venv\\Scripts\\python.exe haushalt_loesung.py --binaer
"""
import argparse
import sys
from pathlib import Path

OPTIMIERUNG = Path(__file__).resolve().parents[2] / "14" / "optimierung"
sys.path.insert(0, str(OPTIMIERUNG))

import pandas as pd  # noqa: E402
import pyomo.environ as pyo  # noqa: E402

from haushalt import (ABFAHRT_KWH, AKKU_KWH, LADE_ETA, NETZ_MAX_KW, START_KWH, WALLBOX_KW,  # noqa: E402
                      WALLBOX_MIN_KW, kennzahlen, kosten, lade_haushalt, referenz, zeige_fahrplan)
from optimierung import loese  # noqa: E402

SPEICHER_KWH = 10     # Heimspeicher (Folie 14)
SPEICHER_KW = 5
SPEICHER_ETA = 0.95   # je Richtung


def baue_modell(d, binaer=False, mindestladung=False, heimspeicher=False):
    m = pyo.ConcreteModel()
    n = len(d)
    m.T = pyo.RangeSet(0, n - 1)
    werte = {s: dict(enumerate(d[s])) for s in d.columns}

    m.bezug = pyo.Var(m.T, bounds=(0, NETZ_MAX_KW))          # kW aus dem Netz
    m.einspeisung = pyo.Var(m.T, bounds=(0, NETZ_MAX_KW))    # kW ins Netz
    m.laden = pyo.Var(m.T, bounds=lambda m, t: (0, WALLBOX_KW * werte["zuhause"][t]))  # nur zu Hause
    m.ladestand = pyo.Var(m.T, bounds=(0, AKKU_KWH))         # kWh am Ende der Stunde

    def vorher(m, t):
        return START_KWH if t == 0 else m.ladestand[t - 1]

    # Akkubilanz wie die Speicherbilanz des Pumpspeichers
    m.akku = pyo.Constraint(m.T, rule=lambda m, t:
                            m.ladestand[t] == vorher(m, t) + LADE_ETA * m.laden[t] - werte["fahrt_kwh"][t])
    # vor jeder Abfahrt mindestens 48 kWh, ebenso am Ende der Woche
    m.abfahrt = pyo.Constraint(m.T, rule=lambda m, t:
                               vorher(m, t) >= ABFAHRT_KWH if werte["abfahrt"][t] == 1 else pyo.Constraint.Skip)
    m.ende = pyo.Constraint(expr=m.ladestand[n - 1] >= ABFAHRT_KWH)

    zufluss = {t: 0 for t in range(n)}
    abfluss = {t: 0 for t in range(n)}
    if heimspeicher:
        m.sp_laden = pyo.Var(m.T, bounds=(0, SPEICHER_KW))
        m.sp_entladen = pyo.Var(m.T, bounds=(0, SPEICHER_KW))
        m.sp_stand = pyo.Var(m.T, bounds=(0, SPEICHER_KWH))
        m.sp_bilanz = pyo.Constraint(m.T, rule=lambda m, t: m.sp_stand[t] == (
            (m.sp_stand[n - 1] if t == 0 else m.sp_stand[t - 1])
            + SPEICHER_ETA * m.sp_laden[t] - m.sp_entladen[t] / SPEICHER_ETA))  # Ende = Anfang
        zufluss = {t: m.sp_entladen[t] for t in range(n)}
        abfluss = {t: m.sp_laden[t] for t in range(n)}

    # Strombilanz des Hauses wie die Waermebilanz der Heizzentrale
    m.strom = pyo.Constraint(m.T, rule=lambda m, t:
                             werte["pv_kw"][t] + m.bezug[t] + zufluss[t]
                             == werte["last_kw"][t] + m.laden[t] + m.einspeisung[t] + abfluss[t])

    m.kosten = pyo.Objective(expr=sum(werte["preis_bezug"][t] * m.bezug[t] - werte["verguetung"][t] * m.einspeisung[t]
                                      for t in m.T), sense=pyo.minimize)

    if binaer:  # Bezug ODER Einspeisung, wie Pumpe oder Turbine
        m.bezieht = pyo.Var(m.T, within=pyo.Binary)
        m.nur_bezug = pyo.Constraint(m.T, rule=lambda m, t: m.bezug[t] <= NETZ_MAX_KW * m.bezieht[t])
        m.nur_einsp = pyo.Constraint(m.T, rule=lambda m, t: m.einspeisung[t] <= NETZ_MAX_KW * (1 - m.bezieht[t]))
    if mindestladung:  # Schwellwert wie die Mindestlast des Kessels
        # Annahme: dreiphasige Wallbox ohne Phasenumschaltung, 6 A je Phase -> 6 A x 3 x 230 V = 4,1 kW.
        # Mit Phasenumschaltung koennte das Auto einphasig schon ab etwa 1,4 kW laden.
        m.laedt = pyo.Var(m.T, within=pyo.Binary)
        m.lade_max = pyo.Constraint(m.T, rule=lambda m, t: m.laden[t] <= WALLBOX_KW * m.laedt[t])
        m.lade_min = pyo.Constraint(m.T, rule=lambda m, t: m.laden[t] >= WALLBOX_MIN_KW * m.laedt[t])
    return m


def plan_aus(m, d):
    namen = {"laden": "laden_kw", "ladestand": "ladestand_kwh", "bezug": "bezug_kw", "einspeisung": "einspeisung_kw"}
    return pd.DataFrame({neu: [pyo.value(getattr(m, alt)[t]) for t in m.T] for alt, neu in namen.items()},
                        index=d.index).clip(lower=0.0)


def main():
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--binaer", action="store_true", help="Bezug ODER Einspeisung (Folie 12)")
    parser.add_argument("--mindestladung", action="store_true", help="Wallbox 0 oder 4,1 bis 11 kW, dreiphasig ohne Phasenumschaltung (Folie 13)")
    parser.add_argument("--heimspeicher", action="store_true", help="Batteriespeicher 10 kWh, 5 kW (Folie 14)")
    parser.add_argument("--grafik", action="store_true", help="Fahrplan zeichnen")
    args = parser.parse_args()

    d = lade_haushalt()
    ref = referenz(d)
    kennzahlen(d, ref, "Referenz")
    m = baue_modell(d, args.binaer, args.mindestladung, args.heimspeicher)
    loese(m)
    plan = plan_aus(m, d)
    name = "MILP" if (args.binaer or args.mindestladung) else "LP"
    kennzahlen(d, plan, name)
    print(f"Zielfunktion: {pyo.value(m.kosten):.2f} Euro, Ersparnis gegenueber der Referenz: "
          f"{1 - pyo.value(m.kosten) / kosten(d, ref):.0%}")
    lokal = plan.copy()
    lokal.index = lokal.index.strftime("%a %d.%m. %H:%M")
    beides = (plan["bezug_kw"] > 0.01) & (plan["einspeisung_kw"] > 0.01)
    if beides.any():
        print("\nStunden mit Bezug UND Einspeisung:")
        print(lokal[beides.to_numpy()].round(2).to_string())
    mai = plan.index.date == pd.Timestamp("2026-05-01").date()
    print("\nLaden am 1. Mai (kW):")
    print(lokal[mai & (plan["laden_kw"] > 0.01).to_numpy()]["laden_kw"].round(2).to_string())
    if args.grafik:
        zeige_fahrplan(d, plan, name)


if __name__ == "__main__":
    main()
