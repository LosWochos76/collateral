"""Musterloesung fuer den Modbus-Smart-Meter-Client."""

from __future__ import annotations

import argparse
import csv
import json
import time
from datetime import datetime, timezone
from pathlib import Path

from modbus_meter.client import ModbusTcpClient
from modbus_meter.protocol import register_to_int16, registers_to_int32, registers_to_uint32


def decode_meter(registers: list[int]) -> dict[str, object]:
    if len(registers) != 13:
        raise ValueError("Es werden genau 13 Register erwartet")
    status = registers[12]
    return {
        "voltage_v": registers[0] * 0.1,
        "current_a": registers[1] * 0.01,
        "active_power_w": registers_to_int32(registers[2], registers[3]),
        "reactive_power_var": registers_to_int32(registers[4], registers[5]),
        "import_energy_wh": registers_to_uint32(registers[6], registers[7]),
        "export_energy_wh": registers_to_uint32(registers[8], registers[9]),
        "frequency_hz": registers[10] * 0.01,
        "power_factor": register_to_int16(registers[11]) * 0.001,
        "status": status,
        "flags": {
            "grid_present": bool(status & 0x0001),
            "importing": bool(status & 0x0002),
            "exporting": bool(status & 0x0004),
            "overvoltage": bool(status & 0x0008),
        },
    }


def csv_row(timestamp: str, values: dict[str, object]) -> dict[str, object]:
    return {"timestamp": timestamp, **{k: v for k, v in values.items() if k != "flags"}}


def main() -> None:
    parser = argparse.ArgumentParser(description="Musterloesung: Modbus Smart Meter auslesen")
    parser.add_argument("--host", default="127.0.0.1")
    parser.add_argument("--port", type=int, default=1502)
    parser.add_argument("--unit-id", type=int, default=1)
    parser.add_argument("--interval", type=float, default=0.0, help="Polling-Intervall in Sekunden")
    parser.add_argument("--count", type=int, default=1, help="Anzahl Messungen; 0 bedeutet unbegrenzt")
    parser.add_argument("--csv", type=Path, help="Messwerte zusaetzlich als CSV schreiben")
    args = parser.parse_args()
    if args.interval < 0 or args.count < 0:
        parser.error("--interval und --count duerfen nicht negativ sein")

    client = ModbusTcpClient(args.host, args.port, args.unit_id)
    csv_file = args.csv.open("w", newline="", encoding="utf-8") if args.csv else None
    writer = None
    try:
        sample = 0
        while args.count == 0 or sample < args.count:
            timestamp = datetime.now(timezone.utc).isoformat()
            values = decode_meter(client.read_input_registers(0, 13))
            print(json.dumps({"timestamp": timestamp, **values}, ensure_ascii=False))
            if csv_file:
                row = csv_row(timestamp, values)
                if writer is None:
                    writer = csv.DictWriter(csv_file, fieldnames=list(row))
                    writer.writeheader()
                writer.writerow(row)
                csv_file.flush()
            sample += 1
            if args.count == 0 or sample < args.count:
                time.sleep(args.interval)
    except (ConnectionError, TimeoutError, OSError) as error:
        raise SystemExit(f"Verbindung fehlgeschlagen: {error}") from error
    finally:
        if csv_file:
            csv_file.close()


if __name__ == "__main__":
    main()
