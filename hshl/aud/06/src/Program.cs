/*var list = new ArrayList<int>();
for (int i=0; i<10; i++)
    list.Add(i);
Console.WriteLine(list.Contains(9));*/

/*var list = new LinkedList<int>();
for (int i=0; i<10; i++)
    list.Push(i);
Console.WriteLine(list.Contains(0));*/

/*var list = new ArrayList<int>(
    new int[]{ 1, 3, 9, 15, 22, 24, 30, 31, 44, 68, 73 }
);
Console.WriteLine(list.ContainsBinary(73));*/

var tree = new BinarySearchTree<int>();
tree.Insert(8);
tree.Insert(3);
tree.Insert(10);
tree.Insert(1);
tree.Insert(6);
tree.Insert(14);
tree.Insert(4);
tree.Insert(7);
tree.Insert(13);
Console.WriteLine(tree.Contains(13));

/*var root = new TreeNode<string>("*");
root.Left = new TreeNode<string>("/");
root.Right = new TreeNode<string>("^");
root.Left.Left = new TreeNode<string>("+");
root.Left.Right = new TreeNode<string>("8");
root.Right.Left = new TreeNode<string>("4");
root.Right.Right = new TreeNode<string>("2");
root.Left.Left.Left = new TreeNode<string>("5");
root.Left.Left.Right = new TreeNode<string>("7");
Console.WriteLine(ExpressionSolver.Solve(root));*/