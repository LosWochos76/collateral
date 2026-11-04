"""Musterloesung zu Folie 7 der Uebung: Kesselstarts in der Heizzentrale.

Erst ansehen, wenn Sie die Aufgabe selbst geloest haben.

Die Heizzentrale aus der Vorlesung mit Mindestlast (--milp) wird um eine
Startvariable z_t ergaenzt (Vorlesung "Logik: Kesselstarts erkennen"):
    a[t-1] + (1 - a[t]) + z[t] >= 1
Jeder Start kostet STARTKOSTEN Euro im Ziel.

Start (im Ordner 15/loesung):
  ../../14/optimierung/.venv/bin/python heizzentrale_starts.py
  ../../14/optimierung/.venv/bin/python heizzentrale_starts.py --startkosten 0
Unter Windows: ..\\..\\14\\optimierung\\.venv\\Scripts\\python.exe heizzentrale_starts.py
"""
import argparse
import sys
from pathlib import Path

OPTIMIERUNG = Path(__file__).resolve().parents[2] / "14" / "optimierung"
sys.path.insert(0, str(OPTIMIERUNG))

import pyomo.environ as pyo  # noqa: E402

import heizzentrale as hz  # noqa: E402
from optimierung import daten_heizzentrale, loese  # noqa: E402


def baue_modell(d, startkosten=1.0):
    m = hz.baue_modell(d, milp=True)          # Heizzentrale mit Binaervariable kessel_an

    # Aufgabe 7: Startvariable, Logik-Ungleichung, Startkosten
    m.kessel_start = pyo.Var(m.T, within=pyo.Binary)

    def anfahrt(m, t):
        if t == 1:                            # vor der ersten Stunde gibt es kein a[t-1]
            return pyo.Constraint.Skip
        return m.kessel_an[t - 1] + (1 - m.kessel_an[t]) + m.kessel_start[t] >= 1
    m.anfahrt = pyo.Constraint(m.T, rule=anfahrt)

    m.kosten.expr += startkosten * sum(m.kessel_start[t] for t in m.T)
    return m


def main():
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--startkosten", type=float, default=1.0, help="Euro je Kesselstart (Vorgabe 1)")
    args = parser.parse_args()

    d = daten_heizzentrale()
    m = baue_modell(d, args.startkosten)
    loese(m)
    plan = hz.fahrplan(m, d)
    k = hz.kennzahlen(plan)
    z = round(sum(pyo.value(m.kessel_start[t]) for t in m.T))
    an = [round(pyo.value(m.kessel_an[t])) for t in m.T]
    echte_starts = sum(1 for i in range(1, len(an)) if an[i] == 1 and an[i - 1] == 0)
    print(f"Startkosten:          {args.startkosten:.2f} Euro je Start")
    print(f"Zielfunktion:         {pyo.value(m.kosten):8.2f} Euro")
    print(f"Energiekosten:        {k['kosten']:8.2f} Euro (Strom und Gas, ohne Startkosten)")
    print(f"Kessel:               {k['kessel_kwh']:8.0f} kWh in {k['kesselstunden']} Stunden")
    print(f"Summe der z_t:        {z:8d}")
    print(f"echte Starts (0 -> 1): {echte_starts:7d}")


if __name__ == "__main__":
    main()
