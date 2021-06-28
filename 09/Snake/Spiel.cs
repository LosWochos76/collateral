using System;

public class Spiel
{
    private int punkte = 0;
    private Random rnd = new Random();

    private Schlange schlange;
    private Block apfel;

    public void Starten()
    {
        schlange = new Schlange();
        apfel = ZufallsApfel();

        while (true)
        {
            if (HatSchlangeApfelgefressen()) 
            {
                punkte++;
                schlange.VerlaengereUmEinenBlock();
            }

            if (schlange.IstKollidiert())
                return;
        }
    }

    private Block ZufallsApfel()
    {
        // Hier muss ein Apfel zufällig auf dem Spielfeld abgelegt werden
        // Dazu brauchen wir natürlich auch eine Idee, wie breit und hoch das Spielfeld ist.
        int x = 320;
        int y = 240; 
        return new Block(x, y);
    }

    private bool HatSchlangeApfelgefressen()
    {
        return (schlange.Kopf.X == apfel.X && schlange.Kopf.Y == apfel.Y);
    }
}