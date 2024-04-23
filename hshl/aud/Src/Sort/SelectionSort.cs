using AUD.List;

namespace AUD.Sort
{
    public class SelectionSort : ArrayList
    {
        public SelectionSort(int[] data) : base(data)
        {
        }

        public SelectionSort(int count) : base(count)
        {
        }

        private int FindMinimumPosition(int start, int end)
        {
            int minValue = data[start];
            int minPos = start;

            for (int i = start + 1; i < end; i++)
            {
                if (data[i] < minValue)
                {
                    minValue = data[i];
                    minPos = i;
                }
            }

            return minPos;
        }

        public void Sort()
        {
            for (int i = 0; i < data.Length; i++)
            {
                int pos = FindMinimumPosition(i, data.Length);
                Swap(i, pos);
            }
        }
    }
}
