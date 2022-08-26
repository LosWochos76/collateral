public class Program
{
    public static int Quersumme(int zahl)
    {
        int quersumme = 0;

        do
        {
            quersumme += zahl % 10;
            zahl /= 10;    
        } 
        while (zahl > 9);

        return quersumme;
    }

    public static bool IstHarshadZahl(int zahl)
    {
        int quersumme = Quersumme(zahl);
        return zahl % quersumme == 0;
    }

    public static void Main(string[] args)
    {
        for (int i=1; i<100; i++)
            if (IstHarshadZahl(i))
                Console.WriteLine("{0} ist eine Harshad-Zahl!", i);
    }
}