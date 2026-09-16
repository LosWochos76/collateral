# Registerplan des simulierten Smart Meters

Alle Messwerte liegen in **Input Registers** und werden mit Funktionscode
`0x04` gelesen. Ein Register ist 16 Bit breit. Mehrwortwerte verwenden
Big-Endian-Wortreihenfolge: zuerst das High Word, dann das Low Word.

Die Spalte „PDU-Adresse“ ist die tatsaechlich im Telegramm uebertragene,
nullbasierte Adresse. Die 3xxxx-Referenz ist nur die traditionelle
Dokumentationsschreibweise.

| 3xxxx-Referenz | PDU-Adresse | Laenge | Datentyp | Faktor | Einheit | Bedeutung |
|---:|---:|---:|---|---:|---|---|
| 30001 | 0 | 1 | uint16 | 0,1 | V | Effektivspannung L1 |
| 30002 | 1 | 1 | uint16 | 0,01 | A | Strom L1 |
| 30003 | 2 | 2 | int32 | 1 | W | Wirkleistung; positiv Bezug, negativ Einspeisung |
| 30005 | 4 | 2 | int32 | 1 | var | Blindleistung |
| 30007 | 6 | 2 | uint32 | 1 | Wh | bezogene Energie, monoton steigend |
| 30009 | 8 | 2 | uint32 | 1 | Wh | eingespeiste Energie, monoton steigend |
| 30011 | 10 | 1 | uint16 | 0,01 | Hz | Netzfrequenz |
| 30012 | 11 | 1 | int16 | 0,001 | - | Leistungsfaktor |
| 30013 | 12 | 1 | uint16 | - | Bitfeld | Status |

## Statusregister 30013

| Bit | Maske | Bedeutung bei 1 |
|---:|---:|---|
| 0 | `0x0001` | Netz vorhanden |
| 1 | `0x0002` | Energiebezug |
| 2 | `0x0004` | Energieeinspeisung |
| 3 | `0x0008` | Ueberspannungswarnung |
| 4 | `0x0010` | Kommunikations-Selbsttest aktiv |

Nicht aufgefuehrte Bits sind reserviert und muessen beim Lesen ignoriert
werden.

## Beispiel

Antwortregister fuer die Wirkleistung:

```text
FFFF F830
```

Als `int32` interpretiert ergibt das `-2000 W`, also eine Einspeisung von
2 kW. Eine Interpretation als zwei getrennte positive Zahlen oder als
`uint32` waere fachlich falsch.
