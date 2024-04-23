using System;

class TicTacToe
{
    private Spielfeld feld;
    private Spieler spieler1 = new Spieler('X');
    private Spieler spieler2 = new Spieler('O');
    private Spieler aktueller_spieler;

    private void WechsleSpieler()
    {
        aktueller_spieler = aktueller_spieler == spieler1 ? spieler2 : spieler1; 
    }

    public void StarteSpiel()
    {
        feld = new Spielfeld();

        do
        {
            WechsleSpieler();

            Console.WriteLine("Spieler " + aktueller_spieler.Spielstein + " ist an der Reihe:");
            aktueller_spieler.Ziehe(feld);
            feld.Ausgeben();

            if (feld.HatGewonnen(aktueller_spieler.Spielstein)) 
            {
                Console.WriteLine("Spieler " + aktueller_spieler.Spielstein + " hat gewonnen!");
                break;
            }
        } 
        while (feld.IstEinFeldFrei());

        if (!feld.HatGewonnen() && !feld.IstEinFeldFrei())
            Console.WriteLine("Unentschieden!");
    }
}