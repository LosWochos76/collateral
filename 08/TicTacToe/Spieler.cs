abstract class Spieler
{
    public char Spielstein { get; set; }

    public Spieler(char spielstein)
    {
        Spielstein = spielstein;
    }

    public abstract void Ziehe(Spielfeld feld);
}