var graph = new UndirectedWeightedGraphAL();
graph.AddEdge(0, 1, 2);
graph.AddEdge(0, 3, 6);
graph.AddEdge(1, 2, 3);
graph.AddEdge(1, 3, 8);
graph.AddEdge(1, 4, 5);
graph.AddEdge(2, 4, 7);
graph.AddEdge(3, 4, 9);

PrimAlgorithm prim = new PrimAlgorithm(graph);
prim.PrimMST();