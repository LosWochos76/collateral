public class AvlTree<T> where T : IComparable<T>
{
	private ComparableTreeNode<T> root = null;
	public int Count { get; private set; }

	public void Insert(T value)
	{
		root = InsertRecursive(root, value);
	}

	private ComparableTreeNode<T> InsertRecursive(ComparableTreeNode<T> parent, T value)
	{
		if (parent == null)
		{
			parent = new ComparableTreeNode<T>(value);
			Count++;
		}
		else if (parent.IsSmallerOrEqualThan(value))
			parent.Right = InsertRecursive(parent.Right, value);
		else
			parent.Left = InsertRecursive(parent.Left, value);

		parent.Height = 1 + Math.Max(Height(parent.Left), Height(parent.Right));
		int balance = BalanceFactor(parent);

		// Links zu schwer
		if (balance > 1)
		{
			if (parent.Left.IsSmallerOrEqualThan(value))
				parent.Left = RotateLeft(parent.Left);
			
			return RotateRight(parent);
		}

		// Rechts zu schwer
		if (balance < -1)
		{
			if (!parent.Right.IsSmallerOrEqualThan(value))
				parent.Right = RotateRight(parent.Right);
			
			return RotateLeft(parent);
		}

		return parent;
	}

	private ComparableTreeNode<T> RotateRight(ComparableTreeNode<T> y)
    {
		Console.WriteLine("Rotate Right: {0}", y.Value);
        var x = y.Left;
        var temp = x.Right;

        x.Right = y;
        y.Left = temp;

        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

        return x;
    }

    private ComparableTreeNode<T> RotateLeft(ComparableTreeNode<T> x)
    {
		Console.WriteLine("Rotate Left: {0}", x.Value);
        var y = x.Right;
        var temp = y.Left;

        y.Left = x;
        x.Right = temp;

        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

        return y;
    }

	private int Height(ComparableTreeNode<T> node)
    {
        if (node == null)
            return 0;

        return node.Height;
    }

	private int BalanceFactor(ComparableTreeNode<T> node)
    {
        if (node == null)
            return 0;

        return Height(node.Left) - Height(node.Right);
    }

	public void Print()
	{
		Print(root);
	}

	private void Print(ComparableTreeNode<T> node)
	{
		if (node == null)
			return;
		
		Console.WriteLine("{0}:{1}", node.Value, node.Height);
		Print(node.Left);
		Print(node.Right);
	}

	public bool Contains(T value)
	{
		return ContainsRecursive(root, value);
	}

	private bool ContainsRecursive(ComparableTreeNode<T> node, T value)
	{
		if (node == null)
			return false;
		else if (node.Equals(value))
			return true;
		else if (node.IsSmallerOrEqualThan(value))
			return ContainsRecursive(node.Right, value);
		else
			return ContainsRecursive(node.Left, value);
	}

	public T GetMinimumValue()
	{
		if (root == null)
			throw new Exception("Empty tree has no minumim!");

		return GetMinimumValue(root);
	}

	private T GetMinimumValue(ComparableTreeNode<T> parent)
	{
		if (parent.Left != null)
			return GetMinimumValue(parent.Left);
		
		return parent.Value;
	}

	public T GetMaximumValue()
	{
		if (root == null)
			throw new Exception("Empty tree has no maximum!");

		return GetMaximumValue(root);
	}

	private T GetMaximumValue(ComparableTreeNode<T> parent)
	{
		if (parent.Right != null)
			return GetMaximumValue(parent.Right);
		
		return parent.Value;
	}
}