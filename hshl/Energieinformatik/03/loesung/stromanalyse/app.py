import pandas as pd
from flask import Flask

app = Flask(__name__)


@app.get("/average-price")
def average_price():
    data = pd.read_csv("preise.csv")
    return {"average": round(data["price"].mean(), 2), "unit": "EUR/MWh"}


app.run(host="0.0.0.0", port=5000)
