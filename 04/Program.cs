using System;

namespace _04
{
    class Program
    {
        // Nur mal, um zu demonstrieren, wie man mit Objekten umgeht, die Ressourcen freigeben sollen:
        static void UmgangMitRessourcen()
        {
            using (Bruch einBruch = new Bruch(1, 3))
            {
                einBruch.Ausgeben();
            }
        }

        // Um zu demonstrieren, wie man zur Laufzeit Ausnahmen behandeln kann
        static void VersucheBruchZuErzeugen()
        {
            Bruch b = null;
            bool fehler_aufgetreten = false;

            do
            {
                fehler_aufgetreten = false;
                Console.Write("Bitte zähler eingeben: ");
                int zaehler = Convert.ToInt32(Console.ReadLine());
                Console.Write("Bitte Nenner eingeben: ");
                int nenner = Convert.ToInt32(Console.ReadLine());

                try
                {
                    b = new Bruch(zaehler, nenner);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Sorry! Es wurden falsche Werte eingegeben!");
                    fehler_aufgetreten = true;
                }
            }
            while (fehler_aufgetreten);

            b.Ausgeben();
        }

        // Objekte auf Wertgleichheit prüfen
        static void Vergleichen()
        {
            Bruch b1 = new Bruch(1,3);
            Bruch b2 = new Bruch(1,3);

            bool verweisgleich = b1 == b2;
            Console.WriteLine("Verweisgleich: " + verweisgleich);

            bool wertgleich = b1.Equals(b2);
            Console.WriteLine("Wertgleich: " + wertgleich);

            Bruch b3 = new Bruch(1,4);
            Console.WriteLine(b3.CompareTo(b1));
        }

        static void Main(string[] args)
        {
            // UmgangMitRessourcen();
            // VersucheBruchZuErzeugen();
            // Vergleichen();
        }
    }
}