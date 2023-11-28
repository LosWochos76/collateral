public class EulerWeg
{
    private IGraph graph;
    private HashSet<string> solutions;

    public EulerWeg(IGraph graph)
    {
        this.graph = graph;
    }

    public HashSet<string> FindSolutions()
    {
        solutions = new HashSet<string>();
        foreach (var node in graph.AllNodes)
            Visit(node, new UndirectedPath());

        return solutions;
    }

    private void Visit(int node, UndirectedPath path)
    {
        path.PushNode(node);

        if (path.NodeCount - 1 == graph.EdgeCount)
        {
            solutions.Add(path.ToString());
        }
        else
        {
            foreach (var neighbour in graph.GetNeighborsOf(node))
            {
                if (!path.IsEdgeInPath(node, neighbour))
                {
                    Visit(neighbour, path);
                }
            }
        }

        path.PopNode();
    }
}