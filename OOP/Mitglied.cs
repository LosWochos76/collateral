using System;

namespace OOP
{
    public class Mitglied
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public int Geschlecht { get; set; }

        public Mitglied(string vorname, string nachname, DateTime geburtsdatum, int geschlecht)
        {
            this.Vorname = vorname;
            this.Nachname = nachname;
            this.Geburtsdatum = geburtsdatum;
            this.Geschlecht = geschlecht;
        }
    }
}