using System;

class Mitglied
{
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public DateTime Geburtsdatum { get; set; }
    
    public Mitglied(string vorname, string nachname, string geburtsdatum)
    {
        this.Vorname = vorname;
        this.Nachname = nachname;
        this.Geburtsdatum = Convert.ToDateTime(geburtsdatum);
    }

    public override string ToString()
    {
        return Vorname + " " + Nachname;
    }
}