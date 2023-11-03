/*var tree = new AvlTree<int>();
tree.Insert(17);
tree.Insert(9);
tree.Insert(23);
tree.Insert(7);
tree.Insert(8);
tree.Print();
Console.WriteLine(tree.GetMaximumValue());*/

Random rnd = new Random();
var tree = new AvlTree<int>();
for (int i=0; i<10; i++)
    tree.Insert(rnd.Next(100));

while (tree.Count > 0)
    Console.WriteLine(tree.ExtractMin());