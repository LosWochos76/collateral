from flask import Flask, request, render_template_string, render_template
import Bundesliga
import Wetter
import Chinook
import Census
import Energy
import Datawarehouse
import Movies

app = Flask(__name__)

@app.route("/")
def index():
    return render_template("index.html")

@app.route("/bundesliga")
def bundesliga():
    Bundesliga.clear()
    Bundesliga.load()
    return render_template("done.html")

@app.route("/wetter")
def wetter():
    Wetter.clear()
    Wetter.load()
    return render_template("done.html")

@app.route("/chinook")
def chinook():
    Chinook.load()
    return render_template("done.html")

@app.route("/census")
def census():
    Census.import_data()
    Census.correct_data()
    return render_template("done.html")

@app.route("/energy_clear")
def energy_clear():
    Energy.clear()
    return render_template("done.html")

@app.route("/energy_prices")
def energy_prices():
    Energy.import_prices()
    return render_template("done.html")

@app.route("/energy_renewables")
def energy_renewables():
    Energy.import_renewables()
    return render_template("done.html")

@app.route("/dwh")
def dwh():
    Datawarehouse.clear()
    Datawarehouse.create_dimensions()
    Datawarehouse.load_fakten()
    return render_template("done.html")

@app.route("/movies_import")
def movies_import():
    Movies.import_data()
    return render_template("done.html")

@app.route("/movies_update_embeddings")
def update_embeddings():
    Movies.update_embeddings()
    return render_template("done.html")

@app.route('/movies_search', methods=['GET', 'POST'])
def movies_search():
    return Movies.movies_search()

if __name__ == "__main__":
	app.run(host="0.0.0.0", port=8000, debug=True)
