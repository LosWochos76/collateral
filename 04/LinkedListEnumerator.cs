using System.Collections;

public partial class LinkedList<T> : ICollection<T>
{
    private class LinkedListEnumerator<T> : IEnumerator<T>
    {
        private LinkedList<T> list;
        private NodeElement<T> current;

        public LinkedListEnumerator(LinkedList<T> list)
        {
            this.list = list;
            current = list.head;
        }

        public T Current 
        {
            get { return current.Value; }
            set { current.Value = value; }
        }

        object IEnumerator.Current 
        {
            get { return current.Value; }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (current.Next == null)
                return false;

            current = current.Next;
            return true;
        }

        public void Reset()
        {
            current = list.head;
        }
    }
}