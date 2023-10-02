public class ArrayHelper
{
    public static int[] GeneriereZufallszahlen(int anzahl)
    {
        Random rnd = new Random();
        int[] result = new int[anzahl];
        for (int i=0; i<anzahl; i++)
            result[i] = rnd.Next();

        return result;
    }

    public static int FindMax(int[] zahlen)
    {
        return FindMax(zahlen, 0, zahlen.Length-1);
    }

    private static int FindMax(int[] zahlen, int start, int end)
    {
        if (start == end)
            return zahlen[start];
        
        var mid = (start + end) / 2;
        var max_left = FindMax(zahlen, start, mid);
        var max_right = FindMax(zahlen, mid+1, end);
        return Math.Max(max_left, max_right);
    }
}