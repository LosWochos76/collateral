using System;

namespace ValidationDemo;

public class Bruch
{
    public int Zaehler { get; set; }

    private int nenner;
    public int Nenner 
    { 
        get { return nenner; }
        set
        {
            if (value == 0)
                throw new ArgumentException("Nenner darf nicht 0 sein!");

            nenner = value;
        }
    }

    public Bruch(int zaehler, int nenner)
    {
        Zaehler = zaehler;
        Nenner = nenner;
    }  

    public Bruch Clone()
    {
        return new Bruch(Zaehler, Nenner);
    }
}