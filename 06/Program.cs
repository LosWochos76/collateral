using System;

namespace _06
{
    class Program
    {
        // Um zu demonstrieren, wie man mit dem Sportverein umgeht
        static void SportvereinTesten()
        {
            Sportverein s = new Sportverein("TUS Hamm");
            s.Vorsitzender = new Mitglied("Erika", "Wischnowski", "3.2.2003");
            s.MitgliedHinzufuegen(new Mitglied("Peter", "Pan", "6.8.1997"));

            s.Ausgeben();
        }

        // Das TicTacToe-Spiel testen
        static void TicTacToeTesten()
        {
            var s1 = new ComputerSpieler();
            var s2 = new ComputerSpieler();
            var spiel = new TicTacToe(s1, s2);
            spiel.StarteSpiel();
        }

        static void Main(string[] args)
        {
            // SportvereinTesten();
            TicTacToeTesten();
        }
    }
}
