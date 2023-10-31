using System.Formats.Tar;

public class MergesortSorter
{
    public static int[] Sort(int[] array)
    {
        return MergeSort(array, 0, array.Length - 1);
    }

    private static int[] MergeSort(int[] array, int left, int right)
    {
        if (left == right)
            return new int[] { array[left] };

        int middle = left + (right - left) / 2;
        int[] leftArray = MergeSort(array, left, middle);
        int[] rightArray = MergeSort(array, middle + 1, right);
        return Merge(leftArray, rightArray);
    }

    private static int[] Merge(int[] leftArray, int[] rightArray)
    {
        int leftLen = leftArray.Length, rightLen = rightArray.Length;
        int[] target = new int[leftLen + rightLen];
        int leftPos = 0, rightPos = 0, value;

        for (int i=0; i<target.Length; i++)
        {
            if (leftPos == leftLen && rightPos < rightLen)
                value = rightArray[rightPos++];
            else if (rightPos == rightLen && leftPos < leftLen)
                value = leftArray[leftPos++];
            else if (leftArray[leftPos] <= rightArray[rightPos])
                value = leftArray[leftPos++];
            else
                value = rightArray[rightPos++];

            target[i] = value;
        }

        return target;
    }
}