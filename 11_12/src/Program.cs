var g = ExampleGraphs.Dijkstra();
var d = new Dijkstra(g);
Path p = d.FindShortestPath(1, 6);
Console.WriteLine(p);