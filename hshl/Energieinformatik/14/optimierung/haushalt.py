"""Uebung zu Kapitel 14: ein Haushalt mit PV-Anlage, Elektroauto und dynamischem Tarif.

Das Startskript laedt die Daten der Woche vom 27. April bis 3. Mai 2026 in den
DataFrame d, zeigt sie als Grafik und rechnet die Referenz "sofort laden".
Ihr eigenes Modell schreiben Sie unten an die Stellen "# Aufgabe 11" usw.

Spalten von d (je Stunde eine Zeile, Index in Ortszeit):
  preis_bezug  Strompreis fuer den Bezug aus dem Netz in Euro/kWh
  verguetung   Verguetung fuer die Einspeisung in Euro/kWh
  pv_kw        Erzeugung der PV-Anlage in kW
  last_kw      Verbrauch des Haushalts ohne Auto in kW
  zuhause      1, wenn das Auto zu Hause steht und laden kann
  abfahrt      1 in der Stunde, in der das Auto abfaehrt
  fahrt_kwh    Energie, die die Fahrt verbraucht hat (in der Ankunftsstunde)

Ein Zeitschritt ist eine Stunde: 1 kW ueber eine Stunde sind 1 kWh.
"""
import matplotlib.pyplot as plt
import numpy as np
import pandas as pd
import pyomo.environ as pyo  # noqa: F401  (fuer Ihr Modell)

from optimierung import DATEN, loese  # noqa: F401  (loese fuer Ihr Modell)

PV_KWP = 10           # Nennleistung der PV-Anlage
PV_SPITZE_KW = 8      # hoechste Erzeugung in dieser Woche
AUFSCHLAG = 0.20      # Euro/kWh fuer Netzentgelt, Abgaben und Steuern
VERGUETUNG = 0.078    # Euro/kWh Einspeiseverguetung, 0 bei negativem Boersenpreis
AKKU_KWH = 60         # Kapazitaet des Autoakkus
WALLBOX_KW = 11       # hoechste Ladeleistung
WALLBOX_MIN_KW = 4.1  # Annahme: dreiphasig ohne Phasenumschaltung, 6 A x 3 x 230 V (Aufgabe 13)
LADE_ETA = 0.9        # Ladewirkungsgrad
START_KWH = 30        # Ladestand zu Beginn der Woche
ABFAHRT_KWH = 48      # Ladestand bei jeder Abfahrt und am Ende der Woche (80 %)
NETZ_MAX_KW = 25      # hoechster Bezug und hoechste Einspeisung


def lade_haushalt(datei="haushalt.csv"):
    """Daten der Woche als DataFrame d, Preise in Euro/kWh, Index in Ortszeit."""
    roh = pd.read_csv(DATEN / datei)
    zeit = pd.to_datetime(roh["zeit"], utc=True).dt.tz_convert("Europe/Berlin")
    d = pd.DataFrame(index=pd.DatetimeIndex(zeit, name="zeit"))
    d["preis_bezug"] = roh["preis_eur_mwh"].to_numpy() / 1000 + AUFSCHLAG
    d["verguetung"] = np.where(roh["preis_eur_mwh"].to_numpy() < 0, 0.0, VERGUETUNG)
    d["pv_kw"] = roh["pv_anteil"].to_numpy() * PV_SPITZE_KW
    for spalte in ["last_kw", "zuhause", "abfahrt", "fahrt_kwh"]:
        d[spalte] = roh[spalte].to_numpy()
    return d


def netz(d, laden_kw):
    """Netzbezug und Einspeisung, wenn nur das Auto geladen wird (kein Speicher)."""
    rest = d["last_kw"] + laden_kw - d["pv_kw"]
    return rest.clip(lower=0), (-rest).clip(lower=0)


def referenz(d):
    """Sofort laden: zu Hause und unter 48 kWh mit voller Leistung bis 48 kWh."""
    stand, laden, ladestand = START_KWH, [], []
    for t in d.index:
        stand -= d.at[t, "fahrt_kwh"]
        l = 0.0
        if d.at[t, "zuhause"] == 1 and stand < ABFAHRT_KWH:
            l = min(WALLBOX_KW, (ABFAHRT_KWH - stand) / LADE_ETA)
        stand += LADE_ETA * l
        laden.append(l)
        ladestand.append(stand)
    plan = pd.DataFrame({"laden_kw": laden, "ladestand_kwh": ladestand}, index=d.index)
    plan["bezug_kw"], plan["einspeisung_kw"] = netz(d, plan["laden_kw"])
    return plan


def kosten(d, plan):
    """Stromkosten der Woche in Euro: Bezug minus Verguetung."""
    return float((d["preis_bezug"] * plan["bezug_kw"] - d["verguetung"] * plan["einspeisung_kw"]).sum())


def zeige_fahrplan(d, plan, titel="Fahrplan"):
    """Preis, PV, Last, Laden und Ladestand einer Loesung als Grafik."""
    fig, ax = plt.subplots(3, 1, figsize=(10, 7), sharex=True)
    ax[0].step(d.index, d["preis_bezug"] * 100, where="post", color="black")
    ax[0].axhline(0, color="grey", lw=0.5)
    ax[0].set_ylabel("Bezug\nin ct/kWh")
    ax[1].fill_between(d.index, d["pv_kw"], step="post", color="gold", alpha=0.6, label="PV")
    ax[1].step(d.index, d["last_kw"], where="post", color="grey", label="Haushalt")
    if plan is not None:
        ax[1].step(d.index, plan["laden_kw"], where="post", color="tab:blue", label="Laden")
        ax[2].plot(d.index, plan["ladestand_kwh"], color="tab:blue", label="Ladestand")
    ax[1].set_ylabel("Leistung\nin kW")
    ax[1].legend(loc="upper right", ncol=3)
    ax[2].fill_between(d.index, 0, AKKU_KWH * d["zuhause"], step="post", color="lightgrey",
                       label="Auto zu Hause")
    ax[2].set_ylabel("Ladestand\nin kWh")
    ax[2].legend(loc="upper right", ncol=2)
    ax[0].set_title(titel)
    fig.tight_layout()
    plt.show()


def kennzahlen(d, plan, name):
    pv_ueberschuss = (d["pv_kw"] - d["last_kw"]).clip(lower=0)
    aus_pv = np.minimum(plan["laden_kw"], pv_ueberschuss).sum()
    beides = int(((plan["bezug_kw"] > 0.01) & (plan["einspeisung_kw"] > 0.01)).sum())
    print(f"{name}: {kosten(d, plan):7.2f} Euro, geladen {plan['laden_kw'].sum():5.1f} kWh, "
          f"davon aus PV-Ueberschuss {aus_pv:5.1f} kWh, Stunden mit Bezug UND Einspeisung: {beides}")


def main():
    d = lade_haushalt()
    print(d.round(3).head(24).to_string())
    print(f"\nPV-Erzeugung der Woche: {d['pv_kw'].sum():.0f} kWh, Verbrauch Haushalt: {d['last_kw'].sum():.0f} kWh")
    ref = referenz(d)
    kennzahlen(d, ref, "Referenz 'sofort laden'")
    zeige_fahrplan(d, ref, "Referenz: sofort laden")

    # Aufgabe 11: Ihr Modell als Funktion baue_modell(d), loesen mit loese(m),
    #             Plan mit den Spalten laden_kw, ladestand_kwh, bezug_kw, einspeisung_kw
    #             erzeugen und mit kennzahlen(d, plan, "LP") und zeige_fahrplan(d, plan) auswerten.

    # Aufgabe 12: Binaervariable "Bezug oder Einspeisung"

    # Aufgabe 13: Mindestladeleistung der Wallbox (WALLBOX_MIN_KW)


if __name__ == "__main__":
    main()
