# Musterlösung zu Aufgabe 6: EDIFACT

Schauen Sie hier erst hinein, wenn Sie die Aufgabe **selbst** gelöst haben.

## 1. Nachrichtentyp

**MSCONS** (*Metered Services Consumption Report*): Übermittlung von Zählerständen und Lastgängen.

Nicht passend sind z. B. UTILMD (Stammdaten), UTILTS (Zeitreihen für die Bilanzkreisabrechnung) oder INVOIC (Rechnung).

## 2. Segment

Vereinfacht nach dem Schema aus der Vorlesung (Segmentkennung, Datenelemente durch `+`, zusammengesetzte Elemente durch `:`, Abschluss mit `'`):

```text
LOC+172+Z-001'
QTY+220:12.4:KWH'
DTM+163:202601010000?+00:303'
```

| Segment | Bedeutung |
|---|---|
| `LOC+172+Z-001'` | Lokation: Zählpunkt Z-001 |
| `QTY+220:12.4:KWH'` | Menge: Wert 12.4 in kWh |
| `DTM+163:202601010000?+00:303'` | Zeitpunkt 2026-01-01 00:00 in UTC (Offset `+00`) |

Das eigentliche Segment für den Wert ist `QTY`. Die anderen beiden ordnen ihn einem Zählpunkt und einem Zeitpunkt zu, wie in der vollständigen MSCONS-Nachricht der Vorlesung.

Im Datum steht `?+`: Das `?` ist das Freigabezeichen. Es sorgt dafür, dass das `+` im Zeitzonen-Offset nicht als Trennzeichen gelesen wird. Wer das in der Übung weglässt, hat den Kern trotzdem verstanden.

Das Beispiel ist bewusst **nicht normkonform**: Eine echte Zählpunktbezeichnung hat 33 Stellen, es fehlen Kopf- und Endsegmente, und die Qualifier hängen von der Anwendungshilfe der jeweiligen MSCONS-Version ab.
