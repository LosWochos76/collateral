using System;

class Dozent : Person
{
    public string Lehrgebiet { get; set; }

    public Dozent(string vorname, string nachname, string lehrgebiet) : base(vorname, nachname)
    {
        this.Lehrgebiet = lehrgebiet;
    }

    public new void Ausgeben()
    {
        base.Ausgeben();
        Console.WriteLine("Lehrgebiet: " + Lehrgebiet);
    }
}