using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Verzeichnis
    {
        public string Name { get; set; }
        public List<Verzeichnis> SubVerzeichnisse { get; set; }

        public Verzeichnis(string name)
        {
            Name = name;
            SubVerzeichnisse = new List<Verzeichnis>();
        }
    }
}
