"""Small, strict Modbus TCP codec used by the teaching project.

Only function 0x03 (Read Holding Registers) and exception responses are needed.
Keeping the codec small makes every byte inspectable in class.
"""

from __future__ import annotations

import socket
import struct
from dataclasses import dataclass

READ_HOLDING_REGISTERS = 0x03
READ_INPUT_REGISTERS = 0x04

ILLEGAL_FUNCTION = 0x01
ILLEGAL_DATA_ADDRESS = 0x02
ILLEGAL_DATA_VALUE = 0x03
GATEWAY_TARGET_FAILED = 0x0B


class ModbusError(Exception):
    """Base class for protocol and transport errors."""


class ModbusProtocolError(ModbusError):
    """A malformed or inconsistent Modbus TCP message was received."""


class ModbusExceptionResponse(ModbusError):
    """The server returned a valid Modbus exception response."""

    def __init__(self, function: int, code: int) -> None:
        super().__init__(f"Modbus exception: function=0x{function:02X}, code=0x{code:02X}")
        self.function = function
        self.code = code


@dataclass(frozen=True)
class Adu:
    transaction_id: int
    protocol_id: int
    unit_id: int
    pdu: bytes

    def encode(self) -> bytes:
        if not 0 <= self.transaction_id <= 0xFFFF:
            raise ValueError("transaction_id must fit in uint16")
        if not 0 <= self.unit_id <= 0xFF:
            raise ValueError("unit_id must fit in uint8")
        if len(self.pdu) > 253:
            raise ValueError("PDU too long")
        length = 1 + len(self.pdu)  # Unit-ID plus PDU
        return struct.pack(">HHHB", self.transaction_id, self.protocol_id, length, self.unit_id) + self.pdu


def recv_exact(sock: socket.socket, size: int) -> bytes:
    chunks: list[bytes] = []
    remaining = size
    while remaining:
        chunk = sock.recv(remaining)
        if not chunk:
            raise EOFError("connection closed while receiving a Modbus message")
        chunks.append(chunk)
        remaining -= len(chunk)
    return b"".join(chunks)


def recv_adu(sock: socket.socket) -> Adu:
    header = recv_exact(sock, 7)
    transaction_id, protocol_id, length, unit_id = struct.unpack(">HHHB", header)
    if length < 2 or length > 254:
        raise ModbusProtocolError(f"invalid MBAP length {length}")
    pdu = recv_exact(sock, length - 1)
    return Adu(transaction_id, protocol_id, unit_id, pdu)


def read_request_pdu(function: int, address: int, count: int) -> bytes:
    if not 0 <= function <= 0xFF:
        raise ValueError("function must fit in uint8")
    if not 0 <= address <= 0xFFFF:
        raise ValueError("address must fit in uint16")
    if not 0 <= count <= 0xFFFF:
        raise ValueError("count must fit in uint16")
    return struct.pack(">BHH", function, address, count)


def decode_read_request(pdu: bytes) -> tuple[int, int, int]:
    if len(pdu) != 5:
        raise ModbusProtocolError("read request PDU must contain exactly 5 bytes")
    return struct.unpack(">BHH", pdu)


def read_response_pdu(function: int, registers: list[int]) -> bytes:
    if len(registers) > 125:
        raise ValueError("at most 125 registers per response")
    payload = b"".join(struct.pack(">H", value) for value in registers)
    return bytes((function, len(payload))) + payload


def decode_read_response(pdu: bytes, expected_function: int, expected_count: int) -> list[int]:
    if len(pdu) < 2:
        raise ModbusProtocolError("response PDU is too short")
    function = pdu[0]
    if function & 0x80:
        if len(pdu) != 2:
            raise ModbusProtocolError("exception response must contain 2 bytes")
        raise ModbusExceptionResponse(function & 0x7F, pdu[1])
    if function != expected_function:
        raise ModbusProtocolError(
            f"unexpected function 0x{function:02X}, expected 0x{expected_function:02X}"
        )
    byte_count = pdu[1]
    if byte_count != expected_count * 2 or len(pdu) != byte_count + 2:
        raise ModbusProtocolError("register byte count does not match request")
    return list(struct.unpack(f">{expected_count}H", pdu[2:]))


def exception_pdu(function: int, code: int) -> bytes:
    return bytes((function | 0x80, code))


def registers_to_uint32(high: int, low: int) -> int:
    return ((high & 0xFFFF) << 16) | (low & 0xFFFF)


def registers_to_int32(high: int, low: int) -> int:
    value = registers_to_uint32(high, low)
    return value - 0x1_0000_0000 if value & 0x8000_0000 else value


def register_to_int16(value: int) -> int:
    value &= 0xFFFF
    return value - 0x1_0000 if value & 0x8000 else value


def uint32_to_registers(value: int) -> tuple[int, int]:
    if not 0 <= value <= 0xFFFF_FFFF:
        raise ValueError("value must fit in uint32")
    return (value >> 16) & 0xFFFF, value & 0xFFFF


def int32_to_registers(value: int) -> tuple[int, int]:
    if not -(2**31) <= value < 2**31:
        raise ValueError("value must fit in int32")
    return uint32_to_registers(value & 0xFFFF_FFFF)


def registers_to_float32(low_register: int, high_register: int) -> float:
    """Combine two registers into an IEEE-754 float, high word at the larger address."""
    raw = ((high_register & 0xFFFF) << 16) | (low_register & 0xFFFF)
    return struct.unpack(">f", struct.pack(">I", raw))[0]


def float32_to_registers(value: float) -> tuple[int, int]:
    """Inverse of registers_to_float32: returns (low_register, high_register)."""
    raw = struct.unpack(">I", struct.pack(">f", value))[0]
    return raw & 0xFFFF, (raw >> 16) & 0xFFFF
