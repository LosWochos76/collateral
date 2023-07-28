public delegate void KontostandGeandertVerarbeiter(Bankkonto quelle, double neuer_kontostand);

public class Bankkonto
{
    public double Kontostand { get; private set; }
    public KontostandGeandertVerarbeiter KontostandGeandert;

    public void Einzahlen(double betrag)
    {
        if (betrag < 0)
            throw new Exception("Einzahlbetrag muss positiov sein!");

        Kontostand += betrag;

        if (KontostandGeandert != null)
            KontostandGeandert(this, Kontostand);
    }

    public void Auszahlen(double betrag)
    {
        if (betrag < 0)
            throw new Exception("Auszahlbetrag muss positiov sein!");

        if (Kontostand < betrag)
            throw new Exception("Kontostand reicht nicht aus!");

        Kontostand -= betrag;
        
        if (KontostandGeandert != null)
            KontostandGeandert(this, Kontostand);
    }
}