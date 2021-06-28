public class Bankkonto
{
    public double Kontostand { get; private set; }

    public void Einzahlen(double betrag)
    {
        Kontostand += betrag;
    }

    public void Auszahlen(double betrag)
    {
        Kontostand -= betrag;
    }
}