import pandas as pd


def charging_allowed(price, limit=80):
    return price < limit


data = pd.read_csv("preise.csv")

average = data["price"].mean()
print(f"Mittlerer Preis: {average:.2f} EUR/MWh")

for hour, price in zip(data["hour"], data["price"]):
    if charging_allowed(price):
        print(f"Stunde {hour}: {price} EUR/MWh -> laden")
    else:
        print(f"Stunde {hour}: {price} EUR/MWh -> nicht laden")
