using System.Drawing;

public class HausVomNikolausMitTiefensuche
{
    private IGraph graph;
    private UndirectedPath path;

    public HausVomNikolausMitTiefensuche()
    {
        graph = new UndirectedGraphAL();
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(1, 4);
        graph.AddEdge(2, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(3, 4);
        graph.AddEdge(3, 5);
        graph.AddEdge(4, 5);

        path = new UndirectedPath();
    }

    public void FindSolutions()
    {
        foreach (var node in graph.AllNodes)
        {
            Visit(node);
        }
    }

    private void Visit(int node)
    {
        path.PushNode(node);

        if (path.Count == 9)
        {
            path.Print();
        }
        else
        {
            foreach (var neighbour in graph.GetNeighborsOf(node))
            {
                if (!path.IsEdgeInPath(node, neighbour))
                {
                    Visit(neighbour);
                }
            }
        }

        path.PopNode();
    }
}