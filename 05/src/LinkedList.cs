public partial class LinkedList<T>
{
    private NodeElement<T> head = null;
    private NodeElement<T> tail = null;
    public int Count { get; set; }

    public void Push(T item)
    {
        var element = new NodeElement<T>(item);
        if (head == null)
        {
            head = element;
            tail = element;
        }
        else
        {
            element.Next = head;
            head = element;
        }

        Count++;
    }

    public void Enqueue(T item)
    {
        var element = new NodeElement<T>(item);
        if (head == null)
        {
            head = element;
            tail = element;
        }
        else
        {
            tail.Next = element;
            tail = element;
        }

        Count++;
    }

    public bool HasElements 
    {
        get { return head != null; }
    }

    public T Pop()
    {
        if (!HasElements)
            throw new Exception("List is empty!");

        var value = head.Value;
        head = head.Next;

        if (head == null)
            tail = null;

        Count--;
        return value;
    }

    public T Dequeue()
    {
        return Pop();
    }

    public void Print()
    {
        var current = head;
        while (current != null)
        {
            Console.WriteLine(current.Value);
            current = current.Next;
        }
    }

    public void PrintRecursive()
    {
        PrintRecursive(head);
    }

    public void PrintRecursive(NodeElement<T> current)
    {
        if (current == null)
            return;

        Console.WriteLine(current.Value);
        PrintRecursive(current.Next);
    }
}