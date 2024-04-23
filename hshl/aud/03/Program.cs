using System.Diagnostics;

long count = 0;
for (long n = 2; n < Int64.MaxValue; n++)
{
    var sw = new Stopwatch();
    sw.Start();
    bool is_prime = Zahlen.IstPrimzahl(n);
    sw.Stop();

    if (is_prime)
        count++;

    if (count == 1_000)
    {
        Console.WriteLine("{0} ist eine Primzahl und es " +
            "hat {1} Ticks gedauert!", n, sw.ElapsedTicks);
        count = 0;
    }
}

Console.ReadLine();
