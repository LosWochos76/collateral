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
        if (path.Length == graph.EdgeCount)
        {
            solutions.Add(path.ToString());
        }
        else
        {
            foreach (var e in graph.GetEdgesFrom(u))
            {
                if (!path.IsInPath(e))
                {
                    path.Push(e);
                    Visit(e.Vertex2, path);
                    path.Pop();
                }
            }
        }
    }
}