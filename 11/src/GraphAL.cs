
public class GraphAL : IGraph
{
    private Dictionary<int, HashSet<int>> adjacency_list;
    private Dictionary<(int, int), double> weights;
    public int EdgeCount { get; private set; }
    public int NodeCount { get; private set; }
    public bool IsDirected { get; private set; }
    public bool IsUndirected { get { return !IsDirected; } }

    public GraphAL(int node_count, bool is_directed)
    {
        NodeCount = node_count;
        IsDirected = is_directed;
        Clear();
    }

    public void AddEdge(int u, int v, double weight)
    {
        if (HasEdge(u, v))
            return;

        adjacency_list[u].Add(v);
        weights[(u,v)] = weight;

        if (IsUndirected)
        {
            adjacency_list[v].Add(u);
            weights[(v,u)] = weight;
        }

        EdgeCount++;
    }

    public void DeleteEdge(int u, int v)
    {
        if (!HasEdge(u, v))
            return;

        adjacency_list[u].Remove(v);
        weights.Remove((u,v));

        if (IsUndirected)
        {
            adjacency_list[v].Remove(u);
            weights.Remove((v,u));
        }

        EdgeCount--;
    }

    public IEnumerable<int> GetNeighborsOf(int u)
    {
        return adjacency_list[u];
    }

    public IEnumerable<Edge> GetEdgesFrom(int u)
    {
        foreach (var v in adjacency_list[u])
            yield return new Edge(IsDirected, u, v, GetWeight(u,v));
    }

    public bool HasEdge(int u, int v)
    {
        return adjacency_list[u].Contains(v);
    }

    public double GetWeight(int u, int v)
    {
        if (!weights.ContainsKey((u,v)))
            return 0;
        else
            return weights[(u,v)];
    }

    public void Clear()
    {
        adjacency_list = new Dictionary<int, HashSet<int>>();
        for (int i=1; i<=NodeCount; i++)
            adjacency_list[i] = new HashSet<int>();

        weights = new Dictionary<(int, int), double>();
        EdgeCount = 0;
    }

    public IEnumerable<int> AllNodes
    {
        get { return adjacency_list.Keys; }
    }
}