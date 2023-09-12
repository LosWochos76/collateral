using System;

public class TicTacToe
{
    private Spielfeld feld;
    private Spieler spieler1;
    private Spieler spieler2;
    private Spieler aktueller_spieler;

    public TicTacToe(Spielfeld feld, Spieler spieler1, Spieler spieler2)
    {
        this.feld = feld;
        this.spieler1 = spieler1;
        this.spieler2 = spieler2;
    }

    private void WechsleSpieler()
    {
        aktueller_spieler = aktueller_spieler == spieler1 ? spieler2 : spieler1;
    }

    public void StarteSpiel()
    {
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