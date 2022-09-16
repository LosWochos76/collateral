public class Program
{
    public static void Main(string[] args)
    {
        /*if (args.Length == 1)
            Console.WriteLine(Caesar.EncryptString(args[0]));*/

        // GibSchaltjahreAus(1900, 2100);

        TimeZoneInfo tzi = TimeZoneInfo.Local;
        Console.WriteLine(tzi.Id);
    }

    public static int Stunden(int jahr, int monat, int tag)
    {
        DateTime d = new DateTime(jahr, monat, tag, 0, 0, 0);
        d = DateTime.SpecifyKind(d, DateTimeKind.Local);
        int count = 0;

        while (d.Day == tag)
        {
            DateTime utc = TimeZoneInfo.ConvertTime(d, TimeZoneInfo.Utc);
            utc = utc.AddHours(1);
            d = TimeZoneInfo.ConvertTime(utc, TimeZoneInfo.Local);
            count++;
        }

        return count;
    }

    public static int TageBis(int tag, int monat)
    {
        DateTime d = DateTime.Today;
        int days = 0;

        while (d.Month != monat || d.Day != tag)
        {
            d = d.AddDays(1);
            days++;
        }

        return days;
    }

    public static void GibSchaltjahreAus(int von, int bis)
    {
        for (int jahr=von; jahr<=bis; jahr++)
            if (DateTime.IsLeapYear(jahr))
                Console.WriteLine("Das Jahr {0} ist ein Schaltjahr!", jahr);
    }
}