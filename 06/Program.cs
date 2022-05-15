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

    public static void Main()
    {
        Wuerfelspiel();
    }
}