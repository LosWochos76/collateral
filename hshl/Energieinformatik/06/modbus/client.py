"""Beispiel: einen einzelnen Messwert der Wetterstation lesen.

Vollstaendiges, lauffaehiges Beispiel fuer die Lufttemperatur (Referenz 40011,
PDU-Adresse 10/11). Alle weiteren Messwerte aus REGISTERPLAN.md zu lesen und
zu interpretieren ist Teil der Uebung.
"""

from __future__ import annotations

import struct

from pymodbus.client import ModbusTcpClient

client = ModbusTcpClient("127.0.0.1", port=1502)
client.connect()
antwort = client.read_holding_registers(address=10, count=2, device_id=1)
low, high = antwort.registers
client.close()

# Die hoeherwertigen Bytes liegen im Register mit der GROESSEREN Adresse
# (siehe REGISTERPLAN.md) -- das ist nicht die ueblichere Reihenfolge.
raw = (high << 16) | low
tair = struct.unpack(">f", struct.pack(">I", raw))[0]

print(f"Lufttemperatur: {tair:.1f} °C")
