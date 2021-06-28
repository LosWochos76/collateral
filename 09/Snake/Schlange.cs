using System.Collections.Generic;

public class Schlange
{
    private int richtung = 0;
    private List<Block> koerper = new List<Block>();
    private bool verlaengern = false;

    public Block Kopf
    {
        get { return koerper[0]; }
    }

    public Schlange()
    {
        // Hier müssen wir Blöcke in die Schlange einfügen, die in der Mitte des Bildschirms positioniert sind.
        // Dazu müssen wir natürlich auch eine Vorstellung von den Dimensionen des Bildschirms haben usw.
    }

    public void BewegeEinenSchritt()
    {
        var kopf_x = Kopf.X;
        var kopf_y = Kopf.Y;

        if (!verlaengern)
        {
            koerper.RemoveAt(koerper.Count-1);
            verlaengern = false;
        }

        if (richtung == 0) // Nach Oben
            koerper.Add(new Block(kopf_x, kopf_y - Block.Hoehe));
        
        if (richtung == 1) // Nach rechts
            koerper.Add(new Block(kopf_x + Block.Breite, kopf_y));

        if (richtung == 2) // Nach unten
            koerper.Add(new Block(kopf_x, kopf_y + Block.Hoehe));
        
        if (richtung == 3) // Nach links
            koerper.Add(new Block(kopf_x - Block.Breite, kopf_y));
    }

    public void WechsleRichtung(int richtung)
    {
        this.richtung = richtung;
    }

    public void VerlaengereUmEinenBlock()
    {
        this.verlaengern = true;
    }

    public bool IstKollidiert()
    {
        // Ist die Schlange mit sich selber kollidiert?
        for (int i=1; i<koerper.Count; i++)
            if (Kopf.X == koerper[i].X && Kopf.Y == koerper[i].Y)
                return true;
        
        // Ist die Schlange mit dem Bildschirmrand kollidiert? (Leider haben wir noch keine Idee vom Bildschirm)
        // ...

        return false;
    }
}