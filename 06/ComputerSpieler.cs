using System;

class ComputerSpieler : Spieler
{
    private Random rnd = new Random();
    public char Symbol { get; set; }

    public void Ziehe(Spielfeld feld)
    {
        if (feld.NichtsMehrFrei())
            return;

        while (true)
        {
            int x = rnd.Next(0, 3);
            int y = rnd.Next(0, 3);

            if (!feld.IstBelegt(x,y))
            {
                feld.Setzen(x, y, Symbol);
                return;
            }
        };
    }
}