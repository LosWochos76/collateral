using System;

class TicTacToe
{
    private Spielfeld feld;
    private ISpieler aktueller_spieler;

    public ISpieler Spieler1 { get; set; }
    public ISpieler Spieler2 { get; set; }

    private void WechsleSpieler()
    {
        aktueller_spieler = aktueller_spieler == Spieler1 ? Spieler2 : Spieler1; 
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