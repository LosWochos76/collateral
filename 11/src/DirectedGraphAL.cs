public class DirectedGraphAL : IGraph
{
    private Dictionary<int, HashSet<int>> adjacency_list = new();

    public void AddEdge(int u, int v)
    {
        if (adjacency_list[u] == null)
            adjacency_list[u] = new HashSet<int>();
        
        adjacency_list[u].Add(v);
    }

    public void DeleteEdge(int u, int v)
    {
        adjacency_list[u].Remove(v);
    }

    public IEnumerable<int> GetNeighborsOf(int u)
    {
        return adjacency_list[u];
    }

    public bool HasEdge(int u, int v)
    {
        return adjacency_list[u].Contains(v);
    }
}