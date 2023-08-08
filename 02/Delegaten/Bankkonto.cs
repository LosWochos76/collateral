public delegate void KontostandGeandertVerarbeiter(object sender, BankkontoEventArgs e);

public class Bankkonto
{
    public double Kontostand { get; private set; }
    public event KontostandGeandertVerarbeiter KontostandGeandert;

    public void Einzahlen(double betrag)
    {
        if (betrag < 0)
            throw new Exception("Einzahlbetrag muss positiov sein!");

        Kontostand += betrag;

        if (KontostandGeandert != null)
            KontostandGeandert(this, new BankkontoEventArgs(betrag, true));
    }

    public void Auszahlen(double betrag)
    {
        if (betrag < 0)
            throw new Exception("Auszahlbetrag muss positiov sein!");

        if (Kontostand < betrag)
            throw new Exception("Kontostand reicht nicht aus!");

        Kontostand -= betrag;
        
        if (KontostandGeandert != null)
            KontostandGeandert(this, new BankkontoEventArgs(betrag, false));
    }
}