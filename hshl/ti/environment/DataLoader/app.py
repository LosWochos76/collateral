from flask import Flask, render_template, redirect
import Bundesliga
import Wetter

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
    return redirect("/")

@app.route("/wetter")
def wetter():
    Wetter.clear()
    Wetter.load_stationen()
    Wetter.load_messungen()
    return redirect("/")

app.run(port=5000)