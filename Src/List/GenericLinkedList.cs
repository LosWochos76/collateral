using System;
using System.Collections.Generic;

namespace AUD.List
{
    public class GenericLinkedList<T> where T : IComparable
    {
        private GenericListNode<T> head = null;
        private GenericListNode<T> tail = null;
        private int count = 0;

        public int Count
        {
            get { return count; }
        }

        public void PushFront(T value)
        {
            var element = new GenericListNode<T>(value);
            
            if (head == null)
            {
                head = element;
                tail = element;
                count = 1;
            }
            else
            {
                element.next = head;
                head = element;
                count++;
            }
        }

        public void PushBack(T value)
        {
            var element = new GenericListNode<T>(value);

            if (tail == null)
            {
                head = element;
                tail = element;
                count = 1;
            }
            else
            {
                tail.next = element;
                tail = element;
                count++;
            }
        }

		public T PopFront()
		{
            T value = head.value;
            head = head.next;
            count--;
            return value;
		}

        public bool Contains(T value)
        {
            GenericListNode<T> node = head;
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
            GenericListNode<T> currentElement = head.next;
            GenericListNode<T> lastElement = head;

            do
            {
                if (currentElement.value.CompareTo(value) > 0)
                    return lastElement;

                lastElement = currentElement;
                currentElement = currentElement.next;
            } while (currentElement != null);

            return null;
        }

        public void InsertSorted(T value)
        {
	        if (count == 0)
		        PushFront(value);
	        else if (head.value.CompareTo(value) > 0)
		        PushFront(value);
	        else if (tail.value.CompareTo(value) < 0)
		        PushBack(value);
	        else {
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
                if (count < 2)
                    return true;

                GenericListNode<T> last = head;
                GenericListNode<T> current = head.next;

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
            while (head != null && head.value.CompareTo(value) == 0)
            {
                head = head.next;
                count--;
            }

            if (count == 0)
                return;

            GenericListNode<T> last = head;
            GenericListNode<T> current = head.next;
            while (current != null)
            {
                if (current.value.CompareTo(value) == 0)
                {
                    last.next = current.next;
                    count--;
                }

                last = current;
                current = current.next;
            }
        }

        public List<T> AsList()
        {
            var result = new List<T>();
            GenericListNode<T> current = head;

            while (current != null)
            {
                result.Add(current.value);
                current = current.next;
            }

            return result;
        }
    }
}