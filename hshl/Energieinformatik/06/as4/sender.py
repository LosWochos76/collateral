"""Send an illustrative AS4-shaped message; run server.py first."""
import sys
from urllib.request import Request, urlopen
from xml.etree import ElementTree as ET
from uuid import uuid4

SOAP = "http://www.w3.org/2003/05/soap-envelope"
EBMS = "http://docs.oasis-open.org/ebxml-msg/ebms/v3.0/ns/core/200704/"
message_id = sys.argv[1] if len(sys.argv) > 1 else str(uuid4())
envelope = ET.Element(f"{{{SOAP}}}Envelope")
header = ET.SubElement(envelope, f"{{{SOAP}}}Header")
user_message = ET.SubElement(header, f"{{{EBMS}}}UserMessage")
info = ET.SubElement(user_message, f"{{{EBMS}}}MessageInfo")
ET.SubElement(info, f"{{{EBMS}}}MessageId").text = message_id
body = ET.SubElement(envelope, f"{{{SOAP}}}Body")
ET.SubElement(body, "{urn:teaching:as4}Payload").text = "MSCONS;Zaehlerstand=12345"
request = Request(
    "http://127.0.0.1:8000/as4",
    data=ET.tostring(envelope, encoding="utf-8", xml_declaration=True),
    headers={"Content-Type": "application/soap+xml; charset=utf-8"},
)
with urlopen(request, timeout=5) as response:
    print("HTTP", response.status)
    print(response.read().decode("utf-8"))
