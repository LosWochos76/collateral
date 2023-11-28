public class DepthFirstSearch
{
    private const int WHITE = 0;
    private const int GRAY = 1;
    private const int BLACK = 2;

    private IGraph graph;
    private Dictionary<int, int> color = new Dictionary<int, int>();
    private Dictionary<int, int> parent = new Dictionary<int, int>();
    private Dictionary<int, int> component = new Dictionary<int, int>();
    private bool has_cycle = false;
    private int components = 0;

    public DepthFirstSearch(IGraph graph)
    {
        this.graph = graph;
    }

    public void Explore()
    {
        // initialize
        for (int i = 1; i <= graph.NodeCount; i++)
        {
            color[i] = WHITE;
            parent[i] = -1;
            component[i] = 0;
        }

        // Depth-first-search
        for (int i = 1; i <= graph.NodeCount; i++)
        {
            if (component[i] == 0)
                components++;

            if (color[i] == WHITE)
                Visit(i);
        }
    }

    private void Visit(int u)
    {
        color[u] = GRAY;
        component[u] = components;

        foreach (var node in graph.GetNeighborsOf(u))
        {
            if (color[node] == WHITE)
            {
                parent[node] = u;
                Visit(node);
            }
            else if (color[node] == GRAY)
            {
                has_cycle = true;
            }
        }

        color[u] = BLACK;
    }

    public bool HasCycle
    {
        get { return has_cycle; }
    }

    public int GetParentOf(int node)
    {
        return parent[node];
    }

    public int Components
    {
        get { return components; }
    }

    public int GetComponentOf(int node)
    {
        return component[node];
    }
}