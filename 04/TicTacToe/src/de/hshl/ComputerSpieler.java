package de.hshl;

import java.util.Random;

public class ComputerSpieler  {
    private Random rnd = new Random();
    private char spielstein;

    public ComputerSpieler(char spielstein) {
        setSpielstein(spielstein);
    }

    public char getSpielstein() {
        return spielstein;
    }

    public void setSpielstein(char spielstein) {
        this.spielstein = spielstein;
    }

    public void ziehe(Spielfeld feld) {
        if (!feld.istEinFeldFrei())
            return;

        while (true) {
            int x = rnd.nextInt(3);
            int y = rnd.nextInt(3);

            if (!feld.istBelegt(x, y)) {
                feld.setzeSpielstein(x, y, spielstein);
                return;
            }
        }
    }
}