public class ArrayHelper
{
    public static int[] Zufallszahlen(int n)
    {
        Random rnd = new Random();
        int[] result = new int[n];

        for (int i=0; i<n; i++)
            result[i] = rnd.Next();

        return result;
    }

    public static void SelectionSort(int[] zahlen)
    {
        for (int i=0; i<zahlen.Length; i++)
        {
            int min_pos = FindMinPos(zahlen, i);
            Swap(zahlen, min_pos, i);
        }
    }

    public static int FindMinPos(int[] zahlen, int start_index)
    {
        int min = zahlen[start_index];
        int min_pos = start_index;

        for (int i=start_index+1; i<zahlen.Length; i++)
        {
            if (zahlen[i] < min)
            {
                min = zahlen[i];
                min_pos = i;
            }
        }

        return min_pos;
    }

    public static void Swap(int[] zahlen, int pos1, int pos2)
    {
        if (pos1 == pos2)
            return;
        
        int tmp = zahlen[pos1];
        zahlen[pos1] = zahlen[pos2];
        zahlen[pos2] = tmp;
    }

    public static void Print(int[] zahlen)
    {
        foreach (var p in zahlen)
            Console.WriteLine(p);
    }

    public static bool IsSorted(int[] zahlen)
    {
        for (int i=1; i<zahlen.Length; i++)
            if (zahlen[i-1] > zahlen[i])
                return false;
        
        return true;
    }
}