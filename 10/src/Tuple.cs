public class Tuple<K,V>
{
    public K Key { get; set; }
    public V Value { get; set; }
    public Tuple<K, V> Next { get; set; }

    public Tuple(K key, V value)
    {
        Key = key;
        Value = value;
    }
}