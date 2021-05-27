using System;

namespace Grafikprogramm
{
    class Program
    {
        static void Main(string[] args)
        {
            Dreieck d = new Dreieck() { X=10, Y=15 };
            d.Zeichne();

            GrafischesElement g = d;
            g.Zeichne();
        }
    }
}
