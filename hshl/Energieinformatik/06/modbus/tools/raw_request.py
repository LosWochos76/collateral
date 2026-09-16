"""Send one Modbus request and display each MBAP/PDU field."""

from __future__ import annotations

import argparse
import socket

from modbus_meter.protocol import Adu, read_request_pdu, recv_adu


def spaced_hex(data: bytes) -> str:
    return " ".join(f"{byte:02X}" for byte in data)


def main() -> None:
    parser = argparse.ArgumentParser(description="Modbus-TCP-Telegramm sichtbar machen")
    parser.add_argument("--host", default="127.0.0.1")
    parser.add_argument("--port", type=int, default=1502)
    parser.add_argument("--unit-id", type=int, default=1)
    parser.add_argument("--function", type=int, default=4)
    parser.add_argument("--address", type=int, default=0)
    parser.add_argument("--count", type=int, default=13)
    args = parser.parse_args()

    request = Adu(1, 0, args.unit_id, read_request_pdu(args.function, args.address, args.count))
    wire_request = request.encode()
    print("REQUEST")
    print(f"  Bytes:          {spaced_hex(wire_request)}")
    print(f"  Transaction-ID: {request.transaction_id}")
    print(f"  Protocol-ID:    {request.protocol_id}")
    print(f"  Length:         {len(request.pdu) + 1}")
    print(f"  Unit-ID:        {request.unit_id}")
    print(f"  Function:       0x{args.function:02X}")
    print(f"  Startadresse:   {args.address}")
    print(f"  Anzahl:         {args.count}")

    with socket.create_connection((args.host, args.port), timeout=3) as sock:
        sock.sendall(wire_request)
        response = recv_adu(sock)
    wire_response = response.encode()
    print("\nRESPONSE")
    print(f"  Bytes:          {spaced_hex(wire_response)}")
    print(f"  Transaction-ID: {response.transaction_id}")
    print(f"  Protocol-ID:    {response.protocol_id}")
    print(f"  Length:         {len(response.pdu) + 1}")
    print(f"  Unit-ID:        {response.unit_id}")
    print(f"  Function:       0x{response.pdu[0]:02X}")
    if response.pdu[0] & 0x80:
        print(f"  Exception-Code: 0x{response.pdu[1]:02X}")
    else:
        print(f"  Byte Count:     {response.pdu[1]}")
        print(f"  Nutzdaten:      {spaced_hex(response.pdu[2:])}")


if __name__ == "__main__":
    main()
