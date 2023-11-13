using System.Runtime.CompilerServices;

public class HashMap<K, V> where K : IComparable
{
    public int Size { get; private set; }
    public int Count {get; private set; } = 0;
    public double LoadFactor { get { return (double)Count / Size; } }
    private Tuple<K, V>[] items;

    public HashMap(int size)
    {
        this.Size = size;
        items = new Tuple<K, V>[size];
    }

    public HashMap() : this(100) 
    {
    }

    public void Insert(K key, V value)
    {
        int pos = key.GetHashCode() % Size;
        var item = new Tuple<K, V>(key, value);
        Count++;

        if (items[pos] == null)
        {
            items[pos] = item;
        }
        else
        {
            item.Next = items[pos];
            items[pos] = item;
        }

        if (LoadFactor > 0.75)
            Reorganize();
    }

    private void Reorganize()
    {
        var new_hm = new HashMap<K, V>(Size * 2);
        for (int i=0; i<Size; i++)
        {
            if (items[i] == null)
                continue;

            var current = items[i];
            while (current != null)
            {
                new_hm.Insert(current.Key, current.Value);
                current = current.Next;
            }
        }

        Size = Size * 2;
        items = new_hm.items;
    }

    public void Remove(K key)
    {
        int pos = key.GetHashCode() % Size;
        if (items[pos] == null)
            return;

        if (items[pos].Key.Equals(key))
        {
            items[pos] = items[pos].Next;
            Count--;
            return;
        }

        Tuple<K, V> last = null;
        Tuple<K, V> current = items[pos];
        while (current != null && !current.Key.Equals(key))
        {
            last = current;
            current = current.Next;
        }

        if (current != null)
        {
            last.Next = current.Next;
            Count--;
        }
    }

    private Tuple<K, V> Find(K key)
    {
        int pos = key.GetHashCode() % Size;
        if (items[pos] == null)
            return null;
        
        Tuple<K, V> current = items[pos];
        while (current != null)
        {
            if (current.Key.Equals(key))
                return current;
    
            current = current.Next;
        }

        return null;
    }

    public bool ContainsKey(K key)
    {
        return Find(key) != null;
    }

    public V GetValue(K key)
    {
        var item = Find(key);

        if (item == null)
            throw new Exception($"Key '{key}' not found!");
        else
            return item.Value;
    }
}