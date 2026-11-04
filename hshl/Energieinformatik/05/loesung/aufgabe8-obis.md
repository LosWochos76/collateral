# Musterlösung zu Aufgabe 8: OBIS-Kennzahlen

Schauen Sie hier erst hinein, wenn Sie die Aufgabe **selbst** gelöst haben.

## 1. Kennzahlensystem

**OBIS**, das *Object Identification System*. Es ist in der Normenreihe IEC 62056 (DLMS/COSEM) festgelegt und kennzeichnet Messgrößen in Zählern eindeutig. In der deutschen Marktkommunikation regelt eine eigene EDI@Energy-Codeliste, welche OBIS-Kennzahlen verwendet werden.

## 2. Bedeutung der Beispielkennungen

Aufbau `A-B:C.D.E`:

| Teil | Bedeutung | Wert im Beispiel |
|---|---|---|
| A | Medium | `1` = Elektrizität |
| B | Kanal | `0` = kein bestimmter Kanal |
| C | Messgröße | `1` = Wirkleistung Bezug (+P), `2` = Wirkleistung Lieferung (−P) |
| D | Messart | `8` = Zeitintegral, also Zählerstand (Energie) |
| E | Tarif | `0` = Summe über alle Tarife (`1` = HT, `2` = NT) |

- `1-0:1.8.0`: **Zählerstand der bezogenen Wirkenergie** (Bezug aus dem Netz), Summe aller Tarife, in kWh.
- `1-0:2.8.0`: **Zählerstand der gelieferten Wirkenergie** (Einspeisung ins Netz, z. B. von einer PV-Anlage), Summe aller Tarife, in kWh.

Im MSCONS-Beispiel der Vorlesung steht `1-1:1.8.1`: Wirkarbeit Bezug im Tarif 1 (HT).

## 3. Bezugswirkleistung (Momentanwert)

**`1-0:1.7.0`**: Messgröße 1 (Wirkleistung Bezug) mit Messart **7 = Momentanwert** statt 8 = Zeitintegral, Einheit W bzw. kW.

Ein möglicher Lösungsweg: Bei `1.8.0` steht C für die Messgröße und D für die Messart. Man sucht also die Messart „Momentanwert“ und behält C = 1. Ein LLM nennt das meist direkt, sollte aber mit einer zweiten Quelle geprüft werden, z. B. mit einer Herstellerdokumentation eines Smart Meters.

Auch richtig: Viele moderne Messeinrichtungen zeigen zusätzlich `1-0:16.7.0`, die **saldierte** Momentanleistung (Bezug minus Lieferung). Für reine Bezugsleistung ist `1.7.0` die passende Kennzahl.
