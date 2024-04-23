using System;
using System.Collections;
using System.Collections.Generic;

namespace AUD.List
{
    public class GenericLinkedList<T> : IEnumerable<T> where T : IComparable
    {
        public GenericListNode<T> Head { get; private set; }
        public GenericListNode<T> Tail { get; private set; }
        public int Count { get; private set; }

        public void PushFront(T value)
        {
            var element = new GenericListNode<T>(value);
            
            if (Head == null)
            {
                Head = element;
                Tail = element;
                Count = 1;
            }
            else
            {
                element.next = Head;
                Head = element;
                Count++;
            }
        }

        public void PushBack(T value)
        {
            var element = new GenericListNode<T>(value);

            if (Tail == null)
            {
                Head = element;
                Tail = element;
                Count = 1;
            }
            else
            {
                Tail.next = element;
                Tail = element;
                Count++;
            }
        }

		public T PopFront()
		{
            T value = Head.value;
            Head = Head.next;
            Count--;
            return value;
		}

        public bool Contains(T value)
        {
            GenericListNode<T> node = Head;
            while (node != null)
            {
                if (node.value.CompareTo(value) == 0)
                    return true;

                node = node.next;
            }

            return false;
        }

        private GenericListNode<T> FindNodeToInsert(T value)
        {
            GenericListNode<T> currentElement = Head.next;
            GenericListNode<T> lastElement = Head;

            do
            {
                if (currentElement.value.CompareTo(value) > 0)
                    return lastElement;

                lastElement = currentElement;
                currentElement = currentElement.next;
            }
            while (currentElement != null);

            return null;
        }

        public void InsertSorted(T value)
        {
	        if (Count == 0)
		        PushFront(value);
	        else if (Head.value.CompareTo(value) > 0)
		        PushFront(value);
	        else if (Tail.value.CompareTo(value) < 0)
		        PushBack(value);
	        else
            {
                var elementToInsertAfter = FindNodeToInsert(value);
                var newElement = new GenericListNode<T>(value);
                newElement.next = elementToInsertAfter.next;
                elementToInsertAfter.next = newElement;
	        }
        }

		public bool IsSorted
        {
            get
            {
                if (Count < 2)
                    return true;

                GenericListNode<T> last = Head;
                GenericListNode<T> current = Head.next;

                while (current != null)
                {
                    if (current.value.CompareTo(last.value) < 0)
                        return false;

                    last = current;
                    current = current.next;
                }

                return true;
            }
        }

        public void RemoveAll(T value)
        {
            while (Head != null && Head.value.CompareTo(value) == 0)
            {
                Head = Head.next;
                Count--;
            }

            if (Count == 0)
                return;

            GenericListNode<T> last = Head;
            GenericListNode<T> current = Head.next;

            while (current != null)
            {
                if (current.value.CompareTo(value) == 0)
                {
                    last.next = current.next;
                    Count--;
                }

                last = current;
                current = current.next;
            }
        }

        public List<T> AsList()
        {
            var result = new List<T>();
            GenericListNode<T> current = Head;

            while (current != null)
            {
                result.Add(current.value);
                current = current.next;
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new GenericLinkedListEnumertor<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}