using System;

class Person
{
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public Adresse Wohnadresse { get; set; }

    public Person(string vorname, string nachname)
    {
        Vorname = vorname;
        Nachname = nachname;
    }

    public void Ausgeben()
    {
        Console.WriteLine(Vorname + " " + Nachname);

        if (Wohnadresse != null)
            Wohnadresse.Ausgeben();
    }
}