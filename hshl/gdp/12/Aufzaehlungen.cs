public class Aufzaehlungen
{
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