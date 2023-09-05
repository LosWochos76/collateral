using System.Collections.Generic;

namespace SeminarManager;

public class Seminar
{
    public int ID { get; set; } = 0;
    public string Name { get; set; } = string.Empty;

    public Seminar(string name)
    {
        Name = name;
    }

    public Person Dozent { get; set; } = null;
    public List<Person> Teilnehmer { get; set; } = new List<Person>();
    public int AnzahlTeilnehmer { get { return Teilnehmer.Count; } }
}