public class AvlTree<T> where T : IComparable<T>
{
	private ComparableTreeNode<T> root = null;
	public int Count { get; private set; }

	public void Insert(T value)
	{
		root = InsertRecursive(root, value);
	}

	private ComparableTreeNode<T> InsertRecursive(ComparableTreeNode<T> node, T value)
	{
		if (node == null)
		{
			node = new ComparableTreeNode<T>(value);
			Count++;
		}
		else if (node.IsSmallerOrEqualThan(value))
			node.Right = InsertRecursive(node.Right, value);
		else
			node.Left = InsertRecursive(node.Left, value);

		var rebalanced = Rebalance(node, value);
		return rebalanced != null ? rebalanced : node;
	}

	private ComparableTreeNode<T> Rebalance(ComparableTreeNode<T> node, T value)
	{
		node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
		int balance = BalanceFactor(node);

		// Links zu schwer
		if (balance > 1)
		{
			if (node.Left.IsSmallerOrEqualThan(value))
				node.Left = RotateLeft(node.Left);
			
			return RotateRight(node);
		}

		// Rechts zu schwer
		if (balance < -1)
		{
			if (!node.Right.IsSmallerOrEqualThan(value))
				node.Right = RotateRight(node.Right);
			
			return RotateLeft(node);
		}

		return null;
	}

	private ComparableTreeNode<T> RotateRight(ComparableTreeNode<T> y)
    {
        var x = y.Left;
        var beta = x.Right;

        x.Right = y;
        y.Left = beta;

        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

        return x;
    }

    private ComparableTreeNode<T> RotateLeft(ComparableTreeNode<T> x)
    {
        var y = x.Right;
        var beta = y.Left;

        y.Left = x;
        x.Right = beta;

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

	public void Delete(T value)
	{
		root = Delete(root, value);
	}

	private ComparableTreeNode<T> Delete(ComparableTreeNode<T> node, T value)
	{
		if (node == null)
            return node;

        if (node.IsLagerThan(value))
            node.Left = Delete(node.Left, value);
        else if (node.IsSmallerThan(value))
            node.Right = Delete(node.Right, value);
        else
        {
            if (node.Left == null)
                return node.Right;
            else if (node.Right == null)
                return node.Left;

            node.Value = GetMinimumValue(node.Right);
            node.Right = Delete(node.Right, node.Value);;
        }

		var rebalanced = Rebalance(node, value);
		return rebalanced != null ? rebalanced : node;
	}

	public string ToDot()
	{
		return "digraph {\n" + ToDot(root) + "\n}";
	}

	private string ToDot(ComparableTreeNode<T> node)
	{
		if (node == null)
			return string.Empty;
		
		string result = string.Format("\t{0}[label=\"{1}\"];\n", node.GetHashCode().ToString(), node.Value);

		if (node.Left != null)
		{
			result += string.Format("\t{0} -> {1};\n", node.GetHashCode().ToString(), node.Left.GetHashCode().ToString()) + ToDot(node.Left);
		}

		if (node.Right != null)
		{
			result += string.Format("\t{0} -> {1};\n", node.GetHashCode().ToString(), node.Right.GetHashCode().ToString()) + ToDot(node.Right);
		}

		return result;
	}
}