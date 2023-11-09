using System.Runtime.CompilerServices;

public class HashMap<K, V> where K : IComparable
{
    public int Size { get; private set; }
    public int Count {get; private set; } = 0;
    public double LoadFactor { get { return (double)Count / Size; } }
    private LinkedList<Tuple<K, V>>[] items;

    public HashMap(int size)
    {
        this.Size = size;
        items = new LinkedList<Tuple<K, V>>[size];
    }

    public HashMap() : this(100) 
    {
    }

    public void Insert(K key, V value)
    {
        int pos = key.GetHashCode() % Size;
        if (items[pos] == null)
            items[pos] = new LinkedList<Tuple<K, V>>();

        items[pos].Push(new Tuple<K, V>(key, value));
        Count++;

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

            var current = items[i].Head;
            while (current != null)
            {
                new_hm.Insert(current.Value.Key, current.Value.Value);
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

        var has_removed = items[pos].RemoveFirst(t => { 
            return t.Key.Equals(key); 
        });

        if (has_removed)
            Count--;
    }

    public bool ContainsKey(K key)
    {
        int pos = key.GetHashCode() % Size;
        if (items[pos] != null)
        {
            return items[pos].FindFirst(t => { 
                return t.Key.Equals(key); 
            }) != null;
        }

        return false;
    }

    public V GetValue(K key)
    {
        int pos = key.GetHashCode() % Size;
        if (items[pos] != null)
        {
            return items[pos].FindFirst(t => { 
                return t.Key.Equals(key); 
            }).Value.Value;
        }

        throw new Exception($"Key '{key}' not found!");
    }
}