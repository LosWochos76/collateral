# Folie 3: Das Kraftwerksmodell befragen

## Basis: CO₂-Grenze 250 t

- Ausgangslage (230 t): Kohle 135,29 MW, Gas 164,71 MW, Gewinn 1682,35 Euro, Dualwert der CO₂-Grenze 18,82 Euro je t.
- **Vorhersage mit dem Dualwert:** 20 t mehr Budget bringen 20 · 18,82 = 376,40 Euro mehr Gewinn, also 2058,75 Euro (Rundung).
- **Gerechnet:** Gewinn 2058,82 Euro, Kohle 182,35 MW, Gas 117,65 MW. Die Vorhersage stimmt: Der Dualwert gilt, solange dieselben Nebenbedingungen bindend sind.

## Basis: CO₂-Grenze 200 t

- Der Solver findet keine zulässige Lösung (`infeasible`; das Skript bricht mit einer Fehlermeldung wie `NoFeasibleSolutionError` ab).
- Grund: 300 MWh müssen geliefert werden, jedes Kraftwerk höchstens 200 MW. Am wenigsten CO₂ entsteht mit 200 MW Gas und 100 MW Kohle: 0,575 · 200 + 1,0 · 100 = **215 t**. Weniger geht nicht.

## Vertiefung: Wie weit gilt der Dualwert?

- Mit steigender Grenze fährt das Modell mehr Kohle. Bei Kohle = 200 MW (Obergrenze) und Gas = 100 MW entstehen 200 + 57,5 = **257,5 t**. Bis dahin gilt der Dualwert von 18,82 Euro je t.
- Mit 270 t: Kohle 200 MW, Gas 100 MW, Gewinn 2200,00 Euro, Dualwert der CO₂-Grenze **0**. Die CO₂-Grenze bindet nicht mehr, jetzt begrenzt die **Höchstleistung des Kohlekraftwerks**. Mehr CO₂-Budget bringt nichts.
