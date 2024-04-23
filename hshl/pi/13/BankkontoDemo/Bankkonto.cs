public class Bankkonto
{
    private double kontostand = 0;

    public void Einzahlen(double betrag)
    {
        kontostand += betrag;
    }

    public void Ausszahlen(double betrag)
    {
        if (kontostand >= betrag)
            kontostand -= betrag;

        if (kontostand < 0) // sollte eigentlich nie eintreten!
            throw new Exception("Betrag kleiner 0!");
    }
}