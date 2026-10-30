"""Day-Ahead-Preise von Energy-Charts (Fraunhofer ISE) laden und als CSV speichern.

Beispiel:
  ./preise_laden.sh --von 2026-01-08 --bis 2026-01-15 --datei preise_testwoche.csv

Die Daten stammen von der Bundesnetzagentur | SMARD.de und stehen unter der
Lizenz CC BY 4.0. Zeiten werden in UTC gespeichert. Seit dem 1. Oktober 2025
hat der Day-Ahead-Markt eine Aufloesung von 15 Minuten; mit --stuendlich
werden die Werte zu Stundenmitteln zusammengefasst.
"""
import argparse

import pandas as pd
import requests

from optimierung import DATEN

API = "https://api.energy-charts.info/price"


def lade(von, bis, zone="DE-LU"):
    """Preise von 'von' (einschliesslich) bis 'bis' (ausschliesslich), Tage in UTC."""
    ende = (pd.Timestamp(bis, tz="UTC") - pd.Timedelta(minutes=15)).strftime("%Y-%m-%dT%H:%MZ")
    start = pd.Timestamp(von, tz="UTC").strftime("%Y-%m-%dT%H:%MZ")
    antwort = requests.get(API, params={"bzn": zone, "start": start, "end": ende}, timeout=60)
    antwort.raise_for_status()
    return nach_tabelle(antwort.json())


def nach_tabelle(json):
    zeit = pd.to_datetime(json["unix_seconds"], unit="s", utc=True)
    return pd.DataFrame({"zeit": zeit, "preis_eur_mwh": json["price"]})


def main():
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--von", required=True, help="erster Tag, z. B. 2026-01-08")
    parser.add_argument("--bis", required=True, help="Tag nach dem letzten Tag, z. B. 2026-01-15")
    parser.add_argument("--zone", default="DE-LU", help="Gebotszone, Standard DE-LU")
    parser.add_argument("--stuendlich", action="store_true", help="Stundenmittel statt 15 Minuten")
    parser.add_argument("--datei", default="preise.csv", help="Dateiname im Ordner daten")
    args = parser.parse_args()

    df = lade(args.von, args.bis, args.zone)
    if args.stuendlich:
        df = df.set_index("zeit").resample("h").mean().round(2).reset_index()
    df["zeit"] = df["zeit"].dt.strftime("%Y-%m-%dT%H:%M:%SZ")
    DATEN.mkdir(exist_ok=True)
    df.to_csv(DATEN / args.datei, index=False)
    print(f"{len(df)} Werte gespeichert in {DATEN / args.datei}")
    print(f"Preis von {df['preis_eur_mwh'].min():.2f} bis {df['preis_eur_mwh'].max():.2f} Euro/MWh")


if __name__ == "__main__":
    main()
