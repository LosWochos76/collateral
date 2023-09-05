using System;

namespace SeminarManager;

public class Person
{
    public int ID { get; set; } = 0;
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public DateTime Geburtstag { get; set; }

    public string VollerName { get { return Vorname + " " + Nachname; } }

    public Person(string vorname, string nachname, DateTime geburtstag)
    {
        Vorname = vorname;
        Nachname = nachname;
        Geburtstag = geburtstag;
    }
}
