from __future__ import annotations

import threading
import unittest

from modbus_meter.client import ModbusTcpClient
from modbus_meter.model import SmartMeterModel
from modbus_meter.protocol import ModbusExceptionResponse
from modbus_meter.server import MeterTcpServer


class IntegrationTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls) -> None:
        cls.server = MeterTcpServer(("127.0.0.1", 0), SmartMeterModel("pv", 60))
        cls.thread = threading.Thread(target=cls.server.serve_forever, daemon=True)
        cls.thread.start()
        cls.port = cls.server.server_address[1]

    @classmethod
    def tearDownClass(cls) -> None:
        cls.server.shutdown()
        cls.server.server_close()
        cls.thread.join(timeout=2)

    def test_reads_complete_register_map(self) -> None:
        registers = ModbusTcpClient("127.0.0.1", self.port).read_input_registers(0, 13)
        self.assertEqual(len(registers), 13)
        self.assertGreater(registers[0], 2000)
        self.assertTrue(registers[12] & 0x0001)

    def test_illegal_address_returns_modbus_exception(self) -> None:
        with self.assertRaises(ModbusExceptionResponse) as caught:
            ModbusTcpClient("127.0.0.1", self.port).read_input_registers(100, 1)
        self.assertEqual(caught.exception.code, 0x02)

    def test_wrong_unit_id_returns_gateway_exception(self) -> None:
        with self.assertRaises(ModbusExceptionResponse) as caught:
            ModbusTcpClient("127.0.0.1", self.port, unit_id=7).read_input_registers(0, 1)
        self.assertEqual(caught.exception.code, 0x0B)


if __name__ == "__main__":
    unittest.main()
