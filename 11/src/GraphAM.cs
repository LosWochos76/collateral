public class GraphAM : IGraph
{
    private double[,] adjacenz_matrix;
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

        adjacenz_matrix[u-1, v-1] = weight;

        if (IsUndirected)
            adjacenz_matrix[v-1, u-1] = weight;

        EdgeCount++;
    }

    public void DeleteEdge(int u, int v)
    {
        if (!HasEdge(u, v))
            return;

        adjacenz_matrix[u-1, v-1] = 0;

        if (IsUndirected)
            adjacenz_matrix[v-1, u-1] = 0;

        EdgeCount--;
    }

    public bool HasEdge(int u, int v)
    {
        return adjacenz_matrix[u-1, v-1] != 0;
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
                yield return new Edge(IsDirected, u, v, GetWeight(u, v));
    }

    public double GetWeight(int u, int v)
    {
        return adjacenz_matrix[u-1, v-1];
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
        adjacenz_matrix = new double[NodeCount, NodeCount];
        EdgeCount = 0;
    }
}