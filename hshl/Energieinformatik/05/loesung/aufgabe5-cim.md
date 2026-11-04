# Musterlösung zu Aufgabe 5: CIM

Schauen Sie hier erst hinein, wenn Sie die Aufgabe **selbst** gelöst haben.

Grundlage: Konnektivitätsdiagramm aus Kapitel 4 (McMorran 2007, S. 17).

## Teil 1: Vererbung nachvollziehen

| Attribut | definiert in |
|---|---|
| ampRating | LoadBreakSwitch (eigenes Attribut) |
| normalOpen | Switch |
| Phases | ConductingEquipment |

Equipment, PowerSystemResource, IdentifiedObject und ProtectedSwitch zeigen im Diagramm keine Attribute.

Ein Breaker besitzt ebenfalls normalOpen und Phases (gleiche Oberklassen) sowie ein eigenes ampRating; zusätzlich inTransitTime, das der LoadBreakSwitch nicht hat.

## Teil 2: Generalisierung und geerbte Assoziationen

- ampRating gehört in die gemeinsame Oberklasse ProtectedSwitch (vertretbar auch: Switch).
- Bei ProtectedSwitch ändert sich für andere Klassen im Diagramm nichts; bei Switch bekäme auch ein einfacher Switch das Attribut.
- Hintergrund: Im aktuellen CIM (CIM17) heißt das Attribut ratedCurrent und ist tatsächlich in Switch definiert; LoadBreakSwitch hat dort kein eigenes Attribut mehr.
- Terminal: Ja. Ein LoadBreakSwitch ist ein ConductingEquipment und erbt damit auch dessen Assoziation zu Terminal (0..n). Über die Terminals ist er mit ConnectivityNodes verbunden. Das gilt für alle Unterklassen, z. B. auch ACLineSegment.

## Teil 3: Smart Meter einordnen

Offene Modellierungsfrage; zwei vertretbare Varianten:

1. Erben von ConductingEquipment: Der Zähler bekommt automatisch Terminals und hängt direkt in der Topologie. Nachteil: Ein Zähler ist für die Netzberechnung kein relevantes Betriebsmittel.
2. Erben von IdentifiedObject (bzw. PowerSystemResource) und Assoziation zu einem bestehenden Anlagenteil, z. B. dem Anschluss eines Verbrauchers. Trennt Messgerät und Netztopologie.

Mögliche Attribute: Zählernummer, Messlokations-/Zählpunkt-ID, Kommunikationsadresse, Ausleseintervall, Zeitpunkt der letzten Auslesung, OBIS-Kennzahlen der erfassten Größen.

Das echte CIM folgt Variante 2: Meter → EndDevice → AssetContainer → Asset → IdentifiedObject (Paket Metering, IEC 61968-9). Ein EndDevice gehört zu höchstens einem UsagePoint (0..1), ein UsagePoint ist mit beliebig vielen Equipment-Objekten verbunden (0..*). Meter-eigene Assoziationen sind z. B. MeterReadings.

Quellen (geprüft am 2026-11-04): CIM17-Schema der Netbeheer Nederland, Klassen Meter und LoadBreakSwitch:
- https://modellen.netbeheernederland.nl/tc57cim/cim17/schema/class/Meter.html
- https://modellen.netbeheernederland.nl/tc57cim/cim17/schema/class/LoadBreakSwitch.html
