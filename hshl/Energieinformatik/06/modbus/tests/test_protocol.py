from __future__ import annotations

import unittest

from modbus_meter.protocol import (
    Adu,
    ModbusExceptionResponse,
    decode_read_response,
    int32_to_registers,
    read_request_pdu,
    register_to_int16,
    registers_to_int32,
    registers_to_uint32,
)


class ProtocolTests(unittest.TestCase):
    def test_known_read_request_wire_format(self) -> None:
        request = Adu(1, 0, 1, read_request_pdu(4, 0, 13))
        self.assertEqual(request.encode().hex(" "), "00 01 00 00 00 06 01 04 00 00 00 0d")

    def test_signed_and_unsigned_register_conversion(self) -> None:
        self.assertEqual(registers_to_uint32(0x1234, 0x5678), 0x12345678)
        self.assertEqual(registers_to_int32(0xFFFF, 0xF830), -2000)
        self.assertEqual(int32_to_registers(-2000), (0xFFFF, 0xF830))
        self.assertEqual(register_to_int16(0xFC22), -990)

    def test_exception_response_is_raised(self) -> None:
        with self.assertRaises(ModbusExceptionResponse) as caught:
            decode_read_response(bytes((0x84, 0x02)), 4, 1)
        self.assertEqual(caught.exception.code, 2)


if __name__ == "__main__":
    unittest.main()
