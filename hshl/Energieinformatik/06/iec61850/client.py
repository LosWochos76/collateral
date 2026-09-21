"""IEC 61850/MMS client for the two official libIEC61850 sample servers."""
import argparse
import sys
import pyiec61850 as iec

WIND_STATUS = "WINDWTG/WTUR1.TurSt.actSt.stVal"
SWITCH = "simpleIOGenericIO/GGIO1.SPCSO1"
SWITCH_STATUS = SWITCH + ".stVal"


def read(connection, reference, kind):
    function = (
        iec.IedConnection_readBooleanValue if kind == "bool"
        else iec.IedConnection_readInt32Value
    )
    value, error = function(connection, reference, iec.IEC61850_FC_ST)
    if error != iec.IED_ERROR_OK:
        raise RuntimeError(f"MMS-Lesen fehlgeschlagen ({reference}): {error}")
    return value


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("action", choices=("wind", "switch"))
    parser.add_argument("value", nargs="?", choices=("on", "off"))
    args = parser.parse_args()
    if args.action == "switch" and args.value is None:
        parser.error("switch benötigt on oder off")
    host = "wind" if args.action == "wind" else "switch"
    connection = iec.IedConnection_create()
    try:
        error = iec.IedConnection_connect(connection, host, 8102)
        if error != iec.IED_ERROR_OK:
            raise RuntimeError(f"MMS-Verbindung fehlgeschlagen: {error}")
        if args.action == "wind":
            print("Turbinenstatus:", read(connection, WIND_STATUS, "int"))
            return
        print("Schalter vorher:", read(connection, SWITCH_STATUS, "bool"))
        control = iec.ControlObjectClient_create(SWITCH, connection)
        if not control:
            raise RuntimeError(f"Steuerobjekt nicht gefunden: {SWITCH}")
        value = iec.MmsValue_newBoolean(args.value == "on")
        try:
            iec.ControlObjectClient_setOrigin(control, None, 3)
            if not iec.ControlObjectClient_operate(control, value, 0):
                raise RuntimeError(
                    f"MMS-Operate abgelehnt: {iec.ControlObjectClient_getLastError(control)}"
                )
        finally:
            iec.MmsValue_delete(value)
            iec.ControlObjectClient_destroy(control)
        print("Schalter danach:", read(connection, SWITCH_STATUS, "bool"))
    finally:
        iec.IedConnection_close(connection)
        iec.IedConnection_destroy(connection)


if __name__ == "__main__":
    try:
        main()
    except RuntimeError as exc:
        print(exc, file=sys.stderr)
        sys.exit(1)
