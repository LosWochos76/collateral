using System;

class Program
{
    public static void Main(string[] args)
    {
        // Aufgabe formatierte Datenausgabe:
        Console.WriteLine("Die Studentin {0} hat die Matrikelnummer {1} " +
            "und hat {2:P} in der Klausur erhalten.",
            "Frederike Öztürk", 7892645, 0.967896);

        // Aufgabe Rechnungsbetrag und Mehrwertsteuer:
        Console.WriteLine("Gesamtbetrag={0:C}, Mwst={1:C}",
            (3 * 459 + 4 * 319 + 5 * 29.90) * 0.97,
            (3 * 459 + 4 * 319 + 5 * 29.90) * 0.97 / 119 * 19
        );

        // Aufgabe Mathematik:
        Console.WriteLine("Hypothenuse={0:F2}", Math.Sqrt(Math.Pow(7, 2) + Math.Pow(9, 2)));
        Console.WriteLine("{0:F2} | {1:F2}", 0.0, 5 * Math.Pow(0.0, 2) - 7 * 0.0 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 0.1, 5 * Math.Pow(0.1, 2) - 7 * 0.1 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 0.2, 5 * Math.Pow(0.2, 2) - 7 * 0.2 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 0.3, 5 * Math.Pow(0.3, 2) - 7 * 0.3 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 0.4, 5 * Math.Pow(0.4, 2) - 7 * 0.4 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 0.5, 5 * Math.Pow(0.5, 2) - 7 * 0.5 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 0.6, 5 * Math.Pow(0.6, 2) - 7 * 0.6 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 0.7, 5 * Math.Pow(0.7, 2) - 7 * 0.7 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 0.8, 5 * Math.Pow(0.8, 2) - 7 * 0.8 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 0.9, 5 * Math.Pow(0.9, 2) - 7 * 0.9 + 9);
        Console.WriteLine("{0:F2} | {1:F2}", 1.0, 5 * Math.Pow(1.0, 2) - 7 * 1.0 + 9);
    }
}