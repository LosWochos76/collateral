using System;

namespace _02
{
    class Program
    {
        static void Distanz()
        {
            Console.Write("Bitte geben Sie die Distanz in km ein: ");
            string input = Console.ReadLine();
            double distanz = Convert.ToDouble(input);

            Console.Write("Bitte geben Sie den Verbrauch pro 100km ein: ");
            input = Console.ReadLine();
            double verbrauch_pro_100km = Convert.ToDouble(input);

            double gesamtverbrauch = distanz / 100 * verbrauch_pro_100km;
            Console.WriteLine("Sie haben " + gesamtverbrauch + " kWh verbraucht!");
            Console.ReadLine();
        }

        static bool IsPrime(int n)
        {
            if (n < 1 || n == 1) 
                return false;
            else if (n == 2 || n == 3) 
                return true;

            for (int i = 2; i <= n / 2; i++) 
                if (n % i == 0)
                    return false;

            return true;
        }

        static void Main(string[] args)
        {
            // Distanz();
            // IsPrime(42);
 
            char[,] feld = TicTacToe.Zufallsfeld();
            TicTacToe.Ausgeben(feld);
            Console.WriteLine("Spiler X hat gewonnen: " + TicTacToe.Gewonnen(feld, 'X'));
            Console.WriteLine("Spiler O hat gewonnen: " + TicTacToe.Gewonnen(feld, 'O'));
       }
    }
}
