public class DepthFirstSearch
{
    private const int WHITE = 0;
    private const int GRAY = 1;
    private const int BLACK = 2;

    private IGraph graph;
    private Dictionary<int, int> color = new Dictionary<int, int>();
    private Dictionary<int, int> parent = new Dictionary<int, int>();
    private Dictionary<int, int> component = new Dictionary<int, int>();
    private IGraph forrest;
    private bool has_cycle = false;
    private int components = 0;

    public DepthFirstSearch(IGraph graph)
    {
        this.graph = graph;
        forrest = new GraphAL(graph.NodeCount, graph.IsDirected);

        for (int i = 1; i <= graph.NodeCount; i++)
        {
            color[i] = WHITE;
            parent[i] = -1;
            component[i] = 0;
        }
    }

    public IGraph ExploreRecursive()
    {
        for (int i = 1; i <= graph.NodeCount; i++)
        {
            if (component[i] == 0)
                components++;

            if (color[i] == WHITE)
                VisitRekursive(i);
        }
        
        return forrest;
    }

    private void VisitRekursive(int start)
    {
        color[start] = GRAY;
        component[start] = components;

        foreach (var edge in graph.GetEdgesFrom(start))
        {
            if (color[edge.V] == WHITE)
            {
                parent[edge.V] = start;
                forrest.AddEdge(edge.U, edge.V, edge.Weight);
                VisitRekursive(edge.V);
            }
            else if (color[edge.V] == GRAY)
            {
                has_cycle = true;
            }
        }

        color[start] = BLACK;
    }

    public IGraph ExploreLinear()
    {
        for (int i = 1; i <= graph.NodeCount; i++)
        {
            if (color[i] == WHITE)
            {
                VisitLinear(i);
            }
        }

        return forrest;
    }

    public void VisitLinear(int start)
    {
        var stack = new Stack<int>();
        stack.Push(start);
        int last_node = 0;

        while (stack.Count > 0)
        {
            int u = stack.Pop();
            if (color[u] == WHITE)
            {
                if (last_node != 0)
                    forrest.AddEdge(last_node, u);
                
                last_node = u;
                foreach (var e in graph.GetEdgesFrom(u).Reverse())
                    stack.Push(e.V);
            }

            color[u] = GRAY;
        }
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