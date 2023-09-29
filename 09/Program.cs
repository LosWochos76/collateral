using System.Diagnostics;
using System.Diagnostics.Metrics;

public class Program
{
    public static long counter = 0;

    public static void Main()
    {
        Hanoi('A', 'B', 'C', 10);
        Console.WriteLine(counter);
    }

    // StackOverflow
    public static void TueEtwas()
    {
        Console.WriteLine("Bin beschäftigt...");
        TueEtwas();
    }

    public static void Countdown(int n)
    {
        if (n == 0)
            return;

        Console.WriteLine(n);
        Countdown(n - 1);
    }

    public static int Fakultaet(int n)
    {
        if (n == 0)
            return 1;
        else
            return n * Fakultaet(n - 1);
    }

    public static int Summe(int n)
    {
        if (n == 0)
            return 1;
        else
            return n + Summe(n - 1);
    }

    public static int AnzahlTeiler(int n, int divisor = 1)
    {
        if (divisor > n)
            return 0;

        if (n % divisor == 0)
            return 1 + AnzahlTeiler(n, divisor + 1);
        else
            return AnzahlTeiler(n, divisor + 1);
    }

    public static int Quersumme(int n)
    {
        if (n < 10)
            return n;
        else
            return n % 10 + Quersumme(n / 10);
    }

    public static long Fib(long n)
    {
        counter++;

        if (n <= 2)
            return 1;

        return Fib(n - 1) + Fib(n - 2);
    }

    public static void Hanoi(char start, char hilf, char ziel, int n)
    {
        counter++;

        if (n == 1)
        {
            Console.WriteLine("Bewege eine Scheibe von {0} nach {1}.", start, ziel);
        }
        else
        {
            Hanoi(start, ziel, hilf, n - 1);
            Hanoi(start, hilf, ziel, 1);
            Hanoi(hilf, start, ziel, n - 1);
        }
    }
}