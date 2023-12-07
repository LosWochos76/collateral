public class HausVomNikolaus
{
    public static IGraph Erzeuge()
    {
        var graph = new GraphAL(5, false);
        graph.AddEdge(1, 2, 1);
        graph.AddEdge(1, 3, 1);
        graph.AddEdge(1, 4, 1);
        graph.AddEdge(2, 3, 1);
        graph.AddEdge(2, 4, 1);
        graph.AddEdge(3, 4, 1);
        graph.AddEdge(3, 5, 1);
        graph.AddEdge(4, 5, 1);
        return graph;
    }
}