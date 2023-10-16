public class NodeElement<T>
{
    public T Value { get; set; }
    public NodeElement<T> Next { get; set; }

    public NodeElement(T value)
    {
        Value = value;
    }
}