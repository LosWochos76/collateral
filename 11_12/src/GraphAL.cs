
using System.Text;

public class GraphAL : IGraph
{
    private Dictionary<int, HashSet<Edge>> adjacency_list;
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

    public void AddEdge(int u, int v, double weight = 1)
    {
        if (HasEdge(u, v))
            return;

        adjacency_list[u].Add(new Edge(IsDirected, u, v, weight));

        if (IsUndirected)
            adjacency_list[v].Add(new Edge(IsDirected, v, u, weight));

        EdgeCount++;
    }

    public void DeleteEdge(int u, int v)
    {
        if (!HasEdge(u, v))
            return;

        adjacency_list[u].Remove(new Edge(IsDirected, v, u, 1));

        if (IsUndirected)
            adjacency_list[v].Remove(new Edge(IsDirected, v, u, 1));

        EdgeCount--;
    }

    public IEnumerable<int> GetNeighborsOf(int u)
    {
        foreach (var edge in adjacency_list[u])
            yield return edge.V;
    }

    public IEnumerable<Edge> GetEdgesFrom(int u)
    {
        return adjacency_list[u];
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

    private Edge GetEdge(int u, int v)
    {
        var search_edge = new Edge(IsDirected, u, v);
        Edge find_edge = null;
        adjacency_list[u].TryGetValue(search_edge, out find_edge);
        return find_edge;
    }

    public bool HasEdge(int u, int v)
    {
        return GetEdge(u, v) != null;
    }

    public double GetWeight(int u, int v)
    {
        var edge = GetEdge(u, v);
        return edge is null ? 0 : edge.Weight;
    }

    public void Clear()
    {
        adjacency_list = new Dictionary<int, HashSet<Edge>>();
        for (int i=1; i<=NodeCount; i++)
            adjacency_list[i] = new HashSet<Edge>();

        EdgeCount = 0;
    }

    public IEnumerable<int> AllNodes
    {
        get { return adjacency_list.Keys; }
    }
}