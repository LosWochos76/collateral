public class ExampleGraphs
{
    public static IGraph Simple()
    {
        var graph = new GraphAL(6, true);
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 5);
        graph.AddEdge(3, 4);
        graph.AddEdge(4, 2);
        graph.AddEdge(4, 6);
        return graph;
    }

    public static IGraph HausVomNikolaus()
    {
        var graph = new GraphAL(5, false);
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(1, 4);
        graph.AddEdge(2, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(3, 4);
        graph.AddEdge(3, 5);
        graph.AddEdge(4, 5);
        return graph;
    }

    public static IGraph Kruskal()
    {
        var graph = new GraphAM(8, false);
        graph.AddEdge(1, 2, 4);
        graph.AddEdge(1, 3, 3);
        graph.AddEdge(1, 4, 5);
        graph.AddEdge(2, 4, 3);
        graph.AddEdge(2, 6, 6);
        graph.AddEdge(3, 4, 2);
        graph.AddEdge(3, 6, 8);
        graph.AddEdge(4, 5, 5);
        graph.AddEdge(4, 6, 6);
        graph.AddEdge(4, 7, 5);
        graph.AddEdge(4, 8, 7);
        graph.AddEdge(5, 8, 6);
        graph.AddEdge(6, 7, 4);
        graph.AddEdge(7, 8, 2);
        return graph;
    }

    public static IGraph Dijkstra()
    {
        var graph = new GraphAM(6, true);
        graph.AddEdge(1, 2, 7);
        graph.AddEdge(1, 3, 9);
        graph.AddEdge(1, 6, 14);
        graph.AddEdge(2, 3, 10);
        graph.AddEdge(2, 4, 15);
        graph.AddEdge(3, 4, 11);
        graph.AddEdge(3, 6, 2);
        graph.AddEdge(4, 5, 6);
        graph.AddEdge(6, 5, 9);
        return graph;
    }
}