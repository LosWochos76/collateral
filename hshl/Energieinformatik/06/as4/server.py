"""AS4 teaching model: SOAP/ebMS-shaped request and technical receipt.

No signature, encryption, certificate or full AS4 profile validation.
"""
from http.server import BaseHTTPRequestHandler, HTTPServer
from xml.etree import ElementTree as ET

SOAP = "http://www.w3.org/2003/05/soap-envelope"
EBMS = "http://docs.oasis-open.org/ebxml-msg/ebms/v3.0/ns/core/200704/"
SEEN = set()


class Handler(BaseHTTPRequestHandler):
    def do_POST(self):
        if self.path != "/as4":
            self.send_error(404)
            return
        try:
            length = int(self.headers.get("Content-Length", "0"))
            if not 0 < length <= 100_000:
                raise ValueError("ungültige Nachrichtenlänge")
            root = ET.fromstring(self.rfile.read(length))
            message_id = root.findtext(f".//{{{EBMS}}}MessageId")
            payload = root.findtext(".//{urn:teaching:as4}Payload")
            if not message_id or not payload:
                raise ValueError("MessageId oder Payload fehlt")
        except (ET.ParseError, ValueError) as exc:
            self.send_error(400, str(exc))
            return
        duplicate = message_id in SEEN
        SEEN.add(message_id)
        print("Duplikat" if duplicate else "Empfangen", message_id, payload)
        envelope = ET.Element(f"{{{SOAP}}}Envelope")
        body = ET.SubElement(envelope, f"{{{SOAP}}}Body")
        receipt = ET.SubElement(body, f"{{{EBMS}}}Receipt")
        ET.SubElement(receipt, f"{{{EBMS}}}RefToMessageId").text = message_id
        ET.SubElement(receipt, "{urn:teaching:as4}Duplicate").text = str(duplicate).lower()
        response = ET.tostring(envelope, encoding="utf-8", xml_declaration=True)
        self.send_response(200)
        self.send_header("Content-Type", "application/soap+xml; charset=utf-8")
        self.send_header("Content-Length", str(len(response)))
        self.end_headers()
        self.wfile.write(response)


HTTPServer(("127.0.0.1", 8000), Handler).serve_forever()
