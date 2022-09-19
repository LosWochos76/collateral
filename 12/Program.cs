public class Program
{
    public static void Main(string[] args)
    {
        int wert = Convert.ToInt32(Console.ReadLine());
        if (Enum.IsDefined(typeof(Wochentage), wert))
            Console.WriteLine((Wochentage)wert);
    }

    public static void Ampeln()
    {
        Ampel a = Ampel.Rot;
        Console.WriteLine(a);

        Ampel[] ampeln = new Ampel[10];
        for (int i=0; i<10; i++)
            ampeln[i] = Ampel.Gruen;
    }
}