namespace Vorlesung
{
    public class Program
    {
        public static void Uhrzeit()
        {
            int now_hour = 16;
            int now_minute = 40;
            int add_minutes = 1784;

            int then = now_hour * 60 + now_minute + add_minutes;
            int then_hour = (then / 60) % 24;
            int then_minute = then % 60;

            Console.WriteLine("Es ist {0}:{1} Uhr", then_hour, then_minute);
        }

        public static void Summe3oder5Teilbar()
        {
            int z = 1;
            int summe = 0;

            while (z <= 100)
            {
                summe += z;
                z++;
            }

            Console.WriteLine("Summe={0}", summe);
        }

        public static void Zinseszins()
        {
            double monatlicher_zins = 1.5;
            double monatliche_sparrate = 100;
            double guthaben = 0;

            int monate = 24;
            int i = 0;

            // Die Laufbedingung kann so geändert werden, dass sie sich nicht auf die Monate, sondern auf das Guthaben bezieht:
            while (i <= monate)
            {
                guthaben += monatliche_sparrate + (guthaben / 100 * monatlicher_zins);
                i++;

                Console.WriteLine("Guthaben nach {0} Monaten: {1:C}", i, guthaben);
            }
        }

        public static void Main(string[] args)
        {
            // Kommentarzeichen löschen, um den jeweiligen Code auszuprobieren:
            //Uhrzeit();
            //Summe3oder5Teilbar();
            Zinseszins();
        }
    }
}