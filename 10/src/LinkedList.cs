using System.Collections;

public partial class LinkedList<T>
{
    public NodeElement<T> Head {get; private set; }
    public NodeElement<T> Tail {get; private set; }
    public int Count { get; set; }

    public void Push(T item)
    {
        var element = new NodeElement<T>(item);
        if (Head == null)
        {
            Head = element;
            Tail = element;
        }
        else
        {
            element.Next = Head;
            Head = element;
        }

        Count++;
    }

    public void Enqueue(T item)
    {
        var element = new NodeElement<T>(item);
        if (Head == null)
        {
            Head = element;
            Tail = element;
        }
        else
        {
            Tail.Next = element;
            Tail = element;
        }

        Count++;
    }

    public bool HasElements 
    {
        get { return Head != null; }
    }

    public T Pop()
    {
        if (!HasElements)
            throw new Exception("List is empty!");

        var value = Head.Value;
        Head = Head.Next;

        if (Head == null)
            Tail = null;

        Count--;
        return value;
    }

    public T Dequeue()
    {
        return Pop();
    }

    public bool Contains(T value)
    {
        return Contains(value, Head);
    }

    private bool Contains(T value, NodeElement<T> node)
    {
        if (node == null)
            return false;
        else if (node.Value.Equals(value))
            return true;
        else
            return Contains(value, node.Next);
    }

    public NodeElement<T> FindFirst(Predicate<T> predicate)
    {
        var current = Head;
        while (current != null)
        {
            if (predicate(current.Value))
                return current;
            
            current = current.Next;
        }

        return null;
    }

    public bool RemoveFirst(Predicate<T> predicate)
    {
        NodeElement<T> current = Head;
        NodeElement<T> last = null;

        if (predicate(current.Value))
        {
            Head = Head.Next;
            return true;
        }

        while (current != null && !predicate(current.Value))
        {
            last = current;
            current = current.Next;
        }

        if (current == null)
            return false;

        last.Next = current.Next;
        return true;
    }
}