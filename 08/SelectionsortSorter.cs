public class SelectionsortSorter
{
    public static void Sort(int[] array)
    {
        for (int i=0; i<array.Length; i++)
        {
            var min_index = FindSmallestIndex(array, i);
            ArrayHelper.Swap(array, i, min_index);
        }
    }

    public static int FindSmallestIndex(int[] array, int start)
    {
        var min = array[start];
        var min_index = start;

        for (int i=start+1; i<array.Length; i++)
        {
            if (array[i] < min)
            {
                min = array[i];
                min_index = i;
            }
        }

        return min_index;
    }

    public static void Sort<T>(LinkedList<T> list) where T : IComparable<T>
    {
        var current = list.Head;
        while (current != null)
        {
            var min = FindSmallest(current);
            list.SwapValues(min, current);
            current = current.Next;
        }
    }

    private static NodeElement<T> FindSmallest<T>(NodeElement<T> start) where T : IComparable<T>
    {
        var min = start;
        var current = start.Next;

        while (current != null)
        {
            if (current.Value.CompareTo(min.Value) < 0)
                min = current;

            current = current.Next;
        }

        return min;
    }
}