public class Program
{
    public static void Main()
    {
        Console.WriteLine(Quersumme(1234));
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

    public static bool IsPrime(int n, int divisor = 2)
    {
        if (divisor * divisor > n)
            return true;
        
        if (n % divisor == 0)
            return false;
        
        return IsPrime(n, divisor + 1);
    }

    public static int Quersumme(int n)
    {
        if (n < 10)
            return n;
        else
            return n % 10 + Quersumme(n / 10);
    }
}