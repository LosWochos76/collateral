"""Simulation of the HSHL rooftop weather station.

Values drift as a bounded random walk; the four radiation channels additionally
follow a simulated day/night cycle so they are plausibly zero at night.
"""

from __future__ import annotations

import math
import random
import threading
import time
from dataclasses import dataclass

from .protocol import float32_to_registers


@dataclass(frozen=True)
class WeatherValues:
    ghi_final_min: float
    dhi_final_min: float
    dni_final_min: float
    cmp_ghi: float
    sensor_temp: float
    tair: float
    rh: float
    bp: float
    ws: float
    wsgust: float
    wd: float
    rain_mm: float
    logger_voltage: float
    logger_temp_c: float
    ic: float


class WeatherStationModel:
    """Generates plausible, slowly drifting values for the rooftop weather station."""

    def __init__(self, speed: float = 60.0) -> None:
        if speed <= 0:
            raise ValueError("speed must be positive")
        self.speed = speed
        self._started = time.monotonic()
        self._lock = threading.Lock()
        self._rng = random.Random()
        self._tair = 14.0
        self._rh = 65.0
        self._bp = 1013.0
        self._ws = 3.0
        self._wd = 210.0
        self._logger_voltage = 13.0
        self._logger_temp = 20.0
        self._ic = 0.0
        self._snapshots = 0

    def _simulated_hour(self) -> float:
        elapsed = time.monotonic() - self._started
        simulated_seconds = elapsed * self.speed
        return (simulated_seconds % (24 * 3600)) / 3600

    @staticmethod
    def _sun_elevation_factor(hour: float) -> float:
        return max(0.0, math.sin(math.pi * (hour - 5.5) / 13.0))

    def _walk(self, current: float, low: float, high: float, step: float) -> float:
        value = current + self._rng.gauss(0.0, step)
        return min(high, max(low, value))

    def snapshot(self) -> WeatherValues:
        with self._lock:
            hour = self._simulated_hour()
            sun = self._sun_elevation_factor(hour)
            clouds = self._rng.uniform(0.7, 1.0)

            ghi = max(0.0, 950 * sun * clouds + self._rng.uniform(0, 5))
            dni = max(0.0, 850 * sun * clouds**2)
            dhi = max(0.0, 0.4 * ghi - 0.1 * dni + self._rng.uniform(0, 3))
            cmp_ghi = max(0.0, ghi + self._rng.uniform(-8, 8))

            self._tair = self._walk(self._tair, -8.0, 32.0, 0.15) + 0.01 * sun
            self._rh = self._walk(self._rh, 25.0, 98.0, 0.4)
            self._bp = self._walk(self._bp, 985.0, 1035.0, 0.05)
            self._ws = self._walk(self._ws, 0.0, 12.0, 0.3)
            gust = self._ws + abs(self._rng.gauss(1.5, 1.0))
            self._wd = (self._wd + self._rng.uniform(-8, 8)) % 360.0
            rain = max(0.0, self._rng.gauss(0.0, 0.4)) if self._rng.random() < 0.1 else 0.0
            self._logger_voltage = self._walk(self._logger_voltage, 11.8, 13.8, 0.03)
            self._logger_temp = self._walk(self._logger_temp, -5.0, 45.0, 0.2) + 0.02 * sun
            sensor_temp = self._logger_temp + 3.0 + self._rng.uniform(-1, 1)

            self._snapshots += 1
            if self._snapshots % 200 == 0:
                self._ic += 1.0

            return WeatherValues(
                ghi_final_min=round(ghi, 1),
                dhi_final_min=round(dhi, 1),
                dni_final_min=round(dni, 1),
                cmp_ghi=round(cmp_ghi, 1),
                sensor_temp=round(sensor_temp, 2),
                tair=round(self._tair, 2),
                rh=round(self._rh, 1),
                bp=round(self._bp, 1),
                ws=round(self._ws, 2),
                wsgust=round(gust, 2),
                wd=round(self._wd, 1),
                rain_mm=round(rain, 2),
                logger_voltage=round(self._logger_voltage, 2),
                logger_temp_c=round(self._logger_temp, 2),
                ic=self._ic,
            )

    def holding_registers(self) -> list[int]:
        values = self.snapshot()
        ordered = (
            values.ghi_final_min,
            values.dhi_final_min,
            values.dni_final_min,
            values.cmp_ghi,
            values.sensor_temp,
            values.tair,
            values.rh,
            values.bp,
            values.ws,
            values.wsgust,
            values.wd,
            values.rain_mm,
            values.logger_voltage,
            values.logger_temp_c,
            values.ic,
        )
        registers: list[int] = []
        for value in ordered:
            low, high = float32_to_registers(value)
            registers.extend((low, high))
        return registers
