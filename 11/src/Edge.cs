public class Edge<T>
{
    public Node<T> Source { get; private set; }
    public Node<T> Dest { get; private set; }
    public double Length { get; private set; }

    public Edge(Node<T> source, Node<T> dest, double length)
    {
        Source = source;
        Dest = dest;
        Length = length;
    }

    public Edge(Node<T> source, Node<T> dest) : this(source, dest, 1)
    {
    }
}