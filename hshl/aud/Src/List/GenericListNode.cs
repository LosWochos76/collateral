using System;

namespace AUD.List
{
    public class GenericListNode<T> where T : IComparable
    {
        public T value;
        public GenericListNode<T> next = null;

        public GenericListNode(T value)
        {
            this.value = value;
        }
    }
}
