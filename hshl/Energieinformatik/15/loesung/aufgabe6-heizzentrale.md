# Folie 6: Die Heizzentrale rechnen

| Variante | Befehl | Kosten | Kesselwärme | Kesselstunden |
|---|---|---|---|---|
| wärmegeführte Regelung | `./heizzentrale.sh --referenz` | 622,73 Euro | 61 kWh | 29 |
| LP | `./heizzentrale.sh` | 594,13 Euro | 2323 kWh | 69 |
| MILP mit Mindestlast | `./heizzentrale.sh --milp` | 594,14 Euro | 2325 kWh | 63 |

- Die Optimierung spart gegenüber der wärmegeführten Regelung 28,60 Euro (4,6 %) in der Woche. Sie nutzt den Kessel bewusst viel stärker, weil Wärme aus Gas bei niedrigem COP günstiger ist.
- Die Mindestlast ändert die Kosten um 1 Cent: Im LP läuft der Kessel nur in 7 Stunden unter 18 kW (bis 0,5 kW). Diese kleinen Mengen übernimmt im MILP die Wärmepumpe oder der Speicher, oder der Kessel läuft eine Stunde mit 18 kW und lädt den Speicher. An der Wirtschaftlichkeit ändert sich fast nichts, am Betrieb (Takten des Brenners) schon.
