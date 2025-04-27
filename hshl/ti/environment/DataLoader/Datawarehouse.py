import psycopg2
import os
import pandas as pd
import openpyxl

host = os.getenv("POSTGRES_HOST", "localhost")
port = os.getenv("POSTGRES_PORT", "5432")
user = os.getenv("POSTGRES_USER", "postgres")
database = os.getenv("POSTGRES_DATABASE", "postgres")
password = os.getenv("POSTGRES_PASSWORD", "hshl")
connection = psycopg2.connect(host=host, port=port, user=user, password=password, database=database)
connection.autocommit = True

positionen = [
    # Teilergebnisplan
    {"nummer": 1, "name": "Steuern und ähnliche Abgaben", "art": "Teilergebnisplan"},
    {"nummer": 2, "name": "Zuwendungen und allgemeine Umlagen", "art": "Teilergebnisplan"},
    {"nummer": 3, "name": "Sonstige Transfererträge", "art": "Teilergebnisplan"},
    {"nummer": 4, "name": "Öffentlich-rechtliche Leistungsentgelte", "art": "Teilergebnisplan"},
    {"nummer": 5, "name": "Privatrechtliche Leistungsentgelte", "art": "Teilergebnisplan"},
    {"nummer": 6, "name": "Kostenerstattungen und Kostenumlagen", "art": "Teilergebnisplan"},
    {"nummer": 7, "name": "Sonstige ordentliche Erträge", "art": "Teilergebnisplan"},
    {"nummer": 8, "name": "Aktivierte Eigenleistungen", "art": "Teilergebnisplan"},
    {"nummer": 9, "name": "Bestandsveränderungen", "art": "Teilergebnisplan"},
    {"nummer": 10, "name": "Ordentliche Erträge", "art": "Teilergebnisplan"},
    {"nummer": 11, "name": "Personalaufwendungen", "art": "Teilergebnisplan"},
    {"nummer": 12, "name": "Versorgungsaufwendungen", "art": "Teilergebnisplan"},
    {"nummer": 13, "name": "Aufwendungen für Sach- und Dienstleistungen", "art": "Teilergebnisplan"},
    {"nummer": 14, "name": "Bilanzielle Abschreibungen", "art": "Teilergebnisplan"},
    {"nummer": 15, "name": "Transferaufwendungen", "art": "Teilergebnisplan"},
    {"nummer": 16, "name": "Sonstige ordentliche Aufwendungen", "art": "Teilergebnisplan"},
    {"nummer": 17, "name": "Ordentliche Aufwendungen", "art": "Teilergebnisplan"},
    {"nummer": 18, "name": "Ordentliches Ergebnis", "art": "Teilergebnisplan"},
    {"nummer": 19, "name": "Finanzerträge", "art": "Teilergebnisplan"},
    {"nummer": 20, "name": "Zinsen und ähnliche Aufwendungen", "art": "Teilergebnisplan"},
    {"nummer": 21, "name": "Finanzergebnis", "art": "Teilergebnisplan"},
    {"nummer": 22, "name": "Ergebnis der laufenden Verwaltungstätigkeit", "art": "Teilergebnisplan"},
    {"nummer": 23, "name": "Außerordentliche Erträge", "art": "Teilergebnisplan"},
    {"nummer": 24, "name": "Außerordentliche Aufwendungen", "art": "Teilergebnisplan"},
    {"nummer": 25, "name": "Außerordentliches Ergebnis", "art": "Teilergebnisplan"},
    {"nummer": 26, "name": "Ergebnis vor Berücksichtigung der internen Leistungserbringung", "art": "Teilergebnisplan"},
    {"nummer": 27, "name": "Erträge aus internen Leistungsbeziehungen", "art": "Teilergebnisplan"},
    {"nummer": 28, "name": "Aufwendungen aus internen Leistungsbeziehungen", "art": "Teilergebnisplan"},
    {"nummer": 29, "name": "Teilergebnis", "art": "Teilergebnisplan"},
    {"nummer": 30, "name": "Globaler Minderaufwand", "art": "Teilergebnisplan"},
    {"nummer": 31, "name": "Teilergebnis nach Abzug globaler Minderaufwand", "art": "Teilergebnisplan"},

    # Teilfinanzplan
    {"nummer": 1, "name": "Steuern und ähnliche Abgaben", "art": "Teilfinanzplan"},
    {"nummer": 2, "name": "Zuwendungen und allgemeine Umlagen", "art": "Teilfinanzplan"},
    {"nummer": 3, "name": "Sonstige Transfereinzahlungen", "art": "Teilfinanzplan"},
    {"nummer": 4, "name": "Öffentlich-rechtliche Leistungsentgelte", "art": "Teilfinanzplan"},
    {"nummer": 5, "name": "Privatrechtliche Leistungsentgelte", "art": "Teilfinanzplan"},
    {"nummer": 6, "name": "Kostenerstattungen, Kostenumlagen", "art": "Teilfinanzplan"},
    {"nummer": 7, "name": "Sonstige Einzahlungen", "art": "Teilfinanzplan"},
    {"nummer": 8, "name": "Zinsen und sonstige Finanzeinzahlungen", "art": "Teilfinanzplan"},
    {"nummer": 9, "name": "Einzahlungen aus laufender Verwaltungstätigkeit", "art": "Teilfinanzplan"},
    {"nummer": 10, "name": "Personalauszahlungen", "art": "Teilfinanzplan"},
    {"nummer": 11, "name": "Versorgungsauszahlungen", "art": "Teilfinanzplan"},
    {"nummer": 12, "name": "Auszahlungen für Sach- und Dienstleistungen", "art": "Teilfinanzplan"},
    {"nummer": 13, "name": "Zinsen und sonstige Finanzauszahlungen", "art": "Teilfinanzplan"},
    {"nummer": 14, "name": "Transferauszahlungen", "art": "Teilfinanzplan"},
    {"nummer": 15, "name": "Sonstige Auszahlungen", "art": "Teilfinanzplan"},
    {"nummer": 16, "name": "Auszahlungen aus laufender Verwaltungstätigkeit", "art": "Teilfinanzplan"},
    {"nummer": 17, "name": "Saldo aus laufender Verwaltungstätigkeit (Einzahlungen ./. Auszahlungen)", "art": "Teilfinanzplan"},
    {"nummer": 18, "name": "Zuwendungen für Investitionsmaßnahmen", "art": "Teilfinanzplan"},
    {"nummer": 19, "name": "Einzahlungen aus der Veräußerung von Sachanlagen", "art": "Teilfinanzplan"},
    {"nummer": 20, "name": "Einzahlungen aus der Veräußerung von Finanzanlagen", "art": "Teilfinanzplan"},
    {"nummer": 21, "name": "Einzahlungen aus Beiträgen und ähnlichen Entgelten", "art": "Teilfinanzplan"},
    {"nummer": 22, "name": "Sonstige Investitionseinzahlungen", "art": "Teilfinanzplan"},
    {"nummer": 23, "name": "Einzahlungen aus Investitionstätigkeit", "art": "Teilfinanzplan"},
    {"nummer": 24, "name": "Auszahlungen für den Erwerb von Grundstücken und Gebäuden", "art": "Teilfinanzplan"},
    {"nummer": 25, "name": "Auszahlungen für Baumaßnahmen", "art": "Teilfinanzplan"},
    {"nummer": 26, "name": "Auszahlungen für den Erwerb von beweglichem Anlagevermögen", "art": "Teilfinanzplan"},
    {"nummer": 27, "name": "Auszahlungen für den Erwerb von Finanzanlagen", "art": "Teilfinanzplan"},
    {"nummer": 28, "name": "Auszahlungen von aktivierbaren Zuwendungen", "art": "Teilfinanzplan"},
    {"nummer": 29, "name": "Sonstige Investitionsauszahlungen", "art": "Teilfinanzplan"},
    {"nummer": 30, "name": "Auszahlungen für Investitionstätigkeit", "art": "Teilfinanzplan"},
    {"nummer": 31, "name": "Saldo der Investitionstätigkeit (Einzahlungen ./. Auszahlungen)", "art": "Teilfinanzplan"},
]

produkte = [
    (1, 1, 1, "Rat, Ausschüsse u. sonst. Gremien", "PO"),
    (1, 1, 2, "Fraktionen", "PO"),
    (1, 1, 3, "Verwaltungsführung", "PM"),
    (1, 1, 4, "Gleichstellung von Mann und Frau", "PM"),
    (1, 1, 5, "Beschäftigtenvertretung", "PM"),
    (1, 1, 6, "Datenschutz und IT-Sicherheit", "PM"),
    (1, 2, 1, "Rechnungsprüfung", "PM"),
    (1, 3, 1, "Organisationsangelegenheiten", "F"),
    (1, 3, 2, "Informations- u. Kommunikationstechnik", "F"),
    (1, 3, 3, "Sonstige Zentrale Dienste", "F"),
    (1, 4, 1, "Presse- u. Öffentlichkeitsarb., Städtepartnersch., Ehrenamt", "F"),
    (1, 5, 1, "Personalmanagement", "PM"),
    (1, 6, 1, "Haushaltsplanung, Haushaltsausführung u. Jahresabschl.", "PO"),
    (1, 6, 2, "Finanz- u. betriebswirtschaftliche Steuerung", "PM"),
    (1, 6, 3, "Vollstreckung", "PM"),
    (1, 6, 4, "Kommunale Steuern und sonstige Abgaben", "PM"),
    (1, 7, 1, "Rechtsangelegenheiten", "F"),
    (1, 8, 1, "Unbebaute Grundstücke", "F"),
    (1, 8, 2, "Verwaltungsgebäude", "PM"),
    (1, 8, 3, "Mehrzweckgebäude", "F"),
    (1, 8, 4, "Liegenschaften", "PM"),
    (1, 8, 5, "Hochbau", "PM"),
    (2, 1, 1, "Allgemeine Sicherheit und Ordnung", "PO"),
    (2, 1, 2, "Verkehrsüberwachung", "PM"),
    (2, 2, 1, "Gewerbebetriebe", "PO"),
    (2, 2, 2, "Märkte und Veranstaltungen", "PM"),
    (2, 3, 1, "Einwohnerangelegenheiten", "PM"),
    (2, 3, 2, "Personenstandswesen", "PO"),
    (2, 3, 3, "Wahlen", "PO"),
    (2, 4, 1, "Feuerschutz und Katastrophenabwehr", "PO"),
    (2, 5, 1, "Rettungsdienst", "PO"),
    (3, 1, 1, "Katholische Grundschule", "PM"),
    (3, 1, 2, "Gemeinschaftsgrundschule Alt-Wetter", "PM"),
    (3, 1, 3, "Gemeinschaftsgrundschule Grundschöttel", "PM"),
    (3, 1, 4, "Gemeinschaftsgrundschule Volmarstein", "PM"),
    (3, 1, 5, "Gemeinschaftsgrundschule Elbschebach Wetter (Ruhr)", "PM"),
    (3, 1, 10, "Gymnasium", "PM"),
    (3, 1, 11, "Förderschule", "PM"),
    (3, 1, 12, "Turn- und Sporthallen", "PM"),
    (3, 1, 14, "Sekundarschule", "PM"),
    (3, 1, 15, "Schulische Ganztagsbetreuung", "PM"),
    (4, 1, 1, "Kulturförderung", "F"),
    (4, 2, 1, "Kommunale Veranstaltungen", "F"),
    (4, 3, 1, "Volkshochschule", "PM"),
    (4, 3, 2, "Musikschule", "F"),
    (4, 3, 3, "Medien und Information", "F"),
    (4, 3, 4, "Archiv", "PM"),
    (4, 3, 5, "Kreisarchiv", "F"),
    (5, 1, 1, "Unterstützung von Senioren", "PM"),
    (5, 2, 1, "Hilfen bei Krankheit, Behinderung, Pflegebedürftigkeit", "PM"),
    (5, 3, 1, "Hilfen zum Lebensunterhalt", "PO"),
    (5, 3, 2, "Grundsicherung im Alter u. bei Erwerbsminderung", "PO"),
    (5, 3, 3, "Sonstige Hilfen und Leistungen", "PO"),
    (5, 3, 4, "Hilfen n.d. Asylbewerberleistungsgesetz", "PO"),
    (5, 3, 5, "Jobcenter EN", "PM"),
    (5, 3, 6, "Unterhaltsvorschuss", "PO"),
    (6, 1, 1, "Tageseinrichtungen für Kinder", "PM"),
    (6, 1, 2, "Kindertagespflege", "PM"),
    (6, 2, 1, "Kinder- und Jugendarbeit in/durch Einrichtungen", "PM"),
    (6, 2, 2, "Kinder- u. Jugendarbeit außerh. von Einricht., Jugendschutz", "PM"),
    (6, 2, 3, "Spielplätze", "F"),
    (6, 3, 1, "Familienunterstützende Maßnahmen", "PM"),
    (6, 3, 2, "Familienergänzende Maßnahmen", "PM"),
    (6, 3, 3, "Familienersetzende Maßnahmen", "PM"),
    (6, 3, 4, "Eingliederungshilfe f. seelisch behinderte Kinder u. Jugendl.", "PM"),
    (7, 1, 1, "Sucht- und Drogenhilfe", "F"),
    (7, 1, 2, "Krankenhäuser", "PO"),
    (8, 1, 1, "Sportförderung", "F"),
    (8, 2, 1, "Stadien/Sportplätze", "F"),
    (8, 2, 2, "Sportgebäude und Sportlerheime", "F"),
    (8, 3, 1, "Freibad", "F"),
    (8, 3, 2, "Hallenbad", "F"),
    (8, 3, 3, "Lehrschwimmbecken", "PM"),
    (9, 1, 1, "Vorbereitende und informelle Bauleitplanung", "PM"),
    (9, 1, 2, "Verbindliche Bauleitplanung, Satzungen", "PM"),
    (9, 1, 3, "Städtebauliche Entwicklung und Sanierung", "PM"),
    (9, 1, 4, "Geoinformation", "PO"),
    (10, 1, 1, "Maßnahmen der Bauaufsicht", "PO"),
    (10, 2, 1, "Denkmalschutz- und pflege", "PM"),
    (10, 3, 1, "Wohnungsbauförderung", "PM"),
    (10, 4, 1, "Subjektbezogene Förderung für Wohnraum", "PO"),
    (10, 5, 1, "Soziale Einrichtungen f. Wohnungslose, Aussiedler, Flüchtlinge", "PM"),
    (11, 1, 1, "Versorgung", "PO"),
    (11, 2, 1, "Entwässerung", "PM"),
    (12, 1, 1, "Straßen, Wege, Plätze einschl. Verkehrsanlagen", "PM"),
    (12, 1, 2, "Parkplätze u. -häuser", "PM"),
    (12, 2, 1, "ÖPNV", "PM"),
    (12, 3, 1, "Straßenreinigung", "PM"),
    (12, 3, 2, "Winterdienst", "PM"),
    (13, 1, 1, "Öffentliches Grün", "PM"),
    (13, 1, 2, "Natur und Landschaft", "PM"),
    (13, 1, 3, "Friedhöfe", "PM"),
    (13, 2, 1, "Wald, Forst- und Landwirtschaft", "PM"),
    (13, 3, 1, "Wasser und Wasserbau", "PM"),
    (14, 1, 1, "Umweltinformation und -koordination", "PM"),
    (14, 1, 2, "Besondere Dienstleistungen im Umweltmanagement, lokale Agenda", "F"),
    (14, 1, 3, "Schutz vor altlastenbedingten Gefahren", "PM"),
    (15, 1, 1, "Wirtschaftsförderung", "F"),
    (15, 1, 2, "Kurzzeittourismus", "F"),
    (15, 1, 3, "Stadtmarketing", "F"),
    (16, 1, 1, "Allgemeine Finanzwirtschaft", "PM"),
]

def clear():
    global connection
    with connection.cursor() as cur:
        cur.execute("drop schema if exists haushalt CASCADE;")
        cur.execute("create schema haushalt;")

def create_dimensions():
    global connection, teilergebnisplan_positionen, teilfinanzplan_positionen

    with connection.cursor() as cur:
        cur.execute("""
            DO $$
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'haushalt_position_art') THEN
                    CREATE TYPE haushalt_position_art AS ENUM ('Teilergebnisplan', 'Teilfinanzplan');
                END IF;
            END
            $$;
        """)

        cur.execute("""
            CREATE TABLE IF NOT EXISTS haushalt.Positionen (
                id SERIAL PRIMARY KEY,
                nummer INTEGER NOT NULL,
                name VARCHAR(250) NOT NULL,
                art haushalt_position_art NOT NULL
            );
        """)

        for pos in positionen:
            cur.execute(
                """
                INSERT INTO haushalt.Positionen (nummer, name, art)
                VALUES (%s, %s, %s);
                """,
                (pos["nummer"], pos["name"], pos["art"])
            )

        cur.execute("""
            CREATE TABLE IF NOT EXISTS haushalt.Produkte (
                id SERIAL PRIMARY KEY,
                obergruppe INTEGER not null,
                mittelgruppe INTEGER not null,
                untergruppe INTEGER not null,
                bezeichnung varchar(250) not null,
                rechtsbindung varchar(5) not null
            );
        """)

        for produkt in produkte:
            cur.execute("""
                INSERT INTO haushalt.Produkte (obergruppe, mittelgruppe, untergruppe, bezeichnung, rechtsbindung)
                VALUES (%s, %s, %s, %s, %s);
            """, produkt)

        cur.execute("""
                    CREATE TABLE IF NOT EXISTS haushalt.Fakten (
                        erscheinungsjahr INTEGER NOT NULL,
                        ansatz INTEGER NOT NULL,
                        produkt_id INTEGER NOT NULL REFERENCES haushalt.Produkte(id),
                        position_id INTEGER NOT NULL REFERENCES haushalt.Positionen(id),
                        wert NUMERIC(14,2) NOT NULL,
                        CONSTRAINT fakten_unique_entry UNIQUE (erscheinungsjahr, ansatz, produkt_id, position_id)
                    );
                """)

def get_produkt_id(obergruppe, mittelgruppe, untergruppe):
    for idx, (og, mg, ug, _, _) in enumerate(produkte, start=1):
        if (og, mg, ug) == (obergruppe, mittelgruppe, untergruppe):
            return idx
    return None

def get_position_id(nummer, art):
    for idx, pos in enumerate(positionen, start=1):
        if pos["nummer"] == nummer and pos["art"] == art:
            return idx
    return None

def insert_fakt(erscheinungsjahr, ansatz, obergruppe, mittelgruppe, untergruppe, position, art, wert):
    global connection

    produkt_id = get_produkt_id(obergruppe, mittelgruppe, untergruppe)
    position_id = get_position_id(position, art)

    if produkt_id is None:
        raise ValueError(f"Produkt nicht gefunden: {obergruppe}-{mittelgruppe}-{untergruppe}")
    if position_id is None:
        raise ValueError(f"Position nicht gefunden: Nummer {position}, Art {art}")

    with connection.cursor() as cur:
        cur.execute("""
            INSERT INTO haushalt.Fakten (erscheinungsjahr, ansatz, produkt_id, position_id, wert)
            VALUES (%s, %s, %s, %s, %s)
            ON CONFLICT (erscheinungsjahr, ansatz, produkt_id, position_id)
            DO UPDATE SET wert = EXCLUDED.wert;
        """, (erscheinungsjahr, ansatz, produkt_id, position_id, wert))

def load_fakten():
    ordner = "./haushalt/"
    for dateiname in os.listdir(ordner):
        if dateiname.endswith(".xlsx") or dateiname.endswith(".xls"):
            dateipfad = os.path.join(ordner, dateiname)
            lade_excel_datei(dateipfad)


def lade_excel_datei(dateipfad):
    global connection

    print(f"Lese Datei: {dateipfad}")

    # Engine automatisch setzen je nach Dateityp
    if dateipfad.endswith(".xlsx"):
        df = pd.read_excel(dateipfad, sheet_name=0, engine="openpyxl")
    elif dateipfad.endswith(".xls"):
        df = pd.read_excel(dateipfad, sheet_name=0, engine="xlrd")
    else:
        print(f"Unbekanntes Dateiformat: {dateipfad}")
        return

    for index, row in df.iterrows():
        try:
            erscheinungsjahr = int(row["Quelle"])
            produktbereich = int(row["Produktbereich"])
            produktgruppe = int(row["Produktgruppe"])
            produkt = int(row["Produkt"])
            typ = row["Typ"]
            position = int(row["Position"])
            ansatz = int(row["Ansatz"])
            wert_string = str(row["Wert"]).replace(".", "").replace(",", ".").replace("€", "").strip()
            wert = float(wert_string)

            if wert == 0:
                continue

            if erscheinungsjahr - ansatz > 1:
                continue

            if typ == "TE":
                art = "Teilergebnisplan"
            elif typ == "TF":
                art = "Teilfinanzplan"
            else:
                print(f"Unbekannter Typ {typ} in Datei {dateipfad} Zeile {index + 2}, überspringe Zeile.")
                continue

            insert_fakt(
                erscheinungsjahr=erscheinungsjahr,
                ansatz=ansatz,
                obergruppe=produktbereich,
                mittelgruppe=produktgruppe,
                untergruppe=produkt,
                position=position,
                art=art,
                wert=wert
            )
        except Exception as e:
            print(f"Fehler in Datei {dateipfad} Zeile {index + 2}: {e}")