using System;

public class Bruch
{
    private int zaehler = 0;
    private int nenner = 0;

    public Bruch(int zaehler, int nenner)
    {
        Zaehler = zaehler;
        Nenner = nenner;
    }

    public int Zaehler
    { 
        get
        {
            return zaehler;
        }
        set
        {
            zaehler = value;
        }
    }
    
    public int Nenner
    { 
        get
        {
            return nenner;
        }
        set
        {
            if (value == 0)
                throw new ArgumentException("Nenner darf nicht 0 sein!");

            nenner = value;
        }
    }

    public void Ausgeben()
    {
        Console.WriteLine(Zaehler + "/" + Nenner);
    }
}