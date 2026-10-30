"""Lehrbeispiel B: Das Pumpspeicherkraftwerk Herdecke an einem Tag bewirtschaften.

Gegen echte Day-Ahead-Preise (1. Mai 2026, stuendlich gemittelt) wird
entschieden, wann gepumpt und wann turbiniert wird. Mit --milp darf die
Maschine in jeder Stunde nur pumpen ODER turbinieren.
"""
import argparse

import pyomo.environ as pyo

from optimierung import ERGEBNISSE, als_tabelle, lade_preise, loese

PUMPE_MW = 153       # Pumpleistung
TURBINE_MW = 162     # Turbinenleistung
SPEICHER_MWH = 590   # Arbeitsvermoegen des Oberbeckens
ETA = 0.75           # Gesamtwirkungsgrad, beim Pumpen angesetzt
START_MWH = 295      # halb voll am Anfang und am Ende des Tages


def baue_modell(preise, milp=False):
    m = pyo.ConcreteModel()
    m.T = pyo.RangeSet(1, len(preise))                    # Stunden 1..24
    m.preis = pyo.Param(m.T, initialize=dict(enumerate(preise, start=1)))

    m.pumpe = pyo.Var(m.T, bounds=(0, PUMPE_MW))          # MW
    m.turbine = pyo.Var(m.T, bounds=(0, TURBINE_MW))      # MW
    m.fuellstand = pyo.Var(pyo.RangeSet(0, len(preise)), bounds=(0, SPEICHER_MWH))  # MWh

    def speicherbilanz(m, t):
        return m.fuellstand[t] == m.fuellstand[t - 1] + ETA * m.pumpe[t] - m.turbine[t]
    m.bilanz = pyo.Constraint(m.T, rule=speicherbilanz)
    m.start = pyo.Constraint(expr=m.fuellstand[0] == START_MWH)
    m.ende = pyo.Constraint(expr=m.fuellstand[len(preise)] == START_MWH)

    def gewinn(m):
        return sum(m.preis[t] * (m.turbine[t] - m.pumpe[t]) for t in m.T)
    m.gewinn = pyo.Objective(rule=gewinn, sense=pyo.maximize)

    if milp:
        m.pumpt = pyo.Var(m.T, within=pyo.Binary)         # 1 = Pumpbetrieb, 0 = Turbinenbetrieb
        m.nur_pumpe = pyo.Constraint(m.T, rule=lambda m, t: m.pumpe[t] <= PUMPE_MW * m.pumpt[t])
        m.nur_turbine = pyo.Constraint(m.T, rule=lambda m, t: m.turbine[t] <= TURBINE_MW * (1 - m.pumpt[t]))
    return m


def rechne(milp=False, datei="preise_2026-05-01.csv"):
    preise = lade_preise(datei)
    m = baue_modell(preise.to_list(), milp)
    loese(m)
    plan = als_tabelle(m, ["pumpe", "turbine"], preise.index)
    plan["fuellstand"] = [pyo.value(m.fuellstand[t]) for t in m.T]
    plan["preis"] = preise
    return plan, pyo.value(m.gewinn)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--milp", action="store_true", help="pumpen ODER turbinieren (Binaervariable)")
    args = parser.parse_args()
    plan, gewinn = rechne(args.milp)
    lokal = plan.copy()
    lokal.index = lokal.index.tz_convert("Europe/Berlin").strftime("%H:%M")
    print(lokal.round(1).to_string())
    beides = ((plan["pumpe"] > 1e-6) & (plan["turbine"] > 1e-6)).sum()
    print(f"\nGewinn des Tages: {gewinn:,.0f} Euro".replace(",", "."))
    print(f"Stunden mit Pumpen UND Turbinieren: {beides}")
    ERGEBNISSE.mkdir(exist_ok=True)
    plan.to_csv(ERGEBNISSE / f"pumpspeicher{'_milp' if args.milp else ''}.csv")


if __name__ == "__main__":
    main()
