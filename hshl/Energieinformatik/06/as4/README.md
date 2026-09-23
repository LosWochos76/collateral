# AS4-Empfang als Lernmodell

Dieses Paket simuliert den Anwendungsfall aus der ersten Vorlesungsfolie zu Kapitel 06: Ein **Messstellenbetreiber** übermittelt eine **MSCONS-Zählerstandsmeldung** an einen **Lieferanten**. Für den Transport nutzt die deutsche Energiewirtschaft heute **AS4** (ebMS 3.0 mit SOAP über HTTP). `server.py` spielt den Lieferanten (AS4-Empfänger), `client.py` den Messstellenbetreiber (AS4-Sender).

Beide Skripte nutzen nur die Python-Standardbibliothek, es müssen keine Pakete installiert werden -- `requirements.txt` ist bewusst leer.

## Schnellstart

In zwei Terminalfenstern, jeweils im Verzeichnis `as4`:

```sh
cd ~/collateral/hshl/Energieinformatik/06/as4
./create_environment.sh
./server.sh
```

```sh
./client.sh
```

Der Server nimmt Nachrichten unter `http://127.0.0.1:8000/as4` entgegen. Der Client verpackt als Payload dieselbe MSCONS-Nachricht wie in Kapitel 4 (echtes EDIFACT, kein Platzhalter) in einen ebMS-3.0/SOAP-Umschlag mit einer zufälligen `MessageId`, sendet ihn per HTTP-POST und gibt sowohl die gesendete Nachricht als auch die technische Empfangsquittung (Receipt) aus. AS4 transportiert das EDIFACT-Dokument unverändert -- es wird dabei nicht in XML umkodiert.

## Duplikate erkennen

Der Server merkt sich bereits gesehene `MessageId`s. Ein zweiter Sendeversuch mit derselben ID zeigt das im Receipt:

```sh
./client.sh meine-test-id
./client.sh meine-test-id
```

Die zweite Antwort enthält `<Duplicate>true</Duplicate>`.

## Abgrenzung

Das Lernmodell implementiert keine vollständige AS4-Konformität: keine Signaturen, keine Verschlüsselung, keine Partnerzertifikate, keine WS-Security-Header. Es zeigt nur die grundlegende Struktur -- eindeutige `MessageId`, SOAP-Umschlag, technisches Receipt. Für echte Marktkommunikation ist ein zertifizierter AS4-Gateway nötig.

## Struktur

```text
server.py               AS4-Empfänger (Lieferant): nimmt Nachrichten entgegen, quittiert, erkennt Duplikate
client.py                AS4-Sender (Messstellenbetreiber): baut den SOAP-Umschlag, sendet ihn
requirements.txt         leer -- nur Standardbibliothek
create_environment.sh    legt .venv an und installiert requirements.txt
server.sh                startet server.py in .venv
client.sh                startet client.py in .venv (optionales Argument: MessageId)
```
