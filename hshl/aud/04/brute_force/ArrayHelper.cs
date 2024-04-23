public class ArrayHelper
{
    public bool Contains(int[] zahlen, int n)
    {
        for (int i=0; i<zahlen.Length; i++)
            if (zahlen[i] == n)
                return true;

        return false;
    }
}