using System;

namespace AUD.Misc
{
    class Hanoi
    {
        public static void hanoi(char Ausgangsstapel, char Hilfsstapel, char Zielstapel, int n)
        {
            if (n == 1)
            {
                Console.WriteLine("Bewege eine Scheibe von Stapel {0} nach Stapel {1}.", Ausgangsstapel, Zielstapel);
            }
            else
            {
                hanoi(Ausgangsstapel, Zielstapel, Hilfsstapel, n - 1);
                hanoi(Ausgangsstapel, Hilfsstapel, Zielstapel, 1);
                hanoi(Hilfsstapel, Ausgangsstapel, Zielstapel, n - 1);
            }
        }
    }
}
