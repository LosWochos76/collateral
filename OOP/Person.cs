using System;

namespace OOP
{
    public class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public Person(string vorname, string nachname)
        {
            Vorname = vorname;
            Nachname = nachname;
        }

        public void ausgeben()
        {
            Console.WriteLine("{0} {1}", Vorname, Nachname);
        }
    }
}
