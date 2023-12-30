var g = ExampleGraphs.Kruskal();
var k = new Kruskal(g);
var edges = k.Find();

foreach (var edge in edges)
    Console.WriteLine("{0}: {1}", edge, edge.Weight);
