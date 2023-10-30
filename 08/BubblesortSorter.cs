public class BubblesortSorter
{
    public static void Sort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (!BubbleUp(array, array.Length - i))
                return;
        }
    }

    private static bool BubbleUp(int[] array, int length)
    {
        bool did_swap = false;
        for (int i = 1; i < length; i++)
        {
            if (array[i] < array[i - 1])
            {
                ArrayHelper.Swap(array, i, i - 1);
                did_swap = true;
            }
        }

        return did_swap;
    }
}