"""Lehrbeispiel C: Kraftwerkseinsatzplanung (Unit Commitment) fuer einen Tag.

Drei Kraftwerke decken eine vorgegebene stuendliche Last. Ein Kraftwerk ist
entweder aus oder laeuft zwischen Mindest- und Hoechstleistung. Jeder Start
kostet Geld. Mit --rampen darf sich die Leistung je Stunde nur begrenzt aendern.
"""
import argparse

import pandas as pd
import pyomo.environ as pyo

from optimierung import loese

# Kraftwerksdaten: Pmax in MW, Mindestlast (Anteil), variable Kosten Euro/MWh,
# Startkosten Euro, Rampe in MW/h, am Netz zu Beginn des Tages
KRAFTWERKE = pd.DataFrame({
    "p_max": [800, 500, 400],
    "min_last": [0.40, 0.35, 0.20],
    "kosten": [32, 58, 92],
    "start": [40000, 20000, 5000],
    "rampe": [150, 200, 300],
    "an_zu_beginn": [1, 0, 0],
}, index=["Braunkohle", "Steinkohle", "Gas"])

# Last in MW je Stunde (Tagesgang mit Spitzen morgens und abends)
LAST = [720, 700, 690, 690, 710, 780, 920, 1080, 1180, 1220, 1240, 1250,
        1230, 1210, 1190, 1200, 1260, 1360, 1420, 1380, 1250, 1080, 900, 780]


def baue_modell(kw=KRAFTWERKE, last=LAST, rampen=False):
    m = pyo.ConcreteModel()
    m.K = pyo.Set(initialize=kw.index.to_list())          # Kraftwerke
    m.T = pyo.RangeSet(1, len(last))                      # Stunden 1..24
    m.T0 = pyo.RangeSet(0, len(last))                     # mit Stunde 0 (Ausgangszustand)

    m.p = pyo.Var(m.K, m.T0, domain=pyo.NonNegativeReals)  # Leistung in MW
    m.an = pyo.Var(m.K, m.T0, within=pyo.Binary)           # 1 = am Netz
    m.start = pyo.Var(m.K, m.T, within=pyo.Binary)         # 1 = Anfahrt in Stunde t

    # Ausgangszustand in Stunde 0
    for k in m.K:
        m.an[k, 0].fix(int(kw.loc[k, "an_zu_beginn"]))
        m.p[k, 0].fix(kw.loc[k, "p_max"] * kw.loc[k, "min_last"] * kw.loc[k, "an_zu_beginn"])

    # Last decken
    m.last = pyo.Constraint(m.T, rule=lambda m, t: sum(m.p[k, t] for k in m.K) == last[t - 1])

    # Schwellwert: aus (0) oder zwischen Pmin und Pmax
    def p_max(m, k, t):
        return m.p[k, t] <= kw.loc[k, "p_max"] * m.an[k, t]
    def p_min(m, k, t):
        return m.p[k, t] >= kw.loc[k, "min_last"] * kw.loc[k, "p_max"] * m.an[k, t]
    m.p_max = pyo.Constraint(m.K, m.T, rule=p_max)
    m.p_min = pyo.Constraint(m.K, m.T, rule=p_min)

    # Anfahrt: nicht an(t-1) und an(t) -> start(t)
    def anfahrt(m, k, t):
        return m.an[k, t - 1] + (1 - m.an[k, t]) + m.start[k, t] >= 1
    m.anfahrt = pyo.Constraint(m.K, m.T, rule=anfahrt)

    if rampen:
        m.rampe_hoch = pyo.Constraint(m.K, m.T, rule=lambda m, k, t:
                                      m.p[k, t] - m.p[k, t - 1] <= max(kw.loc[k, "rampe"], kw.loc[k, "min_last"] * kw.loc[k, "p_max"]))
        m.rampe_runter = pyo.Constraint(m.K, m.T, rule=lambda m, k, t:
                                        m.p[k, t - 1] - m.p[k, t] <= max(kw.loc[k, "rampe"], kw.loc[k, "min_last"] * kw.loc[k, "p_max"]))

    def kosten(m):
        return (sum(kw.loc[k, "kosten"] * m.p[k, t] for k in m.K for t in m.T)
                + sum(kw.loc[k, "start"] * m.start[k, t] for k in m.K for t in m.T))
    m.kosten = pyo.Objective(rule=kosten, sense=pyo.minimize)
    return m


def rechne(rampen=False):
    m = baue_modell(rampen=rampen)
    ergebnis = loese(m)
    plan = pd.DataFrame({k: [pyo.value(m.p[k, t]) for t in m.T] for k in m.K},
                        index=pd.RangeIndex(0, 24, name="stunde"))
    plan["last"] = LAST
    starts = {k: int(round(sum(pyo.value(m.start[k, t]) for t in m.T))) for k in m.K}
    return plan, pyo.value(m.kosten), starts, ergebnis


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--rampen", action="store_true", help="Rampen beruecksichtigen")
    args = parser.parse_args()
    plan, kosten, starts, ergebnis = rechne(args.rampen)
    print(plan.round(0).to_string())
    print(f"\nKosten des Tages: {kosten:,.0f} Euro".replace(",", "."))
    print("Starts:", starts)
    print("Solver:", ergebnis.solver.termination_condition)


if __name__ == "__main__":
    main()
