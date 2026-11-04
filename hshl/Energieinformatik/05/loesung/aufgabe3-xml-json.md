# Musterlösung zu Aufgabe 3: Vom Modell zum Datenformat

Schauen Sie hier erst hinein, wenn Sie die Aufgabe **selbst** gelöst haben.

Grundlage ist das Modell aus Aufgabe 1 (siehe `aufgabe1-messstellen.md`).

## Beispieldatensatz (Objektmodell)

- Messstelle `DE0001234567890123456789012345678`, Werler Str. 10, 59065 Hamm
  - Zählpunkt `Bezug` in kWh mit zwei Messwerten
  - Zählpunkt `Einspeisung` in kWh mit zwei Messwerten

## XML (verschachtelt)

```xml
<Messstelle messlokationsId="DE0001234567890123456789012345678">
  <Adresse>
    <Strasse>Werler Str. 10</Strasse>
    <PLZ>59065</PLZ>
    <Ort>Hamm</Ort>
  </Adresse>
  <Zaehlpunkt bezeichnung="Bezug" einheit="kWh">
    <Messwert zeitstempel="2026-01-01T00:00:00Z" wert="12345.6"/>
    <Messwert zeitstempel="2026-01-01T00:15:00Z" wert="12345.9"/>
  </Zaehlpunkt>
  <Zaehlpunkt bezeichnung="Einspeisung" einheit="kWh">
    <Messwert zeitstempel="2026-01-01T00:00:00Z" wert="2210.0"/>
    <Messwert zeitstempel="2026-01-01T00:15:00Z" wert="2210.0"/>
  </Zaehlpunkt>
</Messstelle>
```

Ob Werte als Attribute oder als Kindelemente erscheinen, ist eine Frage der Abbildungsregeln. Beides ist richtig, solange es im Dokument einheitlich bleibt. Umlaute vermeidet man in Elementnamen gern, erlaubt sind sie in XML aber.

## JSON (verschachtelt)

```json
{
  "messlokationsId": "DE0001234567890123456789012345678",
  "adresse": {"strasse": "Werler Str. 10", "plz": "59065", "ort": "Hamm"},
  "zaehlpunkte": [
    {"bezeichnung": "Bezug", "einheit": "kWh", "messwerte": [
      {"zeitstempel": "2026-01-01T00:00:00Z", "wert": 12345.6},
      {"zeitstempel": "2026-01-01T00:15:00Z", "wert": 12345.9}
    ]},
    {"bezeichnung": "Einspeisung", "einheit": "kWh", "messwerte": [
      {"zeitstempel": "2026-01-01T00:00:00Z", "wert": 2210.0},
      {"zeitstempel": "2026-01-01T00:15:00Z", "wert": 2210.0}
    ]}
  ]
}
```

Die Multiplizität 1..n bzw. 0..n wird in JSON zu einem **Array**, in XML zu **wiederholten Elementen**. PLZ ist eine Zeichenkette, keine Zahl (führende Nullen).

## Variante: Referenzieren statt Verschachteln (JSON)

```json
{
  "messstellen": [
    {"messlokationsId": "DE0001234567890123456789012345678",
     "adresse": {"strasse": "Werler Str. 10", "plz": "59065", "ort": "Hamm"},
     "zaehlpunkte": ["ZP-1", "ZP-2"]}
  ],
  "zaehlpunkte": [
    {"id": "ZP-1", "bezeichnung": "Bezug", "einheit": "kWh"},
    {"id": "ZP-2", "bezeichnung": "Einspeisung", "einheit": "kWh"}
  ],
  "messwerte": [
    {"zaehlpunkt": "ZP-1", "zeitstempel": "2026-01-01T00:00:00Z", "wert": 12345.6}
  ]
}
```

Für das Referenzieren braucht der Zählpunkt eine **eindeutige Kennung**. Das Modell aus Aufgabe 1 hat dafür nur die Bezeichnung, die nicht unbedingt eindeutig ist. Das ist ein guter Anlass, das Modell um eine ID zu ergänzen.

## Nesting oder Referenzieren?

| Kriterium | Verschachteln | Referenzieren |
|---|---|---|
| Redundanz | keine, solange jeder Zählpunkt nur an einer Stelle vorkommt; sonst Kopien | keine, jeder Zählpunkt steht genau einmal im Dokument |
| Konsistenz bei Änderungen | Kopien müssen alle geändert werden, sobald es welche gibt | Änderung an genau einer Stelle |
| Wiederverwendbarkeit | andere Systeme müssen die Messstelle mitlesen, um den Zählpunkt zu finden | andere Systeme referenzieren die ID direkt |
| Lesbarkeit | Struktur des Modells direkt sichtbar | Zusammenhänge erst über IDs auflösen |

**Begründete Entscheidung für das Beispiel:** Verschachteln. Ein Zählpunkt gehört laut Modell zu **genau einer** Messstelle, es entstehen also keine Kopien. Sobald Zählpunkte auch aus anderen Systemen oder Nachrichten angesprochen werden, sollten sie eine eigene ID bekommen. Dann sind beide Varianten kombinierbar: verschachtelt übertragen, über die ID referenzierbar.

Eine andere Entscheidung ist ebenfalls richtig, wenn sie mit den Kriterien begründet wird.
