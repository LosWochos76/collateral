using System;

namespace GridViewDemo;

public class Mitglied
{
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public DateTime Geburtstag { get; set; }

    public Mitglied(string vorname, string nachname, DateTime geburtstag)
    {
        Vorname = vorname;
        Nachname = nachname;
        Geburtstag = geburtstag;
    }
}
