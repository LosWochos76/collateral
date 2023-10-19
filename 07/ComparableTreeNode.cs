public class ComparableTreeNode<T> where T : IComparable<T>
{
    public T Value { get; set; }
    public int Height { get; set; } = 1;
    public ComparableTreeNode<T> Left { get; set; }
    public ComparableTreeNode<T> Right { get; set; }

    public ComparableTreeNode(T value)
    {
        Value = value;
    }

    public bool IsSmallerOrEqualThan(T value)
    {
        return value.CompareTo(Value) >= 0;
    }

    public bool Equals(T value)
    {
        return Value.Equals(value);
    }
}