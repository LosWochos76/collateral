using System;

class Person
{
    protected string vorname;
    protected string nachname;

    public string Vorname
    {
        get { return vorname; }
        set { vorname = value; }
    }

    public string Nachname
    {
        get { return nachname; }
        set { nachname = value; }
    }

    public Person(string vorname, string nachname)
    {
        this.Vorname = vorname;
        this.Nachname = nachname;
    }

    public Adresse Wohnadresse { get; set; }

    public void Ausgeben()
    {
        Console.WriteLine(vorname + " " + nachname);
    }
}