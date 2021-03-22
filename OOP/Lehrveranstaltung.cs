using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Lehrveranstaltung
    {
        public string Name { get; set; }
        public List<Student> Studenten { get; }

        public Lehrveranstaltung(string name)
        {
            Name = name;
            Studenten = new List<Student>();
        }
    }
}
