public class ArrayHelper
{
    public static int Min(int[] zahlen) 
    {
        int min = zahlen[0];
        for (int i=1; i<zahlen.Length; i++)
        {
            if (zahlen[i] < min)
                min = zahlen[i];
        }

        return min;
    }

    public static int[] Zufallszahlen(int anzahl)
    {
        Random rnd = new Random();
        int[] zahlen = new int[anzahl];

        for (int i=0; i<100; i++)
            zahlen[i] = rnd.Next();

        return zahlen;
    }

    public static int Summe(int[] zahlen)
    {
        int summe = 0;
        for (int i=0; i<zahlen.Length; i++)
            summe += zahlen[i];

        return summe;
    }

    public static double ArithmetischerMittelwert(int[] zahlen)
    {
        return Summe(zahlen) / (double)zahlen.Length;
    }
}