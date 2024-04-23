public class Kruskal
{
    private IGraph graph;

    public Kruskal(IGraph graph)
    {
        this.graph = graph;

        if (graph.IsDirected)
            throw new Exception("Graph muss ungerichtet sein!");
    }

    public IEnumerable<Edge> Find()
    {
        var edges = new List<Edge>(graph.AllEdges);
        edges.Sort();

        var st = new HashSet<Edge>();
        var ds = new DisjointSet();
        for (int i=1; i<=graph.NodeCount; i++)
            ds.MakeSet(i);
        
        while (edges.Count > 0 && ds.Count > 1)
        {
            var e = edges.First();
            edges.Remove(e);
            
            if (!ds.AreInSameSet(e.U, e.V))
            {
                st.Add(e);
                ds.Union(e.U, e.V);
            }
        }

        return st;
    }
}