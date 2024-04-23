using AUD.List;

namespace AUD
{
    public class MaxHeap : ArrayList
    {
        public MaxHeap(int[] data) : base(data)
        {
            BuildMaxHeap();
        }

        private void BuildMaxHeap()
        {
            for (int i = data.Length / 2; i >= 0; i--)
                MaxHeapify(data.Length, i);
        }

        private void MaxHeapify(int length, int index)
        {
            int leftIndex = index * 2 + 1;
            int rightIndex = index * 2 + 2;
            int maxIndex = index;

            if ((leftIndex < length) && data[leftIndex] > data[index])
                maxIndex = leftIndex;

            if ((rightIndex < length) && data[rightIndex] > data[maxIndex])
                maxIndex = rightIndex;

            if (maxIndex != index)
            {
                Swap(maxIndex, index);
                MaxHeapify(length, maxIndex);
            }
        }

        public int Max
        {
            get
            {
                return data[0];
            }
        }

        public void Sort()
        {
            for (int length = data.Length-1; length > 0; length--)
            { 
                Swap(0, length);
                MaxHeapify(length, 0);
            }
        }
    }
}