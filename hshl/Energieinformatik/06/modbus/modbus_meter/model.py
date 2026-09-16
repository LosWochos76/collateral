"""Deterministic, time-varying smart-meter model."""

from __future__ import annotations

import math
import threading
import time
from dataclasses import dataclass

from .protocol import int32_to_registers, uint32_to_registers


@dataclass(frozen=True)
class MeterValues:
    voltage_v: float
    current_a: float
    active_power_w: int
    reactive_power_var: int
    import_energy_wh: int
    export_energy_wh: int
    frequency_hz: float
    power_factor: float
    status: int


class SmartMeterModel:
    """Generate plausible values and integrate import/export energy."""

    def __init__(self, profile: str = "pv", speed: float = 60.0) -> None:
        if profile not in {"household", "pv", "charging"}:
            raise ValueError(f"unknown profile {profile!r}")
        if speed <= 0:
            raise ValueError("speed must be positive")
        self.profile = profile
        self.speed = speed
        self._started = time.monotonic()
        self._last_update = self._started
        self._import_wh = 12_345_000.0
        self._export_wh = 2_345_000.0
        self._lock = threading.Lock()

    def _power_at(self, simulated_seconds: float) -> float:
        day = 24 * 3600
        hour = (simulated_seconds % day) / 3600
        household = 420 + 180 * math.sin(2 * math.pi * simulated_seconds / 4200)
        household += 1100 * math.exp(-0.5 * ((hour - 7.5) / 1.2) ** 2)
        household += 1700 * math.exp(-0.5 * ((hour - 19.0) / 1.8) ** 2)

        if self.profile == "household":
            return household
        if self.profile == "pv":
            solar = 5200 * max(0.0, math.sin(math.pi * (hour - 6) / 12)) ** 1.7
            return household - solar

        # A wallbox cycles between 1.4 kW and 7.4 kW to make polling visible.
        wallbox = 4400 + 3000 * math.sin(2 * math.pi * simulated_seconds / 900)
        return household + wallbox

    def snapshot(self) -> MeterValues:
        with self._lock:
            now = time.monotonic()
            elapsed_real = now - self._started
            simulated_seconds = elapsed_real * self.speed + 12 * 3600
            power = self._power_at(simulated_seconds)

            delta_hours = (now - self._last_update) * self.speed / 3600
            if power >= 0:
                self._import_wh += power * delta_hours
            else:
                self._export_wh += -power * delta_hours
            self._last_update = now

            voltage = 230.0 + 1.8 * math.sin(2 * math.pi * simulated_seconds / 600)
            frequency = 50.0 + 0.025 * math.sin(2 * math.pi * simulated_seconds / 180)
            pf = 0.96 if power >= 0 else -0.99
            apparent_power = max(abs(power) / max(abs(pf), 0.1), 1.0)
            current = apparent_power / voltage
            reactive_sign = 1 if power >= 0 else -1
            reactive = reactive_sign * math.sqrt(max(apparent_power**2 - power**2, 0.0))

            status = 0x0001
            status |= 0x0002 if power >= 0 else 0x0004
            if voltage > 253:
                status |= 0x0008

            return MeterValues(
                voltage_v=voltage,
                current_a=current,
                active_power_w=round(power),
                reactive_power_var=round(reactive),
                import_energy_wh=int(self._import_wh),
                export_energy_wh=int(self._export_wh),
                frequency_hz=frequency,
                power_factor=pf,
                status=status,
            )

    def input_registers(self) -> list[int]:
        values = self.snapshot()
        active = int32_to_registers(values.active_power_w)
        reactive = int32_to_registers(values.reactive_power_var)
        imported = uint32_to_registers(values.import_energy_wh)
        exported = uint32_to_registers(values.export_energy_wh)
        return [
            round(values.voltage_v * 10),
            round(values.current_a * 100),
            *active,
            *reactive,
            *imported,
            *exported,
            round(values.frequency_hz * 100),
            round(values.power_factor * 1000) & 0xFFFF,
            values.status,
        ]
