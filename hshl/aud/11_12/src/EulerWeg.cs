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
            Visit(node, new Path());

        return solutions;
    }

    private void Visit(int u, Path path)
    {
        if (path.EdgeCount == graph.EdgeCount)
        {
            solutions.Add(path.ToString());
        }
        else
        {
            foreach (var e in graph.GetEdgesFrom(u))
            {
                if (!path.IsPartOf(e))
                {
                    path.Add(e);
                    Visit(e.V, path);
                    path.Pop();
                }
            }
        }
    }
}