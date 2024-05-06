from flask import Flask, render_template, redirect
import Bundesliga

app = Flask(__name__)

@app.route("/")
def index():
    return render_template("index.html")

@app.route("/bundesliga")
def bundesliga():
    Bundesliga.clear_vereine()
    Bundesliga.load_vereine("2023", 1)
    Bundesliga.load_vereine("2023", 2)

    Bundesliga.clear_spiele()
    Bundesliga.load_spiele("2023", 1)
    Bundesliga.load_spiele("2023", 2)
    return redirect("/")

app.run(port=5000)