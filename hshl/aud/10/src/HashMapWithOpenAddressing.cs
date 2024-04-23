public class HashMapWithOpenAddressing<K, V> where K : IComparable
{
    public int Size { get; private set; }
    public int Count {get; private set; } = 0;
    public double LoadFactor { get { return (double)Count / Size; } }
    private Tuple<K, V>[] items;

    public HashMapWithOpenAddressing(int size)
    {
        this.Size = size;
        items = new Tuple<K, V>[size];
    }

    public HashMapWithOpenAddressing() : this(100) 
    {
    }

    private int GetHashCode(K key, int z)
    {
        return (key.GetHashCode() + z ) % Size;
    }

    public void Insert(K key, V value)
    {
        for (int z=0; z<Size; z++)
        {
            int pos = GetHashCode(key, z);
            if (items[pos] == null)
            {
                items[pos] = new Tuple<K, V>(key, value);;
                Count++;
                break;
            }
            else if (items[pos].Key.Equals(key))
            {
                items[pos].Value = value;
                break;
            }
        } 

        ReorganizeIfNecessary();
    }

    private void ReorganizeIfNecessary()
    {
        if (LoadFactor < 0.75)
            return;

        var new_hm = new HashMapWithOpenAddressing<K, V>(Size * 2);
        for (int i=0; i<Size; i++)
        {
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

    private int FindPos(K key)
    {
        for (int z=0; z<Size; z++)
        {
            int pos = GetHashCode(key, z);
            if (items[pos] != null && items[pos].Key.Equals(key))
                return pos;
        }

        return -1;
    }

    public void Remove(K key)
    {
        int pos = FindPos(key);
        if (pos != -1)
            items[pos] = null;

        Count--;
    }

    public bool ContainsKey(K key)
    {
        return FindPos(key) != -1;
    }

    public V GetValue(K key)
    {
        var pos = FindPos(key);
        if (pos == -1)
            throw new Exception($"Key '{key}' not found!");
        else
            return items[pos].Value;
    }
}