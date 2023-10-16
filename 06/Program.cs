/*var list = new ArrayList<int>();
for (int i=0; i<10; i++)
    list.Add(i);
Console.WriteLine(list.Contains(9));*/

/*var list = new LinkedList<int>();
for (int i=0; i<10; i++)
    list.Push(i);
Console.WriteLine(list.Contains(0));*/

var list = new ArrayList<int>(
    new int[]{ 1, 3, 9, 15, 22, 24, 30, 31, 44, 68, 73 }
);
Console.WriteLine(list.ContainsBinary(23));