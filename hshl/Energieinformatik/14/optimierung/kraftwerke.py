"""Lehrbeispiel A: Zwei Kraftwerke liefern 300 MWh in einer Stunde.

Gesucht ist die Leistung von Kohle- und Gaskraftwerk, die den Gewinn
maximiert, ohne mehr als 230 t CO2 auszustossen.
"""
import pyomo.environ as pyo

m = pyo.ConcreteModel()

# Entscheidungsvariablen: Leistung in MW, je Kraftwerk hoechstens 200 MW
m.kohle = pyo.Var(domain=pyo.NonNegativeReals, bounds=(0, 200))
m.gas = pyo.Var(domain=pyo.NonNegativeReals, bounds=(0, 200))

# Zielfunktion: Gewinn in Euro (10 bzw. 2 Euro je MWh)
m.gewinn = pyo.Objective(expr=10 * m.kohle + 2 * m.gas, sense=pyo.maximize)

# Nebenbedingungen
m.bedarf = pyo.Constraint(expr=m.kohle + m.gas == 300)
m.co2 = pyo.Constraint(expr=1.0 * m.kohle + 0.575 * m.gas <= 230)

# Dualwerte (Schattenpreise) der Nebenbedingungen vom Solver abholen
m.dual = pyo.Suffix(direction=pyo.Suffix.IMPORT)


def main():
    ergebnis = pyo.SolverFactory("highs").solve(m)
    print("Status:", ergebnis.solver.termination_condition)
    print(f"Kohle:  {pyo.value(m.kohle):7.2f} MW")
    print(f"Gas:    {pyo.value(m.gas):7.2f} MW")
    print(f"Gewinn: {pyo.value(m.gewinn):7.2f} Euro")
    print(f"Dualwert CO2-Grenze: {m.dual[m.co2]:.2f} Euro je t")
    print(f"Dualwert Bedarf:     {m.dual[m.bedarf]:.2f} Euro je MWh")


if __name__ == "__main__":
    main()
