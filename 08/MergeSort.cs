public class MergeSort
{
    public static int[] Sort(int[] elements) {
        return Sort(elements, 0, elements.Length - 1);
    }

    private static int[] Sort(int[] elements, int left, int right) {
        if (left == right) 
            return new int[]{elements[left]};

        int middle = left + (right - left) / 2;
        int[] leftArray = Sort(elements, left, middle);
        int[] rightArray = Sort(elements, middle + 1, right);
        return Merge(leftArray, rightArray);
    }

    public static int[] Merge(int[] leftArray, int[] rightArray)
    {
        int leftLen = leftArray.Length;
        int rightLen = rightArray.Length;
        int[] result = new int[leftLen + rightLen];

        int targetPos = 0;
        int leftPos = 0;
        int rightPos = 0;

        while (leftPos < leftLen && rightPos < rightLen) {
            int leftValue = leftArray[leftPos];
            int rightValue = rightArray[rightPos];
            if (leftValue <= rightValue) {
                result[targetPos++] = leftValue;
                leftPos++;
            } else {
                result[targetPos++] = rightValue;
                rightPos++;
            }
        }

        while (leftPos < leftLen) {
            result[targetPos++] = leftArray[leftPos++];
        }

        while (rightPos < rightLen) {
            result[targetPos++] = rightArray[rightPos++];
        }

        return result;
    }
}