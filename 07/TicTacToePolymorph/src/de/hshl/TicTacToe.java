package de.hshl;

public class TicTacToe  {
    private Spielfeld feld;
    private Spieler spieler1 = new ComputerSpieler('X');
    private Spieler spieler2 = new MenschlicherSpieler('O');
    private Spieler aktueller_spieler;
    
    public void setSpieler1(Spieler s) {
    	spieler1 = s;
    }
    
    public void setSpieler2(Spieler s) {
    	spieler2 = s;
    }

    private void wechsleSpieler() {
        aktueller_spieler = aktueller_spieler == spieler1 ? spieler2 : spieler1;
    }

    public void starteSpiel() throws Exception {
    	if (spieler1 == null || spieler2 == null)
    		throw new Exception("Die Spielerobjekte müssen festgelegt werden!");
    	
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