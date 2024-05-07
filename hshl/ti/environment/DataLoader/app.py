from flask import Flask, render_template, redirect
import Bundesliga
import Wetter
import Chinook
import Census
import Energy

app = Flask(__name__)

@app.route("/")
def index():
    return render_template("index.html")

@app.route("/bundesliga")
def bundesliga():
    Bundesliga.clear()
    Bundesliga.load_vereine("2023", 1)
    Bundesliga.load_vereine("2023", 2)
    Bundesliga.load_spiele("2023", 1)
    Bundesliga.load_spiele("2023", 2)
    return render_template("done.html")

@app.route("/wetter")
def wetter():
    Wetter.clear()
    Wetter.load_stationen()
    Wetter.load_messungen()
    return render_template("done.html")

@app.route("/chinook")
def chinook():
    Chinook.import_data()
    return render_template("done.html")

@app.route("/census")
def census():
    Census.import_data()
    return render_template("done.html")

@app.route("/energy")
def energy():
    Energy.clear()
    Energy.import_data()
    Energy.fix_data()
    return render_template("done.html")

if __name__ == "__main__":
	app.run(port=5000)
