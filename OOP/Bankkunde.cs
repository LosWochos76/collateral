using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Bankkunde
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public List<Bankkonto> Konten { get; }

        public Bankkunde(string vorname, string nachname)
        {
            Vorname = vorname;
            Nachname = nachname;

            Bankkonto konto = new Bankkonto(this, 12345);
            Konten.Add(konto);
        }
    }
}
