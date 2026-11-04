# Folie 7: Kesselstarts ergänzen

Lauffähig in `heizzentrale_starts.py`. Der Kern im Block `if milp:`:

```python
m.kessel_start = pyo.Var(m.T, within=pyo.Binary)

def anfahrt(m, t):
    if t == 1:                     # vor der ersten Stunde gibt es kein a[t-1]
        return pyo.Constraint.Skip
    return m.kessel_an[t - 1] + (1 - m.kessel_an[t]) + m.kessel_start[t] >= 1
m.anfahrt = pyo.Constraint(m.T, rule=anfahrt)

m.kosten.expr += 1.0 * sum(m.kessel_start[t] for t in m.T)
```

| Startkosten | echte Starts | Summe der $z_t$ | Energiekosten (Strom und Gas) | Zielfunktion |
|---|---|---|---|---|
| ohne Startvariable (`--milp`) | 6 | – | 594,14 Euro | 594,14 Euro |
| 0 Euro | 6 | 167 | 594,14 Euro | 594,14 Euro |
| 1 Euro | 2 | 2 | 594,26 Euro | 596,26 Euro |
| 5 Euro | 1 | 1 | 595,71 Euro | 600,71 Euro |

- **1 Euro je Start:** Der Kessel startet nur noch 2-mal statt 6-mal. Die Energie wird dafür nur 12 Cent teurer. Er läuft länger am Stück (70 statt 63 Stunden) und lädt den Speicher.
- **0 Euro:** Die Ungleichung erzwingt $z_t = 1$ nur beim Einschalten. In allen anderen Stunden ist $z_t$ frei (Wahrheitstabelle der Vorlesung), und ohne Kosten setzt der Solver es beliebig, hier fast überall auf 1. $z_t$ zählt die Starts also nur, wenn es im Ziel etwas kostet.
- **5 Euro:** nur noch ein einziger Start in der Woche. Die Energiekosten steigen um 1,57 Euro gegenüber dem MILP ohne Startkosten.
- Echte Starts zählt man aus `kessel_an` (Wechsel von 0 auf 1), nicht aus $z_t$.
