public class QuicksortSorter
{
    public static void Sort(int[] list)
    {
        Sort(list, 0, list.Length - 1);
    }

    private static void Sort(int[] list, int from, int to) 
    {
        if (from < to)
        {
            int pivotIndex = Partition(list, from, to);
            Sort(list, from, pivotIndex - 1);
            Sort(list, pivotIndex + 1, to);
        }
    }

    private static int Partition(int[] array, int from, int to)
    {
        int pivot = array[to], left = from, right = to;

        while (true)
        {
            while (right > from && array[right] >= pivot)
                right--;

            while (array[left] < pivot)
                left++;

            if (left < right)
            {
                ArrayHelper.Swap(array, left, right);
            }
            else
            {
                ArrayHelper.Swap(array, left, to);
                return left;
            }
        }
    }
}