"""Send an illustrative AS4-shaped message; run server.py first."""
import sys
from urllib.request import Request, urlopen
from xml.etree import ElementTree as ET
from uuid import uuid4

SOAP = "http://www.w3.org/2003/05/soap-envelope"
EBMS = "http://docs.oasis-open.org/ebxml-msg/ebms/v3.0/ns/core/200704/"

# Dieselbe MSCONS-Zaehlerstandsmeldung wie in Kapitel 4 (Messstellenbetreiber
# 9900204000002 an Lieferant 4012345000023): AS4 transportiert das
# EDIFACT-Dokument unveraendert als Payload, es wird nicht umkodiert.
MSCONS = (
    "UNH+1+MSCONS:D:04B:UN:2.4c'"
    "BGM+7+MSC0001+9'"
    "DTM+137:202509151030?+00:303'"
    "NAD+MS+9900204000002::293'"
    "NAD+MR+4012345000023::293'"
    "UNS+D'"
    "NAD+DP'"
    "LOC+172+DE0001234567890123456789012345678'"
    "LIN+1'"
    "PIA+5+1-1?:1.8.1:SRW'"
    "QTY+220:12345.678:KWH'"
    "DTM+163:202509010000?+00:303'"
    "DTM+164:202509302400?+00:303'"
    "UNT+14+1'"
)

message_id = sys.argv[1] if len(sys.argv) > 1 else str(uuid4())
envelope = ET.Element(f"{{{SOAP}}}Envelope")
header = ET.SubElement(envelope, f"{{{SOAP}}}Header")
user_message = ET.SubElement(header, f"{{{EBMS}}}UserMessage")
info = ET.SubElement(user_message, f"{{{EBMS}}}MessageInfo")
ET.SubElement(info, f"{{{EBMS}}}MessageId").text = message_id
body = ET.SubElement(envelope, f"{{{SOAP}}}Body")
ET.SubElement(body, "{urn:teaching:as4}Payload").text = MSCONS
ET.indent(envelope)

print("Gesendet:")
print(ET.tostring(envelope, encoding="unicode"))

request = Request(
    "http://127.0.0.1:8000/as4",
    data=ET.tostring(envelope, encoding="utf-8", xml_declaration=True),
    headers={"Content-Type": "application/soap+xml; charset=utf-8"},
)
with urlopen(request, timeout=5) as response:
    receipt = ET.fromstring(response.read())
    ET.indent(receipt)
    print(f"\nEmpfangen (HTTP {response.status}):")
    print(ET.tostring(receipt, encoding="unicode"))
