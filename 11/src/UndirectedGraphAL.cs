
public class UndirectedGraphAL : IGraph
{
    private Dictionary<int, HashSet<int>> adjacency_list = new();

    private void CreateIfNotExists(int u)
    {
        if (!adjacency_list.ContainsKey(u))
            adjacency_list[u] = new HashSet<int>();
    }

    public void AddEdge(int u, int v)
    {
        CreateIfNotExists(u);
        adjacency_list[u].Add(v);

        CreateIfNotExists(v);
        adjacency_list[v].Add(u);
    }

    public void DeleteEdge(int u, int v)
    {
        CreateIfNotExists(u);
        adjacency_list[u].Remove(v);

        CreateIfNotExists(v);
        adjacency_list[v].Remove(u);
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
}