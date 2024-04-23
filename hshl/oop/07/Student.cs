using System;

class Student : Person
{
    public int Matrikelnummer { get; set; }

    public Student(string vorname, string nachname, int matrikelnummer) : base(vorname, nachname)
    {
        this.Matrikelnummer = matrikelnummer;
    }

    public new void Ausgeben()
    {
        base.Ausgeben();
        Console.WriteLine("Matrikelnummer: " + Matrikelnummer);
    }
}