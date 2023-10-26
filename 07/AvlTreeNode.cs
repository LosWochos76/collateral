public class AvlTreeNode<T> where T : IComparable<T>
{
    public T Value { get; set; }
    public int Height { get; set; } = 1;
    public AvlTreeNode<T> Left { get; set; }
    public AvlTreeNode<T> Right { get; set; }

    public AvlTreeNode(T value)
    {
        Value = value;
    }

    public bool IsSmallerOrEqualThan(T value)
    {
        return value.CompareTo(Value) >= 0;
    }

    public bool IsLagerThan(T value)
    {
        return value.CompareTo(Value) < 0;
    }

        public bool IsSmallerThan(T value)
    {
        return value.CompareTo(Value) > 0;
    }

    public bool Equals(T value)
    {
        return Value.Equals(value);
    }
}