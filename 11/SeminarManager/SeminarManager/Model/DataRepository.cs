using System;

namespace SeminarManager;

public class DataRepository
{
    public PersonRepository Persons { get; private set; }
    public SeminarRepository Seminars { get; private set; }

    public DataRepository()
    {
        Persons = new PersonRepository();
        Seminars = new SeminarRepository();

        var p = new Person("Michael", "Meier", Convert.ToDateTime("1980-06-12"));
        Persons.Save(p);

        var s = new Seminar("Objektorientierte Programmierung");
        s.Dozent = p;
        Seminars.Save(s);
    }
}