using System;

class TicTacToe
{
    private Spielfeld feld;
    private Spieler spieler1;
    private Spieler spieler2;
    private Spieler aktueller_spieler;

    public TicTacToe(Spieler spieler1, Spieler spieler2)
    {
        this.spieler1 = spieler1;
        spieler1.Symbol = 'X';

        this.spieler2 = spieler2;
        spieler2.Symbol = 'O';
    }

    private void NaechsterSpieler()
    {
        aktueller_spieler = aktueller_spieler == spieler1 ? spieler2 : spieler1; 
    }

    public void StarteSpiel()
    {
        feld = new Spielfeld();

        do
        {
            NaechsterSpieler();

            Console.WriteLine("Spieler " + aktueller_spieler.Symbol + " ist an der Reihe:");
            aktueller_spieler.Ziehe(feld);
            feld.Ausgeben();

            if (feld.HatGewonnen(aktueller_spieler.Symbol)) 
            {
                Console.WriteLine("Spieler " + aktueller_spieler.Symbol + " hat gewonnen!");
                break;
            }
        } 
        while (!feld.NichtsMehrFrei());

        if (!feld.HatGewonnen() && feld.NichtsMehrFrei())
            Console.WriteLine("Unentschieden!");
    }
}