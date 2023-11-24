public class DirectedGraphAL : IGraph
{
    private int edge_count = 0;
    private Dictionary<int, HashSet<int>> adjacency_list = new();

    private void CreateIfNotExists(int u)
    {
        if (!adjacency_list.ContainsKey(u))
            adjacency_list[u] = new HashSet<int>();
    }

    public void AddEdge(int u, int v)
    {
        CreateIfNotExists(u);
        CreateIfNotExists(v);
        edge_count++;
        adjacency_list[u].Add(v);
    }

    public void DeleteEdge(int u, int v)
    {
        CreateIfNotExists(u);
        edge_count--;
        adjacency_list[u].Remove(v);
    }

    public IEnumerable<int> GetNeighborsOf(int u)
    {
        CreateIfNotExists(u);
        return adjacency_list[u];
    }

    public bool HasEdge(int u, int v)
    {
        CreateIfNotExists(u);
        return adjacency_list[u].Contains(v);
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