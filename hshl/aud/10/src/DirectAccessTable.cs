using CommunityToolkit.Diagnostics;

public class DirectAccessTable<V>
{
    private int size;
    private Tuple<uint, V>[] items;

    public DirectAccessTable(int size)
    {
        this.size = size;
        items = new Tuple<uint, V>[size];
    }

    public void Insert(uint key, V value)
    {
        Guard.IsLessThan(key, size);
        items[key] = new Tuple<uint, V>(key, value);
    }

    public void Remove(uint key)
    {
        Guard.IsLessThan(key, size);
        items[key] = null;
    }

    public bool ContainsKey(uint key)
    {
        Guard.IsLessThan(key, size);
        return items[key] != null;
    }

    public V GetValue(uint key)
    {
        Guard.IsLessThan(key, size);
        return items[key].Value;
    }
}