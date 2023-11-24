public class HausVomNikolaus
{
    public static IGraph Erzeuge()
    {
        var graph = new UndirectedGraphAL();
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
}