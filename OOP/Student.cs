using System;

namespace OOP
{
    public class Student : Person
    {
        public int Matrikelnummer { get; set; }

        public Student(string vorname, string nachname, int matrikelnummer) : base(vorname, nachname)
        {
            Matrikelnummer = matrikelnummer;
        }

        public new void ausgeben()
        {
            Console.WriteLine("{0} {1} – {2}",
               Vorname, Nachname, Matrikelnummer);
        }
    }
}
