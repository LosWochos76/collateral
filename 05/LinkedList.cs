using System.Collections;

public partial class LinkedList<T>
{
    private NodeElement<T> head = null;
    public int Count { get; set; }

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

    public bool HasElements 
    {
        get { return head != null; }
    }

    public T Remove()
    {
        if (!HasElements)
            throw new Exception("List is empty!");

        var value = head.Value;
        head = head.Next;
        return value;
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
}