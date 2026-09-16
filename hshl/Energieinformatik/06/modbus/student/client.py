"""Arbeitsvorlage: Modbus-Smart-Meter auslesen.

Die vier TODO-Bloecke gehoeren zur Uebung. REGISTERPLAN.md enthaelt alle
fachlichen Angaben; modbus_meter/protocol.py bietet passende Hilfsfunktionen.
"""

from __future__ import annotations

import argparse
import json

from modbus_meter.client import ModbusTcpClient
from modbus_meter.protocol import register_to_int16, registers_to_int32, registers_to_uint32


def decode_meter(registers: list[int]) -> dict[str, object]:
    if len(registers) != 13:
        raise ValueError("Es werden genau 13 Register erwartet")

    # TODO 2: Mehrwortwerte anhand des Registerplans dekodieren.
    active_power_w = 0
    reactive_power_var = 0
    import_energy_wh = 0
    export_energy_wh = 0

    # TODO 3: Skalierungsfaktoren und Vorzeichen anwenden.
    voltage_v = 0.0
    current_a = 0.0
    frequency_hz = 0.0
    power_factor = 0.0

    # TODO 4: Die Statusbits mit Bitmasken auswerten.
    status_word = registers[12]
    flags = {
        "grid_present": False,
        "importing": False,
        "exporting": False,
        "overvoltage": False,
    }

    return {
        "voltage_v": voltage_v,
        "current_a": current_a,
        "active_power_w": active_power_w,
        "reactive_power_var": reactive_power_var,
        "import_energy_wh": import_energy_wh,
        "export_energy_wh": export_energy_wh,
        "frequency_hz": frequency_hz,
        "power_factor": power_factor,
        "status": status_word,
        "flags": flags,
    }


def main() -> None:
    parser = argparse.ArgumentParser(description="Arbeitsvorlage fuer den Modbus-Smart-Meter-Client")
    parser.add_argument("--host", default="127.0.0.1")
    parser.add_argument("--port", type=int, default=1502)
    parser.add_argument("--unit-id", type=int, default=1)
    args = parser.parse_args()

    client = ModbusTcpClient(args.host, args.port, args.unit_id)
    # TODO 1: Input Register 0 bis einschliesslich 12 lesen.
    registers: list[int] = []
    print(json.dumps(decode_meter(registers), indent=2))


if __name__ == "__main__":
    main()
