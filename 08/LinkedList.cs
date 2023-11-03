public partial class LinkedList<T> where T : IComparable<T>
{
    private NodeElement<T> head = null;
    private NodeElement<T> tail = null;
    public int Count { get; set; }
    public NodeElement<T> Head { get { return head; } }

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

    public void SwapValues(NodeElement<T> a, NodeElement<T> b)
    {
        var temp = a.Value;
        a.Value = b.Value;
        b.Value = temp;
    }

    public static LinkedList<int> RandomNumbers(int count)
    {
        var rnd = new Random();
        var list = new LinkedList<int>();
        for (int i=0; i<count; i++)
            list.Enqueue(rnd.Next(100));
        
        return list;
    }
}