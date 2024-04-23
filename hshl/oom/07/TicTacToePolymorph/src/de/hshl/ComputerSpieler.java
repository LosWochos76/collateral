package de.hshl;

import java.util.Random;

public class ComputerSpieler extends Spieler {
    private Random rnd = new Random();

    public ComputerSpieler(char spielstein) {
        super(spielstein);
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