"""Fiktive Ladevorgaenge-API: das REST-Beispiel aus Kapitel 6 lauffaehig gemacht."""
from itertools import count

from flask import Flask, abort, jsonify, request

app = Flask(__name__)
ladevorgaenge: dict[int, dict] = {}
next_id = count(1)


@app.get("/ladevorgaenge")
def list_ladevorgaenge():
    return jsonify(list(ladevorgaenge.values()))


@app.post("/ladevorgaenge")
def create_ladevorgang():
    data = request.get_json(force=True, silent=True) or {}
    if "zaehlpunkt" not in data:
        abort(400, description="zaehlpunkt fehlt")
    ladevorgang_id = next(next_id)
    ladevorgang = {"id": ladevorgang_id, "zaehlpunkt": data["zaehlpunkt"], "kwh": None}
    ladevorgaenge[ladevorgang_id] = ladevorgang
    return jsonify(ladevorgang), 201


@app.get("/ladevorgaenge/<int:ladevorgang_id>")
def get_ladevorgang(ladevorgang_id):
    ladevorgang = ladevorgaenge.get(ladevorgang_id)
    if ladevorgang is None:
        abort(404, description="Ladevorgang nicht gefunden")
    return jsonify(ladevorgang)


@app.put("/ladevorgaenge/<int:ladevorgang_id>")
def update_ladevorgang(ladevorgang_id):
    ladevorgang = ladevorgaenge.get(ladevorgang_id)
    if ladevorgang is None:
        abort(404, description="Ladevorgang nicht gefunden")
    data = request.get_json(force=True, silent=True) or {}
    if "kwh" in data:
        ladevorgang["kwh"] = data["kwh"]
    return jsonify(ladevorgang)


if __name__ == "__main__":
    app.run(host="127.0.0.1", port=5050)
