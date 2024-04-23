using Newtonsoft.Json;
using System;

namespace SeminarManager;

public class Person
{
    public int ID { get; set; } = 0;
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public DateTime Geburtstag { get; set; }

    [JsonIgnore]
    public string VollerName { get { return Vorname + " " + Nachname; } }

    public Person()
    {
        Vorname = string.Empty;
        Nachname = string.Empty;
        Geburtstag = DateTime.MinValue;
    }

    public Person(string vorname, string nachname, DateTime geburtstag)
    {
        Vorname = vorname;
        Nachname = nachname;
        Geburtstag = geburtstag;
    }
}
