using System.Diagnostics;

public class Program
{
    public static void TenthousandsPrime(CancellationToken cts)
    {
        FindPrime(10000, cts);
    }

    public static void FindPrime(int n, CancellationToken cts)
    {
        int counter = 0;
        long number = 2;

        while (counter < n)
        {
            if (cts.IsCancellationRequested)
            {
                Console.WriteLine("Abbruch!");
                return;
            }

            if (IsPrime(number))
                counter++;

            number++;
        }

        Console.WriteLine(number);
    }

    public static bool IsPrime(long n)
    {
        for (long i=2; i<n/2; i++)
            if (n % i == 0)
                return false;
        
        return true;
    }

    public static void Main(string[] args)
    {
        var sw = new Stopwatch();
        sw.Start();

        CancellationTokenSource cts = new CancellationTokenSource();
        var threads = new List<Thread>();
        for (int i=0; i<3; i++)
        {
            var t = new Thread(() => FindPrime(10000, cts.Token));
            threads.Add(t);
            t.Start();
        }

        /*Thread.Sleep(1000);
        cts.Cancel();*/

        threads.ForEach(t => t.Join());
        
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
}