public partial class LinkedList<T> where T : IComparable<T>
{
    public NodeElement<T> Head { get; private set; }
    public NodeElement<T> Tail { get; private set; }
    public int Count { get; private set; }

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

    public void Print()
    {
        var current = Head;
        while (current != null)
        {
            Console.WriteLine(current.Value);
            current = current.Next;
        }
    }
}