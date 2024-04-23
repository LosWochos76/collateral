using System.Text;

public class DotLayouter
{
    public static string ToDot(Edge e)
    {
        var weight = e.Weight != 1 ? 
            string.Format(" [label={0}]", e.Weight) : 
            string.Empty;

        if (e.IsDirected)
            return string.Format("{0} -> {1}{2};", e.U, e.V, weight);
        else
            return string.Format("{0} -- {1}{2};", e.U, e.V, weight);
    }

    public static string ToDot(IGraph graph)
    {
        var result = new StringBuilder();
        if (graph.IsDirected)
            result.Append("Digraph G {\n");
        else
            result.Append("Graph G {\n");

        foreach (var e in graph.AllEdges)
            result.Append(string.Format("\t{0}\n", ToDot(e)));

        result.Append("}");
        return result.ToString();
    }

    public static string ToDot(Path path)
    {
        if (path.EdgeCount == 0)
            return string.Empty;

        var result = new StringBuilder();
        if (path.First.IsDirected)
            result.Append("Digraph G {\n");
        else
            result.Append("Graph G {\n");

        foreach (var e in path.Edges)
            result.Append(string.Format("\t{0}\n", ToDot(e)));

        result.Append("}");
        return result.ToString();
    }
}