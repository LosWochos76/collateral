# Folie 4 und 5: Den Pumpspeicher variieren

## Basis: Ausgangslage

- `./pumpspeicher.sh`: Gewinn 329.770 Euro, in 3 Stunden wird gleichzeitig gepumpt und turbiniert.
- `./pumpspeicher.sh --milp`: Gewinn 328.097 Euro, keine Stunde mit beidem.

## Basis: Wirkungsgrad 0,85 statt 0,75

Die meisten erwarten mehr Gewinn. Gerechnet ist es **weniger**:

| Modell | ETA 0,75 | ETA 0,85 |
|---|---|---|
| LP | 329.770 Euro | 324.854 Euro |
| MILP (`--milp`) | 328.097 Euro | 322.610 Euro |

Erklärung: Am 1. Mai wird der Betreiber mittags fürs **Pumpen bezahlt** (bis −499 Euro/MWh). Das Oberbecken fasst 590 MWh. Mit höherem Wirkungsgrad ist es nach weniger gepumpter Energie voll: Im MILP beginnt das Pumpen erst um 11 Uhr mit 82 MW statt mit 153 MW, und um 16 Uhr wird gar nicht mehr gepumpt. Es wird also weniger bezahlte Energie abgenommen. Ein Teil davon wird abends zurückgewonnen (um 21 Uhr volle Turbinenleistung), das gleicht den Verlust aber nicht aus. An einem Tag mit negativen Preisen ist ein schlechter Wirkungsgrad also ausnahmsweise kein Nachteil. Der Fahrplan ändert sich um 11, 16, 21 und 22 Uhr.

## Vertiefung: doppelte Speichergröße

- `SPEICHER_MWH = 1180`, `START_MWH = 590`: Gewinn **393.004 Euro**, also rund 19 % mehr, nicht das Doppelte.
- Grund: Jetzt begrenzt die **Pumpleistung**. Von 8 bis 17 Uhr wird ohne Pause mit vollen 153 MW gepumpt, der Speicher ist erst um 17 Uhr voll. Mehr Volumen nützt nur, wenn man es in den billigen Stunden auch füllen kann.

## Herausforderung (Folie 5): ein Wintertag

- `./preise_laden.sh --von 2026-01-12 --bis 2026-01-13 --stuendlich --datei preise_winter.csv`: Preise von 76,59 bis 132,76 Euro/MWh, keine negativen Preise.
- `./pumpspeicher.sh --datei preise_winter.csv`: Gewinn **12.875 Euro**, mit und ohne `--milp` gleich, keine Stunde mit beidem.
- Das Modell läuft ohne Änderung: Nur die Daten wechseln. Der Gewinn ist aber nur ein Fünfundzwanzigstel des Feiertags. Die Spreizung von rund 56 Euro/MWh reicht bei einem Wirkungsgrad von 75 % kaum aus.
