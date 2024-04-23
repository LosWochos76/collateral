using System.Reflection.Metadata.Ecma335;

public class MaxHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public MaxHeap()
    {
        heap = new List<T>();
    }

    public MaxHeap(T[] elements)
    {
        heap = new List<T>(elements);
        BuildMaxHeap();
    }

    public int Count
    {
        get { return heap.Count; }
    }

    public bool HasElements
    {
        get { return heap.Count > 0; }
    }

    private void BuildMaxHeap()
    {
        for (int i=heap.Count / 2; i>=0; i--)
            MaxHeapify(i);
    }

    public void Insert(T item)
    {
        heap.Add(item);
        HeapifyUp(heap.Count - 1);
    }

    public T ExtractMax()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        T max = heap[0];
        int lastIndex = heap.Count - 1;
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex);
        MaxHeapify(0);

        return max;
    }

    public T Max
    {
        get 
        { 
            if (!HasElements)
                throw new InvalidOperationException("Heap is empty");

            return heap[0];
        }        
    }

    private void MaxHeapify(int index)
    {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;
        int largestIndex = index;

        if (leftChildIndex < heap.Count && 
            heap[leftChildIndex].CompareTo(heap[largestIndex]) > 0)
        {
            largestIndex = leftChildIndex;
        }

        if (rightChildIndex < heap.Count && 
            heap[rightChildIndex].CompareTo(heap[largestIndex]) > 0)
        {
            largestIndex = rightChildIndex;
        }

        if (largestIndex != index)
        {
            Swap(index, largestIndex);
            MaxHeapify(largestIndex);
        }
    }

    private void HeapifyUp(int currentIndex)
    {
        if (currentIndex == 0)
            return;

        int parentIndex = (currentIndex - 1) / 2;
        if (heap[currentIndex].CompareTo(heap[parentIndex]) > 0)
        {
            Swap(currentIndex, parentIndex);
            HeapifyUp(parentIndex);
        }
    }

    private void Swap(int index1, int index2)
    {
        T temp = heap[index1];
        heap[index1] = heap[index2];
        heap[index2] = temp;
    }
}