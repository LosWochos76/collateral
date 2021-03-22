using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Seminar
    {
        public string Name { get; set; }
        public Person Dozent { get; set; }
        public List<Person> Teilnehmer { get; }

        public Seminar(string name, Person dozent)
        {
            Teilnehmer = new List<Person>();
            Name = name;
            Dozent = dozent;
        }
    }
}
