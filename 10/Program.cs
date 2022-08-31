public class Program
{
    public static void ZufallszahlenInArray()
    {
        Random rnd = new Random();
        int[] zahlen = new int[100];

        for (int i=0; i<100; i++)
        {
            zahlen[i] = rnd.Next(1, 7);
        }

        int summe = 0;
        for (int i=0; i<100; i++)
        {
            summe += zahlen[i];
        }
    }

    public static void Main(string[] args)
    {
        
    }
}