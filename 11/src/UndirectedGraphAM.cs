public class UndirectedGraphAM : IGraph
{
    private int node_count = 0;
    private int edge_count = 0;
    private int[,] adjacenz_matrix;

    public UndirectedGraphAM(int node_count)
    {
        this.node_count = node_count;
        adjacenz_matrix = new int[node_count, node_count];
    }

    public void AddEdge(int u, int v)
    {
        if (HasEdge(u,v))
            return;
            
        adjacenz_matrix[u-1, v-1] = 1;
        adjacenz_matrix[v-1, u-1] = 1;
        edge_count++;
    }

    public void DeleteEdge(int u, int v)
    {
        if (!HasEdge(u, v))
            return;

        adjacenz_matrix[u-1, v-1] = 0;
        adjacenz_matrix[v-1, u-1] = 0;
        edge_count--;
    }

    public bool HasEdge(int u, int v)
    {
        return adjacenz_matrix[u-1, v-1] != 0;
    }

    public IEnumerable<int> GetNeighborsOf(int u)
    {
        var neighbors = new List<int>();
        for (int v=1; v<=node_count; v++)
            if (HasEdge(u, v))
                neighbors.Add(v);

        return neighbors;
    }

    public IEnumerable<int> AllNodes
    {
        get
        {
            for (int i=1; i<=node_count; i++)
                yield return i;
        }
    }

    public int NodeCount
    {
        get { return node_count; }
    }

    public int EdgeCount
    {
        get { return edge_count; }
    }
}