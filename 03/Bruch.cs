using System;

class Bruch
{
    private int zaehler;
    private int nenner;

    public int Zaehler
    {
        get { return zaehler; }
        set
        {
            zaehler = value;
        }
    }

    public int Nenner
    {
        get { return nenner; }
        set
        {
            if (value == 0)
                throw new Exception("Nenner darf nicht 0 sein!");

            nenner = value;
        }
    }

    public void Ausgeben()
    {
        Console.WriteLine(zaehler + "/" + nenner);
    }

    public Bruch BildKehrwert()
    {
        Bruch ergebnis = new Bruch();
        ergebnis.Zaehler = nenner;
        ergebnis.Nenner = zaehler;
        return ergebnis;
    }

    public Bruch Multipliziere(int zahl)
    {
        Bruch ergebnis = new Bruch();
        ergebnis.Zaehler = zaehler * zahl;
        ergebnis.Nenner = nenner;
        return ergebnis;
    }

    public Bruch Multipliziere(Bruch b)
    {
        Bruch ergebnis = new Bruch();
        ergebnis.Zaehler = zaehler * b.Zaehler;
        ergebnis.Nenner = nenner * b.Nenner;
        return ergebnis;
    }
}