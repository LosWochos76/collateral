class Program
{
    public static void Main(string[] args)
    {
        // Berechnung des Bruttobetrags und der Mehrwertsteuer
        double bruttobetrag = (3 * 459 + 4 * 319 + 5 * 29.90) * 0.97;
        double mehrwertsteuer = bruttobetrag / 119 * 19;
        Console.WriteLine("Gesamtbetrag={0:C}, Mwst={1:C}",
           bruttobetrag, mehrwertsteuer
        );

        // Die Baumstamm-Aufgabe:
        Console.WriteLine("Stammlänge in m: ");
        double laenge = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Stammumfang in m: ");
        double umfang = Convert.ToDouble(Console.ReadLine());
        double volumen = Math.Pow(umfang, 2) / 4 / Math.PI * laenge;
        double gewicht = volumen * 870;
        Console.WriteLine("Volumen={0:F2} m^3 -- Gewicht={1:F2} kg.", volumen, gewicht);
    }
}