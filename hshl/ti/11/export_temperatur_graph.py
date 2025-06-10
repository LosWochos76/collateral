import pandas as pd
import seaborn as sns
import matplotlib
import matplotlib.pyplot as plt
from sqlalchemy import create_engine

# Setze das Backend für matplotlib (damit plt.show() auch außerhalb PyCharm funktioniert)
matplotlib.use('TkAgg')

# Verbindung zur Datenbank
engine = create_engine("postgresql+psycopg2://postgres:hshl@localhost:5432/postgres")
query = "select zeitpunkt, temperatur_2m from wetter.messungen where stations_id=1303 order by zeitpunkt;"
df = pd.read_sql(query, engine)

# Zeitpunkt umwandeln und sortieren
df["zeitpunkt"] = pd.to_datetime(df["zeitpunkt"])
df = df.sort_values("zeitpunkt")
df["temperatur_1woche_glatt"] = df["temperatur_2m"].rolling(window=168, center=True).mean()

sns.set(style="darkgrid")
plt.figure(figsize=(14, 6))

sns.lineplot(data=df, x="zeitpunkt", y="temperatur_2m", label="Original", alpha=0.4)
sns.lineplot(data=df, x="zeitpunkt", y="temperatur_1woche_glatt", label="1Woche geglättet", linewidth=2)

# Titel & Achsen
plt.title("Stündliche Temperaturverläufe mit 1-Woche-Glättung – Station 1303")
plt.xlabel("Zeitpunkt")
plt.ylabel("Temperatur (°C)")
plt.legend()
plt.tight_layout()

# Speichern oder anzeigen
plt.savefig("temperaturverlauf.png")
# plt.show()