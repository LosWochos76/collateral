using System.Collections;

public partial class ArrayList<T>
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
}