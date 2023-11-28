
public class UndirectedWeightedGraphAL : IWeightedGraph
{
    private int edge_count = 0;
    private Dictionary<int, HashSet<int>> adjacency_list = new();
    private Dictionary<(int, int), double> weights = new();

    private void CreateIfNotExists(int u)
    {
        if (!adjacency_list.ContainsKey(u))
            adjacency_list[u] = new HashSet<int>();
    }

    public void AddEdge(int u, int v, double weight = 1)
    {
        if (HasEdge(u, v))
            return;

        adjacency_list[u].Add(v);
        weights[(u,v)] = weight;
        
        adjacency_list[v].Add(u);
        weights[(v,u)] = weight;

        edge_count++;
    }

    public void DeleteEdge(int u, int v)
    {
        if (!HasEdge(u, v))
            return;

        adjacency_list[u].Remove(v);
        weights.Remove((u,v));

        adjacency_list[v].Remove(u);
        weights.Remove((v,u));

        edge_count--;
    }

    public IEnumerable<int> GetNeighborsOf(int u)
    {
        CreateIfNotExists(u);
        return adjacency_list[u];
    }

    public bool HasEdge(int u, int v)
    {
        CreateIfNotExists(u);
        CreateIfNotExists(v);
        return adjacency_list[u].Contains(v);
    }

    public double GetWeight(int u, int v)
    {
        if (weights.ContainsKey((u,v)))
            return weights[(u,v)];
        else
            return 0;
    }

    public IEnumerable<int> AllNodes
    {
        get { return adjacency_list.Keys; }
    }

    public int NodeCount
    {
        get { return adjacency_list.Keys.Count; }
    }

    public int EdgeCount
    {
        get { return edge_count; }
    }
}