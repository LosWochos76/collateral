public class GraphAM : IGraph
{
    private Edge[,] adjacenz_matrix;
    public int NodeCount { get; private set; }
    public int EdgeCount { get; private set; }
    public bool IsDirected { get; private set; }
    public bool IsUndirected { get { return !IsDirected; } }

    public GraphAM(int node_count, bool is_directed)
    {
        NodeCount = node_count;
        IsDirected = is_directed;
        Clear();
    }

    public void AddEdge(int u, int v, double weight)
    {
        if (HasEdge(u,v))
            return;

        adjacenz_matrix[u-1, v-1] = new Edge(IsDirected, u, v, weight);

        if (IsUndirected)
            adjacenz_matrix[v-1, u-1] = new Edge(IsDirected, v, u, weight);;

        EdgeCount++;
    }

    public void DeleteEdge(int u, int v)
    {
        if (!HasEdge(u, v))
            return;

        adjacenz_matrix[u-1, v-1] = null;

        if (IsUndirected)
            adjacenz_matrix[v-1, u-1] = null;

        EdgeCount--;
    }

    public bool HasEdge(int u, int v)
    {
        return adjacenz_matrix[u-1, v-1] != null;
    }

    public IEnumerable<int> GetNeighborsOf(int u)
    {
        for (int v=1; v<=NodeCount; v++)
            if (HasEdge(u, v))
                yield return v;
    }

    public IEnumerable<Edge> GetEdgesFrom(int u)
    {
        for (int v=1; v<=NodeCount; v++)
            if (HasEdge(u, v))
                yield return adjacenz_matrix[u-1, v-1];
    }

    public double GetWeight(int u, int v)
    {
        if (HasEdge(u, v))
            return adjacenz_matrix[u-1, v-1].Weight;
        else
            return 0;
    }

    public IEnumerable<int> AllNodes
    {
        get
        {
            for (int i=1; i<=NodeCount; i++)
                yield return i;
        }
    }

    public void Clear()
    {
        adjacenz_matrix = new Edge[NodeCount, NodeCount];
        EdgeCount = 0;
    }

    public IEnumerable<Edge> AllEdges
    {
        get 
        {
            var hs = new HashSet<Edge>();
            foreach (var node in AllNodes)
                foreach (var edge in GetEdgesFrom(node))
                   hs.Add(edge);

            return hs;
        }
    }
}