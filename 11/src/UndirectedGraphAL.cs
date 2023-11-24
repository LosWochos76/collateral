
public class UndirectedGraphAL : IGraph
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
        if (HasEdge(u, v))
            return;

        adjacency_list[u].Add(v);
        adjacency_list[v].Add(u);
        edge_count++;
    }

    public void DeleteEdge(int u, int v)
    {
        if (!HasEdge(u, v))
            return;

        adjacency_list[u].Remove(v);
        adjacency_list[v].Remove(u);
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