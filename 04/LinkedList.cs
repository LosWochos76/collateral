using System.Collections;

public partial class LinkedList<T> : ICollection<T>
{
    private NodeElement<T> head = null;

    public int Count { get; set; }

    public bool IsReadOnly 
    {
        get 
        {
            return false;
        }
    }

    public void Add(T item)
    {
        var element = new NodeElement<T>(item);
        if (head == null)
        {
            head = element;
        }
        else
        {
            element.Next = head;
            head = element;
        }

        Count++;
    }

    public void Clear()
    {
        head = null;
        Count = 0;
    }

    public bool Contains(T item)
    {
        var current = head;
        while (current != null)
        {
            if (current.Value.Equals(item))
                return true;

            current = current.Next;
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        int i = 0;
        var current = head;
        while (current != null)
        {
            array[i + arrayIndex] = current.Value;
            current = current.Next;
        }
    }

    public bool Remove(T item)
    {
        var current = head;
        NodeElement<T> last = null;

        while (current != null)
        {
            if (current.Value.Equals(item))
            {
                last.Next = current.Next;
                Count--;
                return true;
            }

            last = current;
            current = current.Next;
        }
        
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new LinkedListEnumerator<T>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new LinkedListEnumerator<T>(this);
    }
}