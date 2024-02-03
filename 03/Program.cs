using System;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            Bruch einBruch = new Bruch();
            einBruch.Zaehler = 1;
            einBruch.Nenner = 3;
            einBruch.Ausgeben();

            Bruch zweiterBruch = einBruch.BildKehrwert();
            zweiterBruch.Ausgeben();

            Bruch dritterBruch = einBruch.Multipliziere(zweiterBruch);
            dritterBruch.Ausgeben();
        }
    }
}
