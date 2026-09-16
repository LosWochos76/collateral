# Loesungshinweise fuer Lehrende

## Erwartete Kernergebnisse

- Modbus TCP kapselt die PDU in einen MBAP-Header; es gibt bei TCP keine CRC im
  Modbus-Frame.
- Input Register werden mit Funktionscode 4 gelesen. Funktionscode 3 adressiert
  Holding Registers und liefert in diesem Simulator Exception Code 2.
- Herstellerreferenz 30001 entspricht PDU-Adresse 0. Die fuehrende 3 bezeichnet
  den Registertyp und wird nicht uebertragen.
- Zwei 16-Bit-Worte werden mit `(high << 16) | low` kombiniert. Danach muss bei
  `int32` gegebenenfalls das Zweierkomplement angewandt werden.
- Negative Wirkleistung beschreibt Einspeisung. Import- und Exportenergie sind
  getrennte, monotone Zaehler.

## Fehlerklassifikation

| Versuch | Erwartetes Ergebnis | Kategorie |
|---|---|---|
| falscher Port | Connection refused/Timeout | Transportfehler |
| falsche Unit-ID | Exception `0x0B` | Modbus-Exception |
| Adresse 100 | Exception `0x02` | Modbus-Exception |
| Function 3 | Exception `0x01` (Illegal Function) | Modbus-Exception |
| Anzahl 0 | Exception `0x03` | Modbus-Exception |
| falsche Wortfolge | syntaktisch gueltig, unrealistischer Wert | Dekodierfehler |

## Diskussionsimpulse

- Netzwerksegmentierung, Firewall/Allowlist, VPN bzw. gesicherte Gateways,
  Monitoring und minimale Schreibrechte sind wichtiger als ein Passwort, das
  klassisches Modbus TCP selbst gar nicht vorsieht.
- Ein neutrales Informationsmodell benoetigt Datentyp, Einheit, Skalierung,
  Vorzeichen, Wortreihenfolge, Qualitaet und Zeitbezug.
- Publish/Subscribe passt besser zu vielen Empfaengern, Ereignissen und
  asynchronen Aenderungen; Polling ist einfach und vorhersehbar, erzeugt aber
  auch ohne Wertveraenderung Verkehr.
- Veraltete Werte koennen durch ein zusaetzliches Zeit-/Sequenzregister oder
  durch Zeitstempel des erfassenden Clients erkannt werden.

## Bewertungsrubrik (20 Punkte)

- Telegramm und Adressierung korrekt erklaert: 4
- Datentypen, Wortreihenfolge und Skalierung korrekt: 6
- Polling und CSV robust umgesetzt: 4
- Fehler sinnvoll klassifiziert: 3
- Security- und Architekturtransfer: 3
