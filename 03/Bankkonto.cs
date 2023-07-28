public class Bankkonto
{
    public double Kontostand { get; private set; }

    public void Einzahlen(double betrag)
    {
        if (betrag < 0)
            throw new Exception("Einzahlbetrag muss positiv sein!");
        
        Kontostand += betrag;
    }

    public void Auszahlen(double betrag)
    {
        if (betrag > Kontostand)
            throw new Exception("Kontostand darf nicht negativ werden!");
        
        Kontostand -= betrag;
    }
}