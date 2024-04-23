using System;

namespace Array
{
    class Program
    {
        static void Main(string[] args)
        {
            Bruch[] brueche = new Bruch[20];
            for (int i=0; i<20; i++)
                brueche[i] = new Bruch(1, (i+1));

            for (int i=0; i<20; i++)
                brueche[i].Ausgeben();
        }
    }
}
