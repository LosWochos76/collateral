public class BinarySearchTree<T> where T : IComparable<T>
{
	private TreeNode<T> root = null;
	public int Count { get; private set; }

	public void Insert(T value)
	{
		root = InsertRecursive(root, value);
	}

	private TreeNode<T> InsertRecursive(TreeNode<T> parent, T value)
	{
		if (parent == null)
		{
			parent = new TreeNode<T>(value);
			Count++;
		}
		else if (value.CompareTo(parent.Value) < 0)
		{
			parent.Left = InsertRecursive(parent.Left, value);
		}
		else
		{
			parent.Right = InsertRecursive(parent.Right, value);
		}
		
		return parent;
	}

	public void Print()
	{
		PrintRecursive(root);
	}

	private void PrintRecursive(TreeNode<T> node)
	{
		if (node == null)
			return;
			
		Console.WriteLine(node.Value);
		PrintRecursive(node.Left);
		PrintRecursive(node.Right);
	}

	public bool Contains(T value)
	{
		if (root == null)
			return false;
		
		return ContainsRecursive(root, value);
	}

	private bool ContainsRecursive(TreeNode<T> node, T value)
	{
		if (node == null)
			return false;
		else if (node.Value.Equals(value))
			return true;
		else if (value.CompareTo(node.Value) < 0)
			return ContainsRecursive(node.Left, value);
		else
			return ContainsRecursive(node.Right, value);
	}
}