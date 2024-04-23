using AUD.List;

namespace AUD.Sort
{
    public class BubbleSort : ArrayList
    {
        public BubbleSort(int[] data) : base(data)
        {
        }

        public BubbleSort(int count) : base(count)
        {
        }

        private bool BubbleUp(int length)
        {
            bool did_swap = false;

            for (int i = 1; i < length; i++)
            {
                if (data[i - 1] > data[i])
                {
                    Swap(i - 1, i);
                    did_swap = true;
                }
            }

            return did_swap;
        }

        public void Sort()
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!BubbleUp(data.Length - i))
                    return;
            }
        }
    }
}
