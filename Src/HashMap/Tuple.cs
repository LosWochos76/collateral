using System;

namespace AUD.HashMap
{
    public class Tuple : IComparable
    {
        public int Key { get; set; }
        public string Value { get; set; }

        public Tuple(int key, string value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(object obj)
        {
            var kv = obj as Tuple;
            if (kv == null)
                throw new Exception("Not of type Tuple!");

            if (kv.Key > Key)
                return 1;

            if (kv.Key < Key)
                return -1;

            return 0;
        }
    }
}