/*var tree = new AvlTree<int>();
tree.Insert(17);
tree.Insert(9);
tree.Insert(23);
tree.Insert(7);
tree.Insert(8);
tree.Print();
Console.WriteLine(tree.GetMaximumValue());*/

var tree = new AvlTree<int>();
tree.Insert(17);
tree.Insert(9);
tree.Insert(23);
tree.Insert(7);
tree.Insert(8);
Console.WriteLine(tree.ToDot());