public class BinarySearchTree<T> where T : IComparable<T>
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
		
		return parent;
	}

	public void Print()
	{
		Print(root);
	}

	private void Print(ComparableTreeNode<T> node)
	{
		if (node == null)
			return;
		
		Print(node.Left);
		Print(node.Right);
		Console.WriteLine(node.Value);
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
}