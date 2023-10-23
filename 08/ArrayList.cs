using System.Collections;

public partial class ArrayList<T> where T : IComparable<T>
{
    private int size, count;
    private T[] data;

    public ArrayList(int size)
    {
        this.size = size;
        this.data = new T[size];
    }

    public ArrayList() : this(10)
    {
    }

    public int Count
    {
        get { return count; }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index > count - 1)
                throw new ArgumentException("Bad index!");
            
            return data[index];
        }

        set
        {
            if (index < 0 || index > count - 1)
                throw new ArgumentException("Bad index!");
            
            data[index] = value;
        }
    }

    public void Add(T item)
    {
        EnsureSize(count + 1);
        data[count] = item;
        count++;
    }

    private void EnsureSize(int new_size)
    {
        if (new_size <= size)
            return;

        T[] new_data = new T[size * 2];
        Array.Copy(data, new_data, size);
        data = new_data;
        size *= 2;
    }

    public void Print()
    {
        for (int i=0; i<count; i++)
            Console.WriteLine(data[i]);
    }

    public void SelectionSort()
    {
        for (int i=0; i<count; i++)
        {
            var min = FindSmallestIndex(i);
            SwapValues(i, min);
        }
    }

    public int FindSmallestIndex(int start)
    {
        var min = data[start];
        var min_index = start;

        for (int i=start+1; i<count; i++)
        {
            if (data[i].CompareTo(min) < 0)
            {
                min = data[i];
                min_index = i;
            }
        }

        return min_index;
    }

    private void SwapValues(int index1, int index2)
    {
        var temp = data[index1];
        data[index1] = data[index2];
        data[index2] = temp;
    }

    private bool BubbleUp(int length)
    {
        bool did_swap = false;
        for (int i = 1; i < length; i++)
        {
            if (data[i].CompareTo(data[i - 1]) < 0)
            {
                SwapValues(i - 1, i);
                did_swap = true;
            }
        }

        return did_swap;
    }

    public void BubbleSort()
    {
        for (int i = 0; i < count; i++)
        {
            if (!BubbleUp(count - i))
                return;
        }
    }
}