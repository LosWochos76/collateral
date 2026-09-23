"""Threaded Modbus TCP simulator for the HSHL rooftop weather station."""

from __future__ import annotations

import argparse
import socketserver
from typing import cast

from .model import WeatherStationModel
from .protocol import (
    Adu,
    GATEWAY_TARGET_FAILED,
    ILLEGAL_DATA_ADDRESS,
    ILLEGAL_DATA_VALUE,
    ILLEGAL_FUNCTION,
    READ_HOLDING_REGISTERS,
    ModbusProtocolError,
    decode_read_request,
    exception_pdu,
    read_response_pdu,
    recv_adu,
)


class WeatherStationRequestHandler(socketserver.BaseRequestHandler):
    def handle(self) -> None:
        server = cast("WeatherStationTcpServer", self.server)
        while True:
            try:
                request = recv_adu(self.request)
            except (EOFError, ConnectionError, TimeoutError):
                return
            except ModbusProtocolError:
                return

            function = request.pdu[0] if request.pdu else 0
            if request.protocol_id != 0:
                return
            if request.unit_id != server.unit_id:
                response_pdu = exception_pdu(function, GATEWAY_TARGET_FAILED)
            elif function != READ_HOLDING_REGISTERS:
                response_pdu = exception_pdu(function, ILLEGAL_FUNCTION)
            else:
                try:
                    _, address, count = decode_read_request(request.pdu)
                except ModbusProtocolError:
                    response_pdu = exception_pdu(function, ILLEGAL_DATA_VALUE)
                else:
                    registers = server.model.holding_registers()
                    if count < 1 or count > 125:
                        response_pdu = exception_pdu(function, ILLEGAL_DATA_VALUE)
                    elif address + count > len(registers):
                        response_pdu = exception_pdu(function, ILLEGAL_DATA_ADDRESS)
                    else:
                        response_pdu = read_response_pdu(function, registers[address : address + count])

            response = Adu(request.transaction_id, 0, request.unit_id, response_pdu)
            self.request.sendall(response.encode())


class WeatherStationTcpServer(socketserver.ThreadingTCPServer):
    allow_reuse_address = True
    daemon_threads = True

    def __init__(
        self,
        address: tuple[str, int],
        model: WeatherStationModel,
        unit_id: int = 1,
    ) -> None:
        self.model = model
        self.unit_id = unit_id
        super().__init__(address, WeatherStationRequestHandler)


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(description="Simulierte Modbus-TCP-Wetterstation (HSHL-Dach)")
    parser.add_argument("--host", default="127.0.0.1", help="Bind-Adresse (Standard: nur lokal)")
    parser.add_argument("--port", type=int, default=1502)
    parser.add_argument("--unit-id", type=int, default=1)
    parser.add_argument("--speed", type=float, default=60.0, help="Simulationssekunden pro Echtzeitsekunde")
    return parser


def main() -> None:
    args = build_parser().parse_args()
    if not 0 <= args.unit_id <= 247:
        raise SystemExit("--unit-id muss zwischen 0 und 247 liegen")
    model = WeatherStationModel(speed=args.speed)
    with WeatherStationTcpServer((args.host, args.port), model, args.unit_id) as server:
        host, port = server.server_address
        print(f"Wetterstation lauscht auf {host}:{port}, Unit-ID {args.unit_id}")
        print("Abbruch mit Ctrl+C")
        try:
            server.serve_forever()
        except KeyboardInterrupt:
            print("\nServer beendet")


if __name__ == "__main__":
    main()
