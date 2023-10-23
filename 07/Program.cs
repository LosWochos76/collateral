/*var tree = new AvlTree<int>();
tree.Insert(17);
tree.Insert(9);
tree.Insert(23);
tree.Insert(7);
tree.Insert(8);
tree.Print();
Console.WriteLine(tree.GetMaximumValue());*/

var tree = new AvlTree<int>();
tree.Insert(7);
tree.Insert(3);
tree.Insert(22);
tree.Insert(1);
tree.Insert(5);
tree.Insert(14);
tree.Insert(35);
tree.Insert(4);
tree.Delete(3);
tree.Print();