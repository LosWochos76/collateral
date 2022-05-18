class Program
{
    public static void Wuerfelspiel()
    {
        Random rnd = new Random();
        double guthaben = 10;

        do
        {
            Console.WriteLine("Aktuelles Guthaben={0:C}", guthaben);
            Console.Write("Bitte Einsatz eingeben: ");
            double einsatz = Convert.ToDouble(Console.ReadLine());

            Console.Write("Auf welche Zahl wollen Sie setzen? ");
            int zahl = Convert.ToInt32(Console.ReadLine());
            int wuerfel = rnd.Next(1, 7);

            if (wuerfel == zahl)
            {
                guthaben += einsatz;
                Console.WriteLine("Es wurde die {0} gewürfelt!", wuerfel);
                Console.WriteLine("Glückwunsch! Sie haben gewonnen!");
            }
            else
            {
                guthaben -= einsatz;
                Console.WriteLine("Es wurde die {0} gewürfelt!", wuerfel);
                Console.WriteLine("Schade! Sie haben verloren!");
            }
        }
        while (guthaben > 0);
    }

    public static void MonteCarloSimulation()
    {
        Random rnd = new Random();
        int count = 10000000;
        int i = 0;
        int count_in = 0;

        while (i < count)
        {
            double x = rnd.NextDouble() * 2 - 1;
            double y = rnd.NextDouble() * 2 - 1;
            double len = Math.Sqrt(x*x+y*y);
            if (len <= 1)
                count_in++;

            i++;
        }

        double pi = 4*(double)count_in/count;
        Console.WriteLine(pi);
    }

    public static void FakultaetMitWhile(long n)
    {
        if (n == 0)
        {
            Console.WriteLine(1);
            return;
        }

        long result = 1;
        long i = 1;
    
        while (i <= n)
        {
            result = result * i;
            i++;
        }

        Console.WriteLine(result);
    }

    public static void FakultaetMitFor(long n)
    {
        if (n == 0)
        {
            Console.WriteLine(1);
            return;
        }

        long result = 1;
        for (long i=1; i<=n; i++)
            result = result * i;

        Console.WriteLine(result);
    }

    public static void Main()
    {
        //Wuerfelspiel();
        //MonteCarloSimulation();
        FakultaetMitFor(20);
    }
}