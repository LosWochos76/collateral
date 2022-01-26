package de.hshl;

import java.util.Random;

public class Spielfeld  {
    private char[][] feld;

    public Spielfeld() {
        initialisieren();
    }

    public void initialisieren() {
        feld = new char[3][3];
        for (int x = 0; x < 3; x++) {
            for (int y = 0; y < 3; y++) {
                setzeSpielstein(x, y, ' ');
            }
        }
    }

    public void setzeSpielstein(int x, int y, char spieler) {
        feld[y][x] = spieler;
    }

    public char holeSpielstein(int x , int y) {
        return feld[x][y];
    }

    public boolean istMitSpielsteinBelegt(int x, int y, char spielstein) {
        return feld[x][y] == spielstein;
    }

    public void zufaelligBelegen() {
        Random rnd = new Random();

        for (int x = 0; x < 3; x++) {
            for (int y = 0; y < 3; y++) {
                switch (rnd.nextInt(3)) {
                    case 1: setzeSpielstein(x, y, 'X'); break;
                    case 2: setzeSpielstein(x, y, 'O'); break;
                    default: setzeSpielstein(x, y, ' '); break;
                }
            }
        }
    }

    public void ausgeben() {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
               System.out.print("'" + holeSpielstein(i, j) + "' ");
            }

            System.out.println();
        }

        System.out.println();
    }

    public boolean hatGewonnen(char spielstein) {
        for (int i = 0; i < 3; i++) {
            if (istMitSpielsteinBelegt(0, i, spielstein) &&
                istMitSpielsteinBelegt(1, i, spielstein) &&
                istMitSpielsteinBelegt(2, i, spielstein))
                return true;

            if (istMitSpielsteinBelegt(i, 0, spielstein) &&
                istMitSpielsteinBelegt(i, 1, spielstein) &&
                istMitSpielsteinBelegt(i, 2, spielstein))
                return true;
        }

        if (istMitSpielsteinBelegt(0, 0, spielstein) &&
            istMitSpielsteinBelegt(1, 1, spielstein) &&
            istMitSpielsteinBelegt(2, 2, spielstein))
            return true;

        if (istMitSpielsteinBelegt(0, 2, spielstein) &&
            istMitSpielsteinBelegt(1, 1, spielstein) &&
            istMitSpielsteinBelegt(2, 0, spielstein))
            return true;

        return false;
    }

    public boolean hatGewonnen() {
        return hatGewonnen('X') || hatGewonnen('O');
    }

    public boolean istBelegt(int x, int y) {
        return feld[y][x] != ' ';
    }

    public boolean istEinFeldFrei() {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (feld[i][j] ==' '){
                    return true;
                }
            }
        }

        return false;
    }
}