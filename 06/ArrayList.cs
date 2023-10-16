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

    public ArrayList(T[] source) : this(source.Length)
    {
        Array.Copy(source, data, source.Length);
        count = source.Length;
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

    public bool Contains(T value)
    {
        for (int i=0; i<count; i++)
            if (data[i].Equals(value))
                return true;
        
        return false;
    }

    private void Swap(int pos1, int pos2)
    {
        if (pos1 == pos2)
            return;
        
        var tmp = data[pos1];
        data[pos1] = data[pos2];
        data[pos2] = tmp;
    }

    public bool ContainsBinary(T value)
    {
        return ContainsBinary(value, 0, count);
    }

    private bool ContainsBinary(T value, int from, int to)
    {
        if (to <= from)
            return false;

        int middle_pos = (from + to) / 2;
        int compare_result = value.CompareTo(data[middle_pos]);

        if (compare_result == 0)
            return true;
        else if (compare_result < 0)
            return ContainsBinary(value, from, middle_pos);
        else
            return ContainsBinary(value, middle_pos+1, to);
    }
}