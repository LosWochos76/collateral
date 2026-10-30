"""Laufendes Beispiel: die Heizzentrale des Mehrfamilienhauses optimieren.

Waermepumpe, Gaskessel und Waermespeicher sollen den Waermebedarf der
Testwoche (8. bis 14. Januar 2026) moeglichst guenstig decken. Eingaben sind
die Waermebedarfsprognose aus Kapitel 12 und echte Day-Ahead-Preise.

Optionen:
  --milp         Kessel mit Mindestlast (Binaervariable)
  --lastspitze   Leistungspreis fuer die hoechste Strombezugsleistung
  --referenz     einfache waermegefuehrte Regelung zum Vergleich
  --nachrechnen  Fahrplan aus der Prognose mit dem gemessenen Bedarf bewerten
  --prognose     regression (Standard) oder naiv: welche Prognose aus Kapitel 12
"""
import argparse

import numpy as np
import pandas as pd
import pyomo.environ as pyo

from optimierung import (ERGEBNISSE, GASPREIS, KESSEL_ETA, KESSEL_MAX_KW, KESSEL_MIN,
                         LEISTUNGSPREIS, SPEICHER_KWH, SPEICHER_VERLUST, WP_MAX_KW,
                         daten_heizzentrale, loese)


def baue_modell(d, milp=False, lastspitze=False, speicher_kwh=SPEICHER_KWH, start_kwh=None, ende_kwh=None):
    """d: DataFrame mit bedarf_kw, strompreis (Euro/kWh) und cop je Stunde."""
    m = pyo.ConcreteModel()
    n = len(d)
    m.T = pyo.RangeSet(1, n)
    m.T0 = pyo.RangeSet(0, n)
    m.bedarf = pyo.Param(m.T, initialize=dict(enumerate(d["bedarf_kw"], start=1)))
    m.preis = pyo.Param(m.T, initialize=dict(enumerate(d["strompreis"], start=1)))
    m.cop = pyo.Param(m.T, initialize=dict(enumerate(d["cop"], start=1)))

    m.wp = pyo.Var(m.T, bounds=(0, WP_MAX_KW))             # Waerme aus der Waermepumpe, kW
    m.kessel = pyo.Var(m.T, bounds=(0, KESSEL_MAX_KW))     # Waerme aus dem Kessel, kW
    m.laden = pyo.Var(m.T, domain=pyo.NonNegativeReals)    # in den Speicher, kW
    m.entladen = pyo.Var(m.T, domain=pyo.NonNegativeReals) # aus dem Speicher, kW
    m.speicher = pyo.Var(m.T0, bounds=(0, speicher_kwh))   # Fuellstand, kWh

    # Waermebilanz: Erzeugung + Entladen - Laden = Bedarf
    m.waerme = pyo.Constraint(m.T, rule=lambda m, t:
                              m.wp[t] + m.kessel[t] + m.entladen[t] - m.laden[t] == m.bedarf[t])
    # Speicher: Verlust je Stunde, Zufluss und Abfluss
    m.bilanz = pyo.Constraint(m.T, rule=lambda m, t:
                              m.speicher[t] == (1 - SPEICHER_VERLUST) * m.speicher[t - 1]
                              + m.laden[t] - m.entladen[t])
    if start_kwh is None:
        m.zyklus = pyo.Constraint(expr=m.speicher[0] == m.speicher[n])  # Ende = Anfang
    else:
        m.speicher[0].fix(start_kwh)
    if ende_kwh is not None:
        m.ende = pyo.Constraint(expr=m.speicher[n] >= ende_kwh)

    def kosten(m):
        strom = sum(m.preis[t] * m.wp[t] / m.cop[t] for t in m.T)
        gas = sum(GASPREIS / KESSEL_ETA * m.kessel[t] for t in m.T)
        return strom + gas
    m.kosten = pyo.Objective(rule=kosten, sense=pyo.minimize)

    if milp:
        m.kessel_an = pyo.Var(m.T, within=pyo.Binary)
        m.kessel_max = pyo.Constraint(m.T, rule=lambda m, t: m.kessel[t] <= KESSEL_MAX_KW * m.kessel_an[t])
        m.kessel_min = pyo.Constraint(m.T, rule=lambda m, t:
                                      m.kessel[t] >= KESSEL_MIN * KESSEL_MAX_KW * m.kessel_an[t])
    if lastspitze:
        m.spitze = pyo.Var(domain=pyo.NonNegativeReals)    # hoechster Strombezug, kW
        m.spitze_def = pyo.Constraint(m.T, rule=lambda m, t: m.spitze >= m.wp[t] / m.cop[t])
        m.kosten.expr += LEISTUNGSPREIS * m.spitze
    return m


def fahrplan(m, d):
    plan = pd.DataFrame({
        "waermepumpe_kw": [pyo.value(m.wp[t]) for t in m.T],
        "kessel_kw": [pyo.value(m.kessel[t]) for t in m.T],
        "laden_kw": [pyo.value(m.laden[t]) for t in m.T],
        "entladen_kw": [pyo.value(m.entladen[t]) for t in m.T],
        "speicher_kwh": [pyo.value(m.speicher[t]) for t in m.T],
    }, index=d.index)
    plan["strom_kw"] = plan["waermepumpe_kw"] / d["cop"]
    return pd.concat([d, plan], axis=1).clip(lower=-0.0)


def kennzahlen(plan):
    strom = (plan["strom_kw"] * plan["strompreis"]).sum()
    gas = (plan["kessel_kw"] * GASPREIS / KESSEL_ETA).sum()
    return {"kosten": strom + gas, "stromkosten": strom, "gaskosten": gas,
            "strom_kwh": plan["strom_kw"].sum(), "kessel_kwh": plan["kessel_kw"].sum(),
            "wp_kwh": plan["waermepumpe_kw"].sum(), "kesselstunden": int((plan["kessel_kw"] > 0.01).sum()),
            "spitze_kw": plan["strom_kw"].max()}


def optimiere(d, **optionen):
    m = baue_modell(d, **optionen)
    loese(m)
    return fahrplan(m, d), pyo.value(m.kosten)


def referenz(d):
    """Waermegefuehrte Regelung: Waermepumpe zuerst, Kessel fuer den Rest, kein Speicher."""
    plan = d.copy()
    plan["waermepumpe_kw"] = np.minimum(d["bedarf_kw"], WP_MAX_KW)
    plan["kessel_kw"] = d["bedarf_kw"] - plan["waermepumpe_kw"]
    plan["laden_kw"] = plan["entladen_kw"] = plan["speicher_kwh"] = 0.0
    plan["strom_kw"] = plan["waermepumpe_kw"] / d["cop"]
    return plan


def nachrechnen(plan, messung, start_kwh=None):
    """Plan aus der Prognose mit dem gemessenen Bedarf fahren.

    Waermepumpe und Kessel laufen wie geplant. Der Speicher gleicht die
    Abweichung aus. Ist er leer, springt der Kessel zusaetzlich ein; ist er
    voll, wird zuerst der Kessel, dann die Waermepumpe gedrosselt.
    """
    ist = plan.copy()
    s = plan["speicher_kwh"].iloc[-1] if start_kwh is None else start_kwh
    wp, kessel, speicher = [], [], []
    for t in plan.index:
        w, k = plan.at[t, "waermepumpe_kw"], plan.at[t, "kessel_kw"]
        s = (1 - SPEICHER_VERLUST) * s + w + k - messung[t]
        if s < 0:                       # Speicher leer: Kessel springt ein
            k, s = k - s, 0.0
        if s > SPEICHER_KWH:            # Speicher voll: drosseln
            zuviel = s - SPEICHER_KWH
            weniger_k = min(k, zuviel)
            k, zuviel = k - weniger_k, zuviel - weniger_k
            w = w - zuviel
            s = SPEICHER_KWH
        wp.append(w); kessel.append(k); speicher.append(s)
    ist["bedarf_kw"] = messung
    ist["waermepumpe_kw"], ist["kessel_kw"], ist["speicher_kwh"] = wp, kessel, speicher
    ist["strom_kw"] = ist["waermepumpe_kw"] / ist["cop"]
    return ist


def rollierend(prognose, messung, alle=6):
    """Alle 'alle' Stunden neu planen: aktueller Fuellstand, Rest der Prognose."""
    d = prognose.copy()
    s = s_anfang = None
    teile = []
    for beginn in range(0, len(d), alle):
        rest = d.iloc[beginn:]
        m = baue_modell(rest, start_kwh=s, ende_kwh=s_anfang if s is not None else None)
        loese(m)
        plan = fahrplan(m, rest).iloc[:alle]
        if s is None:
            s = pyo.value(m.speicher[0])
            s_anfang = s
        ist = nachrechnen(plan, messung, start_kwh=s)
        s = ist["speicher_kwh"].iloc[-1]
        teile.append(ist)
    return pd.concat(teile), s_anfang


def main():
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--milp", action="store_true")
    parser.add_argument("--lastspitze", action="store_true")
    parser.add_argument("--referenz", action="store_true")
    parser.add_argument("--nachrechnen", action="store_true")
    parser.add_argument("--prognose", choices=["regression", "naiv"], default="regression")
    args = parser.parse_args()

    d = daten_heizzentrale("prognose_kw" if args.prognose == "regression" else "naiv_kw")
    ERGEBNISSE.mkdir(exist_ok=True)
    if args.referenz:
        plan = referenz(d)
        name = "referenz"
    else:
        plan, _ = optimiere(d, milp=args.milp, lastspitze=args.lastspitze)
        name = "optimiert" + ("_milp" if args.milp else "") + ("_lastspitze" if args.lastspitze else "")
    k = kennzahlen(plan)
    print(f"Variante: {name}, Prognose: {args.prognose}")
    print(f"Kosten der Woche:   {k['kosten']:8.2f} Euro (Strom {k['stromkosten']:.2f}, Gas {k['gaskosten']:.2f})")
    print(f"Waerme Waermepumpe: {k['wp_kwh']:8.0f} kWh, Strom {k['strom_kwh']:.0f} kWh")
    print(f"Waerme Kessel:      {k['kessel_kwh']:8.0f} kWh in {k['kesselstunden']} Stunden")
    print(f"Spitze Strombezug:  {k['spitze_kw']:8.1f} kW")
    plan.to_csv(ERGEBNISSE / f"heizzentrale_{name}.csv")

    if args.nachrechnen and not args.referenz:
        messung = daten_heizzentrale("messung_kw")["bedarf_kw"]
        ist = nachrechnen(plan, messung)
        roll, _ = rollierend(d, messung)
        ideal, _ = optimiere(daten_heizzentrale("messung_kw"))
        print("\nBewertung mit dem gemessenen Bedarf:")
        for titel, p in [("Plan aus Prognose, fest", ist), ("rollierend alle 6 h", roll),
                         ("perfekte Voraussicht", ideal)]:
            kk = kennzahlen(p)
            print(f"  {titel:26s} {kk['kosten']:8.2f} Euro, Kessel {kk['kessel_kwh']:6.0f} kWh")
        ist.to_csv(ERGEBNISSE / "heizzentrale_nachgerechnet.csv")


if __name__ == "__main__":
    main()
