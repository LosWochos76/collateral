public class Program
{
    public static void Main(string[] args)
    {
    }

    public static void MethodenVonStringAuslesen()
    {
        Type t = typeof(string);
        foreach (var m in t.GetMethods())
            Console.WriteLine(m);
    }

    public static void AmpelnErstellen()
    {
        Ampel a = Ampel.Rot;
        Console.WriteLine(a);

        Ampel[] ampeln = new Ampel[10];
        for (int i=0; i<10; i++)
            ampeln[i] = Ampel.Gruen;
    }

    public static void ElementVonAufzaehlungVomNutzerEinlesen()
    {
        int wert = Convert.ToInt32(Console.ReadLine());
        if (Enum.IsDefined(typeof(Wochentage), wert))
            Console.WriteLine((Wochentage)wert);
    }

    public static void ElementeVonAufzaehlungIterieren() 
    {
        foreach (int wert in Enum.GetValues(typeof(Wochentage)))
        {
            string name = Enum.GetName(typeof(Wochentage), wert);
            Console.WriteLine("{0} - {1}", name, wert);
        }
    }

    public static (int min, int max) MinMax(int[] zahlen)
    {
        int min = zahlen[0];
        int max = zahlen[0];

        for (int i=1; i<zahlen.Length; i++)
        {
            if (min > zahlen[i])
                min = zahlen[i];
            
            if (max < zahlen[i])
                max = zahlen[i];
        }

        return (min, max);
    }

    public static int[] Zufallszahlen(int anzahl)
    {
        Random rnd = new Random();
        int[] zahlen = new int[anzahl];

        for (int i=0; i<zahlen.Length; i++)
            zahlen[i] = rnd.Next(0, 1000);

        return zahlen;
    }
}   