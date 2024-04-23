using System;

class Bruch : IDisposable, IComparable
{
    private int zaehler;
    private int nenner;

    public Bruch(int zaehler, int nenner)
    {
        this.Zaehler = zaehler;
        this.Nenner = nenner;
    }

    public int Zaehler
    {
        get { return zaehler; }
        set
        {
            zaehler = value;
        }
    }

    // Ein Destruktor macht in C# nicht wirklich viel Sinn, aber es gibt ihn
    ~Bruch()
    {
        Console.WriteLine("Ein Bruch-Objekt wird nun zerstört!");
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

    public double Dezimalzahl
    {
        get
        {
            return (double)zaehler / nenner;
        }

    }

    public void Ausgeben()
    {
        Console.WriteLine(zaehler + "/" + nenner);
    }

    public Bruch BildKehrwert()
    {
        return new Bruch(nenner, zaehler);
    }

    public Bruch Multipliziere(int zahl)
    {
        return new Bruch(zaehler * zahl, nenner);
    }

    public Bruch Multipliziere(Bruch b)
    {
        return new Bruch(zaehler * b.Zaehler, nenner * b.Nenner);
    }

    // Beim Bruch ist zwar nichts aufzuräumen, aber sonst wäre das hier der richtige Ort
    public void Dispose()
    {
        Console.WriteLine("Bruch wird nun aufgeräumt!");
    }

    public override bool Equals(Object o)
    {
        if (o == null)
            return false;

        Bruch b = o as Bruch;
        if (b == null)
            return false;

        return b.Zaehler == Zaehler && b.Nenner == Nenner;
    }

    public int CompareTo(object obj)
    {
        if (obj == null)
            return -1;

        var b = obj as Bruch;
        if (b == null)
            throw new ArgumentException("Übergebenes Objekt ist kein Bruch!");

        if (b.Dezimalzahl > Dezimalzahl)
            return 1;
        else if (b.Dezimalzahl < Dezimalzahl)
            return -1;
        else
            return 0;
    }
}