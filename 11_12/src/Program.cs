var g = ExampleGraphs.Simple();
var bfs = new BreadthFirstSearch(g);
bfs.Explore(1);
Console.WriteLine(bfs.MakePath(6));