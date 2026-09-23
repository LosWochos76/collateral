"""Minimal synchronous Modbus TCP client used by the exercises."""

from __future__ import annotations

import itertools
import socket

from .protocol import (
    Adu,
    READ_HOLDING_REGISTERS,
    ModbusProtocolError,
    decode_read_response,
    read_request_pdu,
    recv_adu,
)


class ModbusTcpClient:
    def __init__(self, host: str, port: int = 1502, unit_id: int = 1, timeout: float = 3.0) -> None:
        self.host = host
        self.port = port
        self.unit_id = unit_id
        self.timeout = timeout
        self._transactions = itertools.cycle(range(1, 0x10000))

    def read_holding_registers(self, address: int, count: int) -> list[int]:
        transaction_id = next(self._transactions)
        request = Adu(
            transaction_id=transaction_id,
            protocol_id=0,
            unit_id=self.unit_id,
            pdu=read_request_pdu(READ_HOLDING_REGISTERS, address, count),
        )
        with socket.create_connection((self.host, self.port), self.timeout) as sock:
            sock.settimeout(self.timeout)
            sock.sendall(request.encode())
            response = recv_adu(sock)
        if response.protocol_id != 0:
            raise ModbusProtocolError("response has a non-zero protocol identifier")
        if response.transaction_id != transaction_id:
            raise ModbusProtocolError("response transaction does not match request")
        if response.unit_id != self.unit_id:
            raise ModbusProtocolError("response Unit-ID does not match request")
        return decode_read_response(response.pdu, READ_HOLDING_REGISTERS, count)
