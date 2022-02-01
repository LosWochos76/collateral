package de.hshl;

public abstract class Spieler {
	protected char spielstein;

    public Spieler(char spielstein) {
        setSpielstein(spielstein);
    }

    public char getSpielstein() {
        return spielstein;
    }

    public void setSpielstein(char spielstein) {
        this.spielstein = spielstein;
    }

    public abstract void ziehe(Spielfeld feld);
}