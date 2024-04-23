using System;
using System.Collections.Generic;
using AUD.List;

namespace AUD.HashMap
{
    public class GenericHashMap<K, V> where K : IComparable
    {
        private int size;
        private LinkedList<GenericTuple<K, V>>[] items;

        public GenericHashMap(int size)
        {
            this.size = size;
            items = new LinkedList<GenericTuple<K, V>>[size];
        }

        public void Insert(K key, V value)
        {
            int pos = key.GetHashCode() % size;
            if (items[pos] == null)
                items[pos] = new LinkedList<GenericTuple<K, V>>();

            items[pos].AddLast(new GenericTuple<K, V>(key, value));
        }

        public bool ContainsKey(int key)
        {
            int pos = key.GetHashCode() % size;
            if (items[pos] != null)
            {
                foreach (var t in items[pos])
                {
                    if (t.Key.CompareTo(key) == 0)
                        return true;
                }
            }

            return false;
        }

        public V GetValue(int key)
        {
            int pos = key.GetHashCode() % size;

            if (items[pos] != null)
            {
                foreach (var t in items[pos])
                {
                    if (t.Key.CompareTo(key) == 0)
                        return t.Value;
                }
            }

            throw new Exception($"Key '{key}' not found!");
        }
    }
}