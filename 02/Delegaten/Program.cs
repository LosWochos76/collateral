public class Program
{
    public static void Main()
    {
        //Taschenrechner2 t = new Taschenrechner2();
        //t.Rechne();

        Bankkonto k = new Bankkonto();
        k.KontostandGeandert += KontostandGeaendert;

        k.Einzahlen(100);
        k.Auszahlen(50);
    }

    private static void KontostandGeaendert(Bankkonto quelle, double neuer_kontostand) 
    {
        Console.WriteLine("Kontostand geändert! Neuer Kontostand={0:C}", neuer_kontostand);
    }
}