from __future__ import annotations

import argparse
import os
from dataclasses import dataclass
from pathlib import Path

os.environ.setdefault("XDG_CACHE_HOME", str(Path(".cache").resolve()))

import numpy as np
import pandas as pd
from oemof import solph
from pyomo.environ import SolverFactory
from reportlab.lib.colors import HexColor, black, white
from reportlab.pdfgen import canvas


@dataclass(frozen=True)
class ThermalPlant:
    label: str
    capacity_mw: float
    min_load: float
    variable_cost_eur_per_mwh: float
    emission_t_per_mwh: float


THERMAL_PLANTS = [
    ThermalPlant(
        label="Braunkohle",
        capacity_mw=18_000,
        min_load=0.40,
        variable_cost_eur_per_mwh=32,
        emission_t_per_mwh=1.05,
    ),
    ThermalPlant(
        label="Steinkohle",
        capacity_mw=16_000,
        min_load=0.35,
        variable_cost_eur_per_mwh=58,
        emission_t_per_mwh=0.82,
    ),
    ThermalPlant(
        label="Gas",
        capacity_mw=36_000,
        min_load=0.20,
        variable_cost_eur_per_mwh=92,
        emission_t_per_mwh=0.38,
    ),
]

RENEWABLE_CAPACITY_MW = {
    "PV": 82_000,
    "Wind": 70_000,
}


def make_time_index(year: int) -> pd.DatetimeIndex:
    return pd.date_range(
        f"{year}-01-01 00:00",
        f"{year + 1}-01-01 00:00",
        freq="h",
        inclusive="left",
    )


def synthetic_demand(index: pd.DatetimeIndex, seed: int = 42) -> pd.Series:
    rng = np.random.default_rng(seed)
    hour = index.hour.to_numpy()
    day_of_year = index.dayofyear.to_numpy()
    weekday = index.weekday.to_numpy()

    seasonal = 1.0 + 0.11 * np.cos(2 * np.pi * (day_of_year - 15) / 365)
    morning_peak = 6_500 * np.exp(-0.5 * ((hour - 8) / 2.7) ** 2)
    evening_peak = 9_500 * np.exp(-0.5 * ((hour - 18) / 3.0) ** 2)
    night_relief = -4_500 * np.exp(-0.5 * ((hour - 3) / 3.5) ** 2)
    weekday_factor = np.where(weekday < 5, 1.0, 0.88)
    noise = rng.normal(0, 900, len(index))

    demand = (52_000 + morning_peak + evening_peak + night_relief) * seasonal
    demand = demand * weekday_factor + noise
    demand = np.clip(demand, 34_000, 86_000)

    # Scale to a plausible German annual electricity demand of 505 TWh.
    demand *= 505_000_000 / demand.sum()
    return pd.Series(demand, index=index, name="Last_MW")


def synthetic_pv_profile(index: pd.DatetimeIndex) -> pd.Series:
    hour = index.hour.to_numpy() + index.minute.to_numpy() / 60
    day = index.dayofyear.to_numpy()

    daylight = np.clip(np.sin(np.pi * (hour - 5.0) / 14.0), 0, None)
    seasonal = np.clip(0.22 + 0.78 * np.sin(np.pi * (day - 20) / 365), 0.05, 1.0)
    cloud_variation = 0.78 + 0.12 * np.sin(2 * np.pi * day / 9) + 0.08 * np.sin(2 * np.pi * day / 23)
    profile = daylight**1.7 * seasonal * cloud_variation
    return pd.Series(np.clip(profile, 0, 0.92), index=index, name="PV_Auslastung")


def synthetic_wind_profile(index: pd.DatetimeIndex, seed: int = 7) -> pd.Series:
    rng = np.random.default_rng(seed)
    day = index.dayofyear.to_numpy()
    hour = np.arange(len(index))

    seasonal = 0.30 + 0.12 * np.cos(2 * np.pi * (day - 20) / 365)
    synoptic = 0.12 * np.sin(2 * np.pi * hour / (24 * 4.8)) + 0.08 * np.sin(2 * np.pi * hour / (24 * 9.5))
    noise = rng.normal(0, 0.06, len(index))
    raw = seasonal + synoptic + noise
    smooth = pd.Series(raw, index=index).rolling(6, center=True, min_periods=1).mean()
    return smooth.clip(0.03, 0.82).rename("Wind_Auslastung")


def build_energy_system(index: pd.DatetimeIndex, demand: pd.Series, pv_cf: pd.Series, wind_cf: pd.Series):
    energy_system = solph.EnergySystem(timeindex=index, infer_last_interval=False)
    electricity = solph.Bus(label="Strom")
    energy_system.add(electricity)

    energy_system.add(
        solph.components.Sink(
            label="Last",
            inputs={electricity: solph.Flow(fix=demand, nominal_capacity=1)},
        )
    )

    energy_system.add(
        solph.components.Source(
            label="PV",
            outputs={
                electricity: solph.Flow(
                    fix=pv_cf,
                    nominal_capacity=RENEWABLE_CAPACITY_MW["PV"],
                    variable_costs=0,
                )
            },
        )
    )
    energy_system.add(
        solph.components.Source(
            label="Wind",
            outputs={
                electricity: solph.Flow(
                    fix=wind_cf,
                    nominal_capacity=RENEWABLE_CAPACITY_MW["Wind"],
                    variable_costs=0,
                )
            },
        )
    )

    for plant in THERMAL_PLANTS:
        energy_system.add(
            solph.components.Source(
                label=plant.label,
                outputs={
                    electricity: solph.Flow(
                        nominal_capacity=plant.capacity_mw,
                        minimum=plant.min_load,
                        maximum=1,
                        variable_costs=plant.variable_cost_eur_per_mwh,
                        nonconvex=solph.NonConvex(),
                    )
                },
            )
        )

    energy_system.add(
        solph.components.Sink(
            label="Abregelung",
            inputs={electricity: solph.Flow(variable_costs=0)},
        )
    )
    energy_system.add(
        solph.components.Source(
            label="Import_Lastabwurf",
            outputs={electricity: solph.Flow(variable_costs=5_000)},
        )
    )

    return energy_system


def solve_model(energy_system, solver: str):
    model = solph.Model(energy_system)
    if solver in {"highs", "appsi_highs"}:
        opt = SolverFactory(solver)
        solver_results = opt.solve(model, tee=False)
        status = str(solver_results.solver.status).lower()
        termination = str(solver_results.solver.termination_condition).lower()
        model.es.results = solver_results
        model.solver_results = solver_results
        if status != "ok" or termination != "optimal":
            raise RuntimeError(f"Solver endete mit Status={status}, Termination={termination}")
    else:
        model.solve(solver=solver, solve_kwargs={"tee": False})
    return model


def flow_series(results, source: str, target: str = "Strom") -> pd.Series:
    key = ((source, target), "flow")
    for result_key, values in results.items():
        from_label = str(result_key[0].label)
        to_label = str(result_key[1].label)
        if (from_label, to_label) == (source, target):
            return values["sequences"]["flow"]
    raise KeyError(f"Flow {key} nicht in den Ergebnissen gefunden.")


def collect_timeseries(model, index: pd.DatetimeIndex, demand: pd.Series):
    results = solph.processing.results(model)
    timeseries = pd.DataFrame(index=index)
    timeseries["Last_MW"] = demand
    timeseries["PV_MW"] = flow_series(results, "PV").reindex(index).fillna(0)
    timeseries["Wind_MW"] = flow_series(results, "Wind").reindex(index).fillna(0)

    for plant in THERMAL_PLANTS:
        dispatch = flow_series(results, plant.label).reindex(index).fillna(0)
        timeseries[f"{plant.label}_MW"] = dispatch

    timeseries["Import_Lastabwurf_MW"] = flow_series(results, "Import_Lastabwurf").reindex(index).fillna(0)
    timeseries["Abregelung_MW"] = flow_series(results, "Strom", "Abregelung").reindex(index).fillna(0)
    timeseries["Residuallast_MW"] = timeseries["Last_MW"] - timeseries["PV_MW"] - timeseries["Wind_MW"]

    return timeseries


def summarize(timeseries: pd.DataFrame) -> pd.DataFrame:
    summary_rows = []
    for plant in THERMAL_PLANTS:
        dispatch = timeseries[f"{plant.label}_MW"]
        energy_mwh = dispatch.sum()
        variable_costs = energy_mwh * plant.variable_cost_eur_per_mwh
        emissions = energy_mwh * plant.emission_t_per_mwh
        summary_rows.append(
            {
                "Anlage": plant.label,
                "Installierte_Leistung_MW": plant.capacity_mw,
                "Energie_MWh": energy_mwh,
                "Variable_Kosten_EUR": variable_costs,
                "CO2_t": emissions,
            }
        )

    summary = pd.DataFrame(summary_rows)
    dispatch_total = {
        "Anlage": "Summe regelbare Kraftwerke",
        "Installierte_Leistung_MW": summary["Installierte_Leistung_MW"].sum(),
        "Energie_MWh": summary["Energie_MWh"].sum(),
        "Variable_Kosten_EUR": summary["Variable_Kosten_EUR"].sum(),
        "CO2_t": summary["CO2_t"].sum(),
    }
    total_cost = dispatch_total["Variable_Kosten_EUR"]
    total_co2 = dispatch_total["CO2_t"]

    import_energy = timeseries["Import_Lastabwurf_MW"].sum()
    if import_energy > 0.01:
        import_row = {
            "Anlage": "Import/Lastabwurf",
            "Installierte_Leistung_MW": np.nan,
            "Energie_MWh": import_energy,
            "Variable_Kosten_EUR": import_energy * 5_000,
            "CO2_t": 0,
        }
        summary.loc[len(summary)] = import_row
        total_cost += import_row["Variable_Kosten_EUR"]

    summary.loc[len(summary)] = dispatch_total
    summary.loc[len(summary)] = {
        "Anlage": "Gesamt",
        "Installierte_Leistung_MW": np.nan,
        "Energie_MWh": np.nan,
        "Variable_Kosten_EUR": total_cost,
        "CO2_t": total_co2,
    }

    return summary


def solve_in_chunks(
    index: pd.DatetimeIndex,
    demand: pd.Series,
    pv_cf: pd.Series,
    wind_cf: pd.Series,
    solver: str,
    chunk_days: int,
) -> pd.DataFrame:
    if chunk_days <= 0:
        energy_system = build_energy_system(index, demand, pv_cf, wind_cf)
        model = solve_model(energy_system, solver)
        return collect_timeseries(model, index, demand)

    chunk_hours = chunk_days * 24
    chunks = []
    for start in range(0, len(index), chunk_hours):
        stop = min(start + chunk_hours, len(index))
        chunk_index = index[start:stop]
        print(f"Optimiere {chunk_index[0]} bis {chunk_index[-1]} ({len(chunk_index)} h)")
        energy_system = build_energy_system(
            chunk_index,
            demand.reindex(chunk_index),
            pv_cf.reindex(chunk_index),
            wind_cf.reindex(chunk_index),
        )
        model = solve_model(energy_system, solver)
        chunks.append(collect_timeseries(model, chunk_index, demand.reindex(chunk_index)))
    return pd.concat(chunks).sort_index()


def write_plot(timeseries: pd.DataFrame, output_dir: Path, week: int):
    start = timeseries.index.min() + pd.Timedelta(days=(week - 1) * 7)
    end = start + pd.Timedelta(days=7)
    view = timeseries.loc[start:end]
    stack_columns = ["PV_MW", "Wind_MW", "Braunkohle_MW", "Steinkohle_MW", "Gas_MW", "Import_Lastabwurf_MW"]
    colors = ["#f5c542", "#5ca8d7", "#7f5a3a", "#4b4b4b", "#c95b40", "#d62728"]
    width, height = 1200, 650
    left, right, top, bottom = 80, 30, 60, 90
    plot_w = width - left - right
    plot_h = height - top - bottom
    max_y = max(view["Last_MW"].max(), view[stack_columns].sum(axis=1).max()) * 1.08

    def x_pos(i: int) -> float:
        return left + (i / max(len(view) - 1, 1)) * plot_w

    def y_pos(value: float) -> float:
        return top + plot_h - (value / max_y) * plot_h

    cumulative = pd.Series(0.0, index=view.index)
    areas = []
    for column, color in zip(stack_columns, colors):
        lower = cumulative.copy()
        upper = cumulative + view[column]
        upper_points = [(x_pos(i), y_pos(v)) for i, v in enumerate(upper)]
        lower_points = [(x_pos(i), y_pos(v)) for i, v in reversed(list(enumerate(lower)))]
        points = " ".join(f"{x:.1f},{y:.1f}" for x, y in upper_points + lower_points)
        areas.append(f'<polygon points="{points}" fill="{color}" fill-opacity="0.9"/>')
        cumulative = upper

    load_points = " ".join(f"{x_pos(i):.1f},{y_pos(v):.1f}" for i, v in enumerate(view["Last_MW"]))
    y_ticks = []
    for value in np.linspace(0, max_y, 6):
        y = y_pos(value)
        y_ticks.append(
            f'<line x1="{left}" y1="{y:.1f}" x2="{width - right}" y2="{y:.1f}" stroke="#dddddd"/>'
            f'<text x="{left - 10}" y="{y + 4:.1f}" text-anchor="end" font-size="12">{value / 1000:.0f}</text>'
        )

    legend_items = []
    for i, (column, color) in enumerate(zip(stack_columns + ["Last_MW"], colors + ["#000000"])):
        x = left + (i % 4) * 230
        y = height - 55 + (i // 4) * 22
        legend_items.append(
            f'<rect x="{x}" y="{y - 11}" width="16" height="10" fill="{color}"/>'
            f'<text x="{x + 24}" y="{y - 2}" font-size="13">{column}</text>'
        )

    day_labels = []
    for i, timestamp in enumerate(view.index):
        if timestamp.hour == 0:
            x = x_pos(i)
            day_labels.append(
                f'<line x1="{x:.1f}" y1="{top}" x2="{x:.1f}" y2="{top + plot_h}" stroke="#eeeeee"/>'
                f'<text x="{x:.1f}" y="{top + plot_h + 22}" text-anchor="middle" font-size="12">'
                f'{timestamp.strftime("%d.%m.")}</text>'
            )

    svg = f"""<svg xmlns="http://www.w3.org/2000/svg" width="{width}" height="{height}" viewBox="0 0 {width} {height}">
<rect width="100%" height="100%" fill="white"/>
<text x="{left}" y="32" font-size="22" font-family="Arial, sans-serif">Kraftwerkseinsatz in Kalenderwoche {week}</text>
<text x="22" y="{top + plot_h / 2:.1f}" transform="rotate(-90 22 {top + plot_h / 2:.1f})" text-anchor="middle" font-size="14" font-family="Arial, sans-serif">Leistung [GW]</text>
<g font-family="Arial, sans-serif">
{''.join(y_ticks)}
{''.join(day_labels)}
<line x1="{left}" y1="{top + plot_h}" x2="{width - right}" y2="{top + plot_h}" stroke="#333333"/>
<line x1="{left}" y1="{top}" x2="{left}" y2="{top + plot_h}" stroke="#333333"/>
{''.join(areas)}
<polyline points="{load_points}" fill="none" stroke="#000000" stroke-width="2.4"/>
{''.join(legend_items)}
</g>
</svg>
"""
    (output_dir / "dispatch_plot.svg").write_text(svg, encoding="utf-8")
    write_plot_pdf(
        output_dir / "dispatch_plot.pdf",
        view,
        stack_columns,
        colors,
        week,
        width,
        height,
        left,
        right,
        top,
        bottom,
        max_y,
    )


def write_plot_pdf(
    path: Path,
    view: pd.DataFrame,
    stack_columns: list[str],
    colors: list[str],
    week: int,
    width: int,
    height: int,
    left: int,
    right: int,
    top: int,
    bottom: int,
    max_y: float,
):
    plot_w = width - left - right
    plot_h = height - top - bottom

    def x_pos(i: int) -> float:
        return left + (i / max(len(view) - 1, 1)) * plot_w

    def y_pos(value: float) -> float:
        return top + plot_h - (value / max_y) * plot_h

    def pdf_y(svg_y: float) -> float:
        return height - svg_y

    c = canvas.Canvas(str(path), pagesize=(width, height))
    c.setFillColor(white)
    c.rect(0, 0, width, height, stroke=0, fill=1)

    c.setFillColor(black)
    c.setFont("Helvetica", 22)
    c.drawString(left, height - 32, f"Kraftwerkseinsatz in Kalenderwoche {week}")

    c.saveState()
    c.translate(22, pdf_y(top + plot_h / 2))
    c.rotate(90)
    c.setFont("Helvetica", 14)
    c.drawCentredString(0, 0, "Leistung [GW]")
    c.restoreState()

    c.setStrokeColor(HexColor("#dddddd"))
    c.setLineWidth(0.8)
    c.setFillColor(black)
    c.setFont("Helvetica", 12)
    for value in np.linspace(0, max_y, 6):
        y = y_pos(value)
        c.line(left, pdf_y(y), width - right, pdf_y(y))
        c.drawRightString(left - 10, pdf_y(y + 4), f"{value / 1000:.0f}")

    c.setStrokeColor(HexColor("#eeeeee"))
    for i, timestamp in enumerate(view.index):
        if timestamp.hour == 0:
            x = x_pos(i)
            c.line(x, pdf_y(top), x, pdf_y(top + plot_h))
            c.setFillColor(black)
            c.drawCentredString(x, pdf_y(top + plot_h + 22), timestamp.strftime("%d.%m."))

    cumulative = pd.Series(0.0, index=view.index)
    for column, color in zip(stack_columns, colors):
        lower = cumulative.copy()
        upper = cumulative + view[column]
        upper_points = [(x_pos(i), pdf_y(y_pos(v))) for i, v in enumerate(upper)]
        lower_points = [(x_pos(i), pdf_y(y_pos(v))) for i, v in reversed(list(enumerate(lower)))]
        points = upper_points + lower_points
        area = c.beginPath()
        area.moveTo(*points[0])
        for point in points[1:]:
            area.lineTo(*point)
        area.close()
        c.setFillColor(HexColor(color))
        c.drawPath(area, stroke=0, fill=1)
        cumulative = upper

    c.setStrokeColor(black)
    c.setLineWidth(1)
    c.line(left, pdf_y(top + plot_h), width - right, pdf_y(top + plot_h))
    c.line(left, pdf_y(top), left, pdf_y(top + plot_h))

    load = c.beginPath()
    load.moveTo(x_pos(0), pdf_y(y_pos(view["Last_MW"].iloc[0])))
    for i, value in enumerate(view["Last_MW"].iloc[1:], start=1):
        load.lineTo(x_pos(i), pdf_y(y_pos(value)))
    c.setStrokeColor(black)
    c.setLineWidth(2.4)
    c.drawPath(load, stroke=1, fill=0)

    c.setFont("Helvetica", 13)
    for i, (column, color) in enumerate(zip(stack_columns + ["Last_MW"], colors + ["#000000"])):
        x = left + (i % 4) * 230
        y = height - 55 + (i // 4) * 22
        c.setFillColor(HexColor(color))
        c.rect(x, pdf_y(y - 1), 16, 10, stroke=0, fill=1)
        c.setFillColor(black)
        c.drawString(x + 24, pdf_y(y - 9), column)

    c.showPage()
    c.save()


def main():
    parser = argparse.ArgumentParser(description="Einfaches deutsches Energiesystemmodell mit oemof.solph.")
    parser.add_argument("--year", type=int, default=2026, help="Simulationsjahr")
    parser.add_argument("--solver", default="highs", help="Pyomo-Solver, z.B. highs oder appsi_highs")
    parser.add_argument("--output", type=Path, default=Path("results"), help="Ausgabeordner")
    parser.add_argument("--plot-week", type=int, default=3, help="Kalenderwoche fuer die Dispatch-Grafik")
    parser.add_argument(
        "--chunk-days",
        type=int,
        default=31,
        help="Optimierungshorizont in Tagen; 0 loest das ganze Jahr in einem Modell.",
    )
    args = parser.parse_args()

    index = make_time_index(args.year)
    demand = synthetic_demand(index)
    pv_cf = synthetic_pv_profile(index)
    wind_cf = synthetic_wind_profile(index)

    timeseries = solve_in_chunks(index, demand, pv_cf, wind_cf, args.solver, args.chunk_days)
    summary = summarize(timeseries)

    args.output.mkdir(parents=True, exist_ok=True)
    timeseries.to_csv(args.output / "timeseries.csv", index_label="Zeit")
    summary.to_csv(args.output / "summary.csv", index=False)
    write_plot(timeseries, args.output, args.plot_week)

    total_row = summary.loc[summary["Anlage"] == "Gesamt"].iloc[0]
    total_cost = total_row["Variable_Kosten_EUR"]
    total_co2 = total_row["CO2_t"]
    print("\nErgebnis")
    print("========")
    print(summary.to_string(index=False, formatters={
        "Installierte_Leistung_MW": "{:,.0f}".format,
        "Energie_MWh": "{:,.0f}".format,
        "Variable_Kosten_EUR": "{:,.0f}".format,
        "CO2_t": "{:,.0f}".format,
    }))
    print(f"\nGesamtkosten: {total_cost:,.0f} EUR")
    print(f"CO2-Emissionen: {total_co2:,.0f} t")
    print(f"Ergebnisse gespeichert in: {args.output.resolve()}")


if __name__ == "__main__":
    main()
