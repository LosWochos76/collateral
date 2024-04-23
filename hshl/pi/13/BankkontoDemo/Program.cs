using System.Reflection.Metadata;

public class Program
{
    private static Bankkonto konto = new Bankkonto();
    private static Random rnd = new Random();

    private static void Einzahler()
    {
        while (true)
        {
            konto.Einzahlen(1000);
            Thread.Sleep(rnd.Next(0, 500));
        }
    }

    private static void Auszahler()
    {
        while (true)
        {
            konto.Ausszahlen(1000);
            Thread.Sleep(rnd.Next(0, 500));
        }
    }

    public static void Main()
    {
        for (int i=0; i<50; i++)
            (new Thread(Einzahler)).Start();

        for (int i=0; i<200; i++)
            (new Thread(Auszahler)).Start();
    }
}