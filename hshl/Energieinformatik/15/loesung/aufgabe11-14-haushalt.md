# Folie 11 bis 14: Das Haushaltsmodell in Pyomo

Lauffähig in `haushalt_loesung.py`. Das Modell steht in der Funktion `baue_modell(d, binaer, mindestladung, heimspeicher)`, die Notation in `aufgabe10-modell.md`.

## Ergebnisse

| Variante | Befehl | Kosten der Woche | geladen | davon aus PV-Überschuss | Stunden mit Bezug und Einspeisung |
|---|---|---|---|---|---|
| Referenz "sofort laden" | `./haushalt.sh` | 13,36 Euro | 73,3 kWh | 19,4 kWh | 0 |
| LP | `haushalt_loesung.py` | −6,45 Euro | 85,2 kWh | 39,1 kWh | 4 |
| MILP, Bezug oder Einspeisung | `--binaer` | 8,34 Euro | 86,7 kWh | 31,3 kWh | 0 |
| MILP mit Mindestladeleistung | `--binaer --mindestladung` | 8,66 Euro | 86,1 kWh | 30,7 kWh | 0 |
| MILP mit Heimspeicher | `--binaer --heimspeicher` | −10,10 Euro | 86,7 kWh | 31,3 kWh | 0 |

Daten: echte Day-Ahead-Preise und deutsche PV-Einspeisung vom 27.04. bis 03.05.2026 (Energy-Charts), PV 445 kWh in der Woche, Haushaltslast synthetisch mit 63 kWh in der Woche (3.300 kWh im Jahr). Jeder Lauf dauert unter einer Sekunde.

## Folie 11: Das Modell in Pyomo

- HiGHS meldet `optimal`. Das LP kostet −6,45 Euro, also verdient der Haushalt Geld. Gegenüber der Referenz sind das 19,81 Euro weniger.
- Das Auto lädt fast nur mittags, wenn PV-Überschuss da ist oder der Preis negativ ist.

## Folie 12: Das Ergebnis prüfen

- Am **1. Mai von 12 bis 15 Uhr** (Ortszeit) bezieht das LP 25 kW aus dem Netz und speist gleichzeitig 25 kW ein. Der Day-Ahead-Preis liegt dort bei bis zu −499 Euro/MWh, der Bezugspreis mit Aufschlag also bei bis zu −30 ct/kWh. Jede bezogene kWh bringt Geld, die Einspeisung kostet nichts (Vergütung 0 bei negativem Preis). Das Modell verdient an Strom, den es sofort wieder abgibt.
- In Wirklichkeit geht das nicht: Ein Zähler misst je Augenblick nur eine Richtung, und kein Netzbetreiber zahlt dafür. Wie beim Pumpspeicher der Vorlesung nutzt der Solver aus, was nicht verboten ist.
- **Vertiefung:** Mit der Binärvariablen "Bezug oder Einspeisung" kostet die Woche **8,34 Euro**, das sind **38 %** weniger als die Referenz. Am 1. Mai lädt das Auto um 13 und 14 Uhr mit vollen 11 kW und wird für den Bezug bezahlt.

## Folie 13: Mindestladeleistung der Wallbox

- Ohne Mindestladeleistung lädt das Auto im MILP in 4 von 12 Ladestunden mit weniger als 4,1 kW (bis 1,1 kW herunter), um PV-Reste zu nutzen.
- Mit $4{,}1\, w_t \le l_t \le 11\, w_t$ kostet die Woche **8,66 Euro**, also 32 Cent mehr. Es gibt keine Ladestunde unter 4,1 kW mehr. Am 1. Mai lädt das Auto um 13 und 14 Uhr mit 11 kW und um 15 Uhr mit genau 4,1 kW.
- Dieselbe Formel wie beim Kessel: $P^{min} a \le x \le P^{max} a$.
- **Annahme:** Die Wallbox ist dreiphasig angeschlossen und schaltet nicht um. Der Ladestandard (IEC 61851-1, Control-Pilot-Signal) verlangt mindestens 6 A je Phase, das ergibt 6 A · 3 · 230 V ≈ 4,1 kW. Viele Wallboxen und Autos können auf eine Phase umschalten und laden dann schon ab etwa 1,4 kW (z. B. 2,3 kW bei 10 A).
- **Idee zur Vertiefung (nicht gerechnet):** zwei Betriebsarten mit je einer Binärvariablen, einphasig $1{,}4\, u_t \le l_t^{(1)} \le 3{,}7\, u_t$ und dreiphasig $4{,}1\, w_t \le l_t^{(3)} \le 11\, w_t$, dazu $u_t + w_t \le 1$ und $l_t = l_t^{(1)} + l_t^{(3)}$.

## Folie 14: Heimspeicher (Herausforderung)

- Neue Variablen: Laden, Entladen und Stand des Speichers je Stunde. Neue Nebenbedingungen: Speicherbilanz mit 95 % je Richtung, Grenzen, Ende gleich Anfang. Die Strombilanz bekommt zwei neue Terme.
- Mit `--binaer --heimspeicher` kostet die Woche −10,10 Euro, der Speicher spart also **18,44 Euro** in dieser Woche.
- 6.000 Euro / 18,44 Euro ≈ 325 Wochen ≈ 6 Jahre. Die Rechnung ist zu optimistisch: Die Woche ist sehr sonnig und hat an jedem Tag negative Preise, am 1. Mai bis −499 Euro/MWh. Der Speicher verdient vor allem daran, an diesen Stunden Netzstrom zu laden. Außerdem kennt das Modell Preise und PV der ganzen Woche im Voraus (vollkommene Voraussicht), Alterung und Wechselrichterverluste fehlen.

## Folie 15: Andere Woche und oemof.solph (Herausforderung)

Keine Musterlösung im Material. Erwartung: Mit Januarpreisen und PV auf 20 % gibt es kaum negative Preise und wenig PV-Überschuss. Die Ersparnis kommt dann nur noch aus dem Verschieben in billige Nachtstunden und ist deutlich kleiner. Über ein Jahr ist die Ersparnis also viel geringer, als die Mai-Woche vermuten lässt. Mit oemof.solph muss für das LP (ohne Binärvariable) dasselbe Ergebnis herauskommen wie mit Pyomo.
