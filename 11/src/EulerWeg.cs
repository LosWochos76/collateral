public class EulerWeg
{
    private IGraph graph;
    private UndirectedPath path;
    private HashSet<string> solutions;

    public EulerWeg(IGraph graph)
    {
        this.graph = graph;
        path = new UndirectedPath();
    }

    public HashSet<string> FindSolutions()
    {
        solutions = new HashSet<string>();

        foreach (var node in graph.AllNodes)
        {
            Visit(node);
        }

        return solutions;
    }

    private void Visit(int node)
    {
        path.PushNode(node);

        if (path.Count == graph.EdgeCount + 1)
        {
            solutions.Add(path.ToString());
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