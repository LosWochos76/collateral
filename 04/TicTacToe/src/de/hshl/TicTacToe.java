package de.hshl;

public class TicTacToe  {
    private Spielfeld feld;
    private ComputerSpieler spieler1 = new ComputerSpieler('X');
    private ComputerSpieler spieler2 = new ComputerSpieler('O');
    private ComputerSpieler aktueller_spieler;

    private void wechsleSpieler() {
        aktueller_spieler = aktueller_spieler == spieler1 ? spieler2 : spieler1;
    }

    public void starteSpiel() {
        feld = new Spielfeld();

        do {
            wechsleSpieler();

            System.out.println("Spieler " + aktueller_spieler.getSpielstein() + " ist an der Reihe:");
            aktueller_spieler.ziehe(feld);
            feld.ausgeben();

            if (feld.hatGewonnen(aktueller_spieler.getSpielstein())) {
                System.out.println("Spieler " + aktueller_spieler.getSpielstein() + " hat gewonnen!");
                break;
            }
        } while (feld.istEinFeldFrei());

        if (!feld.hatGewonnen() && !feld.istEinFeldFrei())
            System.out.println("Unentschieden!");
    }
}