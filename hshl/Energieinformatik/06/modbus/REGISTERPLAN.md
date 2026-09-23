# Registerplan der Wetterstation

Dieses Datenmodell entspricht der **echten Wetterstation auf dem Dach der
HSHL**. Der simulierte Server bildet exakt diese Register nach -- inklusive
ihrer (etwas ungewöhnlichen) Wortreihenfolge.

Alle Messwerte liegen in **Holding Registers** und werden mit Funktionscode
`0x03` gelesen. Ein Register ist 16 Bit breit. Jeder Messwert belegt zwei
aufeinanderfolgende Register und ist als **IEEE-754-Float (32 Bit)** kodiert.

**Wortreihenfolge:** Die zwei höherwertigen Bytes eines Messwerts liegen im
Register mit der **größeren** Adresse -- also *nicht* wie beim klassischen
Big-Endian-Wort zuerst das High Word. Wer das nicht beachtet, erhält
syntaktisch gültige, aber fachlich falsche Werte.

Die Spalte „PDU-Adresse" ist die tatsächlich im Telegramm übertragene,
nullbasierte Adresse. Die 4xxxx-Referenz ist die beim Hersteller übliche
Dokumentationsschreibweise (PDU-Adresse = Referenz − 40001).

| 4xxxx-Referenz | PDU-Adresse (low/high) | Variable | Einheit | Bedeutung |
|---:|---:|---|---|---|
| 40001 | 0/1 | `ghi_final_min` | W/m² | GHI, Minutenmittel der Vorminute; minütlich aktualisiert |
| 40003 | 2/3 | `dhi_final_min` | W/m² | DHI, Minutenmittel der Vorminute; minütlich aktualisiert |
| 40005 | 4/5 | `dni_final_min` | W/m² | DNI, Minutenmittel der Vorminute; minütlich aktualisiert |
| 40007 | 6/7 | `cmp_ghi` | W/m² | GHI des CMP10-Pyranometers; sekündlich aktualisiert |
| 40009 | 8/9 | `sensor_temp` | °C | Gehäusetemperatur des RSP-Sensors; sekündlich aktualisiert |
| 40011 | 10/11 | `tair` | °C | Lufttemperatur; alle 10 s aktualisiert |
| 40013 | 12/13 | `rh` | % | relative Luftfeuchte; alle 10 s aktualisiert |
| 40015 | 14/15 | `bp` | hPa | Luftdruck; alle 10 min aktualisiert |
| 40017 | 16/17 | `ws` | m/s | Windgeschwindigkeit; sekündlich aktualisiert |
| 40019 | 18/19 | `wsgust` | m/s | Windböe; sekündlich aktualisiert |
| 40021 | 20/21 | `wd` | ° | Windrichtung; sekündlich aktualisiert |
| 40023 | 22/23 | `rain_mm` | mm | Niederschlag; sekündlich aktualisiert |
| 40025 | 24/25 | `logger_voltage` | V | Versorgungsspannung des Loggers; sekündlich aktualisiert |
| 40027 | 26/27 | `logger_temp_c` | °C | Innentemperatur des Loggers; sekündlich aktualisiert |
| 40029 | 28/29 | `ic` | Anzahl | Reinigungs-Ereigniszähler; minütlich aktualisiert |

Insgesamt also 15 Messwerte in 30 Registern (PDU-Adressen 0 bis 29).

## Beispiel

Antwortregister für `tair` (PDU-Adresse 10/11):

```text
Adresse 10 (low):  00 00
Adresse 11 (high): 41 60
```

Zusammengesetzt zum 32-Bit-Wort `0x41600000` (High Word aus Adresse 11 zuerst!)
und als IEEE-754-Float interpretiert ergibt das `14.0 °C`. Würde man
stattdessen Adresse 10 als High Word behandeln (die "übliche" Reihenfolge),
käme ein unsinniger Wert heraus.

## `ic` ist ein Zähler, kein Messwert

`ic` ändert sich nur gelegentlich (ein Reinigungsereignis wird geloggt) und
bleibt sonst über viele Abfragen hinweg konstant -- anders als alle übrigen,
sich laufend ändernden Messgrößen. Das ist beabsichtigt und entspricht dem
Verhalten des realen Geräts.
