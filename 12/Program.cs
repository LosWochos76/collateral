public class Program
{
    public static void Main(string[] args)
    {
        //MethodenVonStringAuslesen();

        //(double, string, int) t = (17.4, "Hallo", 42);
        //Console.WriteLine(t);
        var t = (Bool: true, Value: 41);
        Console.WriteLine(t.Bool);

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


}