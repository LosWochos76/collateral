# Folie 10: Das Haushaltsmodell auf Papier

## Bezeichnungen

| Zeichen | Bedeutung | Art |
|---|---|---|
| $T = \{0, \dots, 167\}$ | Stunden der Woche | Indexmenge |
| $p_t$, $v_t$ | Bezugspreis und Vergütung in Euro/kWh | Parameter |
| $P_t$, $L_t$ | PV-Erzeugung und Haushaltslast in kW | Parameter |
| $h_t$ | 1, wenn das Auto zu Hause ist | Parameter |
| $a_t$ | 1 in der Abfahrtsstunde | Parameter |
| $f_t$ | Verbrauch der Fahrt in kWh (in der Ankunftsstunde) | Parameter |
| $b_t$, $e_t$ | Netzbezug und Einspeisung in kW | Variable |
| $l_t$ | Ladeleistung des Autos in kW | Variable |
| $s_t$ | Ladestand am Ende von Stunde $t$ in kWh | Variable |

## Zielfunktion

Die Stromkosten der Woche, Bezug minus Vergütung:

$$\min \sum_{t \in T} \left( p_t\, b_t - v_t\, e_t \right)$$

## Nebenbedingungen (für alle $t \in T$)

- **Strombilanz des Hauses** (wie die Wärmebilanz der Heizzentrale): $P_t + b_t = L_t + l_t + e_t$
- **Akkubilanz** (wie die Speicherbilanz des Pumpspeichers): $s_t = s_{t-1} + 0{,}9\, l_t - f_t$ mit $s_{-1} = 30$
- **Grenzen:** $0 \le b_t \le 25$, $0 \le e_t \le 25$, $0 \le s_t \le 60$
- **Laden nur zu Hause:** $0 \le l_t \le 11\, h_t$
- **Ladestand bei Abfahrt:** $s_{t-1} \ge 48$, wenn $a_t = 1$
- **Ende der Woche:** $s_{167} \ge 48$

Ein Zeitschritt ist eine Stunde, deshalb sind kW und kWh je Stunde zahlengleich.

## Erweiterungen

- **Folie 12, Bezug oder Einspeisung:** Binärvariable $y_t$, $b_t \le 25\, y_t$, $e_t \le 25\,(1 - y_t)$ (wie Pumpe oder Turbine).
- **Folie 13, Mindestladeleistung:** Binärvariable $w_t$, $4{,}1\, w_t \le l_t \le 11\, w_t$ (Schwellwert wie die Mindestlast des Kessels). Die Grenze $l_t \le 11\, h_t$ bleibt bestehen. Annahme: dreiphasig angeschlossene Wallbox ohne Phasenumschaltung, 6 A je Phase: 6 A · 3 · 230 V ≈ 4,1 kW.
- **Folie 14, Heimspeicher:** Laden $c_t \le 5$, Entladen $d_t \le 5$, Stand $0 \le q_t \le 10$, $q_t = q_{t-1} + 0{,}95\, c_t - d_t / 0{,}95$, Ende gleich Anfang. In der Strombilanz kommt $d_t$ auf die Seite der Quellen, $c_t$ auf die Seite der Verbraucher.
