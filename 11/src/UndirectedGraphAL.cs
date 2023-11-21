
public class UndirectedGraphAL : IGraph
{
    private Dictionary<int, HashSet<int>> adjacency_list = new();

    public void AddEdge(int u, int v)
    {
        if (!adjacency_list.ContainsKey(u-1))
            adjacency_list[u-1] = new HashSet<int>();
        
        adjacency_list[u-1].Add(v);

        if (!adjacency_list.ContainsKey(v-1))
            adjacency_list[v-1] = new HashSet<int>();

        adjacency_list[v-1].Add(u);
    }

    public void DeleteEdge(int u, int v)
    {
        adjacency_list[u-1].Remove(v);
        adjacency_list[v-1].Remove(u);
    }

    public IEnumerable<int> GetNeighborsOf(int u)
    {
        return adjacency_list[u-1];
    }

    public bool HasEdge(int u, int v)
    {
        return adjacency_list[u-1].Contains(v);
    }
}