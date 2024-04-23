using System;
using System.Collections;
using System.Collections.Generic;

namespace AUD.List
{
    public class GenericLinkedListEnumertor<T> : IEnumerator<T> where T : IComparable
    {
        private GenericLinkedList<T> list;
        private GenericListNode<T> current;

        public GenericLinkedListEnumertor(GenericLinkedList<T> list)
        {
            this.list = list;
        }

        public T Current
        {
            get
            {
                return current.value;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (current == null)
                current = list.Head;
            else
                current = current.next;

            return current != null;
        }

        public void Reset()
        {
            current = null;
        }
    }
}
