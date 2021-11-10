using System;

namespace AUD.List
{
    public class GenericTuple<K, V> : IComparable where K : IComparable 
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public GenericTuple(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(object obj)
        {
            var kv = obj as GenericTuple<K, V>;
            if (kv == null)
                throw new Exception("Not of type Tuple!");

            return kv.Key.CompareTo(Key);
        }
    }
}