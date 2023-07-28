public class Bankkonto : IObservable
{
    public double Kontostand { get; private set; }

    private List<IObserver> beobachter = new List<IObserver>();

    public void BeobachterAnmelden(IObserver b)
    {
        beobachter.Add(b);
    }

    public void BeobachterAbmelden(IObserver b)
    {
       beobachter.Remove(b);
    }

    public void BeobachterInformieren()
    {
        foreach (var b in beobachter)
            b.AenderungEingetreten(this);
    }

    public void Einzahlen(double betrag)
    {
        if (betrag < 0)
            throw new Exception("Einzahlbetrag muss größer 0 sein!");
        
        Kontostand += betrag;
        BeobachterInformieren();
    }

    public void Auszahlen(double betrag)
    {
        if (betrag < 0)
            throw new Exception("Auszahlbetrag muss größer 0 sein!");
        
        if (Kontostand < betrag)
            throw new Exception("Kontostand reicht nicht!");

        Kontostand -= betrag;
        BeobachterInformieren();
    }
}