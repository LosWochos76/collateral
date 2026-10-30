"""Die Heizzentrale als Energiesystem mit oemof.solph.

Dasselbe Modell wie in heizzentrale.py, aber als Graph aus Bussen,
Komponenten und Flows. oemof.solph erzeugt daraus ein Pyomo-Modell.

Optionen:
  --investition  Groesse des Waermespeichers optimieren
  --heizperiode  ganze Heizperiode (Oktober bis Maerz) mit gemessenem Bedarf
  --lp-datei     das erzeugte Modell als LP-Datei speichern
"""
import argparse

import pandas as pd
from oemof import solph

from optimierung import (ERGEBNISSE, GASPREIS, KESSEL_ETA, KESSEL_MAX_KW, SPEICHER_KWH,
                         SPEICHER_VERLUST, WP_MAX_KW, daten_heizzentrale,
                         lade_preise, lade_waermebedarf)

SPEICHER_EURO_KWH = 30      # Investition je kWh Speicherinhalt (ca. 1000 Euro je m3 Wasser)
ANNUITAET = 0.0736          # 20 Jahre, 4 % Zins
WOCHEN_ANTEIL = 7 / 365     # Kosten der Testwoche: Anteil an den Jahreskosten


def baue_energiesystem(d, investition=False, ep_costs=None):
    """d: DataFrame mit bedarf_kw, strompreis (Euro/kWh) und cop je Stunde."""
    zeit = pd.date_range(d.index[0], periods=len(d) + 1, freq="h")
    es = solph.EnergySystem(timeindex=zeit, infer_last_interval=False)

    strom, gas, waerme = (solph.Bus(label="strom"), solph.Bus(label="gas"),
                          solph.Bus(label="waerme"))
    es.add(strom, gas, waerme)
    es.add(solph.components.Source(label="stromnetz",
        outputs={strom: solph.Flow(variable_costs=d["strompreis"])}))
    es.add(solph.components.Source(label="gasnetz",
        outputs={gas: solph.Flow(variable_costs=GASPREIS)}))
    es.add(solph.components.Converter(label="waermepumpe",
        inputs={strom: solph.Flow()},
        outputs={waerme: solph.Flow(nominal_capacity=WP_MAX_KW)},
        conversion_factors={waerme: d["cop"]}))
    es.add(solph.components.Converter(
        label="kessel",
        inputs={gas: solph.Flow()},
        outputs={waerme: solph.Flow(nominal_capacity=KESSEL_MAX_KW)},
        conversion_factors={waerme: KESSEL_ETA}))

    if investition:
        kapazitaet = solph.Investment(ep_costs=ep_costs)
    else:
        kapazitaet = SPEICHER_KWH
    es.add(solph.components.GenericStorage(label="speicher",
        nominal_capacity=kapazitaet,
        inputs={waerme: solph.Flow()}, outputs={waerme: solph.Flow()},
        loss_rate=SPEICHER_VERLUST,
        balanced=True))            # Fuellstand am Ende = am Anfang
    es.add(solph.components.Sink(label="haus",
        inputs={waerme: solph.Flow(fix=d["bedarf_kw"], nominal_capacity=1)}))
    return es


def rechne(d, investition=False, ep_costs=None, lp_datei=None):
    es = baue_energiesystem(d, investition, ep_costs)
    modell = solph.Model(es)       # erzeugt das Pyomo-Modell
    if lp_datei:
        modell.write(str(lp_datei), io_options={"symbolic_solver_labels": True})
    modell.solve(solver="highs")
    ergebnisse = solph.processing.results(modell)
    kosten = modell.objective()

    def fluss(von, nach):
        return ergebnisse[(es.groups[von], es.groups[nach])]["sequences"]["flow"].iloc[:len(d)].to_numpy()

    plan = pd.DataFrame({
        "waermepumpe_kw": fluss("waermepumpe", "waerme"),
        "kessel_kw": fluss("kessel", "waerme"),
        "strom_kw": fluss("strom", "waermepumpe"),
    }, index=d.index)
    inhalt = solph.views.node(ergebnisse, "speicher")["sequences"]
    spalte = [c for c in inhalt.columns if c[1] == "storage_content"][0]
    plan["speicher_kwh"] = inhalt[spalte].iloc[1:len(d) + 1].to_numpy()
    groesse = None
    if investition:
        groesse = ergebnisse[(es.groups["speicher"], None)]["scalars"]["invest"]
    return plan, kosten, groesse


def main():
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--investition", action="store_true")
    parser.add_argument("--heizperiode", action="store_true")
    parser.add_argument("--lp-datei", action="store_true")
    args = parser.parse_args()

    if args.heizperiode:
        d = daten_heizzentrale("messung_kw", lade_waermebedarf("waermebedarf_heizperiode.csv"),
                               lade_preise("preise_heizperiode.csv"))
        ep_costs = SPEICHER_EURO_KWH * ANNUITAET           # ganze Jahreskosten in der Heizperiode
    else:
        d = daten_heizzentrale()
        ep_costs = SPEICHER_EURO_KWH * ANNUITAET * WOCHEN_ANTEIL

    ERGEBNISSE.mkdir(exist_ok=True)
    lp = ERGEBNISSE / "heizzentrale.lp" if args.lp_datei else None
    plan, kosten, groesse = rechne(d, args.investition, ep_costs, lp)
    print(f"Zeitschritte: {len(d)}")
    print(f"Wert der Zielfunktion: {kosten:.2f} Euro")
    print(f"Waerme Waermepumpe: {plan['waermepumpe_kw'].sum():.0f} kWh, Kessel: {plan['kessel_kw'].sum():.0f} kWh")
    if groesse is not None:
        print(f"Optimale Speichergroesse: {groesse:.0f} kWh "
              f"(Investitionskosten im Zeitraum {groesse * ep_costs:.2f} Euro)")
    if lp:
        print(f"LP-Datei: {lp}")
    plan.to_csv(ERGEBNISSE / "heizzentrale_oemof.csv")


if __name__ == "__main__":
    main()
