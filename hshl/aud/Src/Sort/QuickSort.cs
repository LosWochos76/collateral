using AUD.List;

namespace AUD.Sort
{
    public class QuickSort : ArrayList
    {
        public QuickSort(int[] data) : base(data)
        {
        }

        public QuickSort(int count) : base(count)
        {
        }

        private int Partition(int from, int to)
        {
            int pivot = data[to];
            int left = from;
            int right = to;

            while (true)
            {
                while (right > from && data[right] >= pivot)
                    right--;

                while (data[left] < pivot)
                    left++;

                if (left < right)
                {
                    Swap(left, right);
                }
                else
                {
                    Swap(left, to);
                    return left;
                }
            }
        }

        private void Sort(int from, int to)
        {
            if (from < to)
            {
                int pivotIndex = Partition(from, to);
                Sort(from, pivotIndex - 1);
                Sort(pivotIndex + 1, to);
            }
        }

        public void Sort()
        {
            Sort(0, data.Length - 1);
        }
    }
}