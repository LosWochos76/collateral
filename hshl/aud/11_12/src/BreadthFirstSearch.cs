using System.Collections.Generic;

public class BreadthFirstSearch
{
    private const int WHITE = 0;
    private const int GRAY = 1;
    private const int BLACK = 2;
    private IGraph graph;
    private int start_node;
    private Dictionary<int, int> color = new Dictionary<int, int>();
    private Dictionary<int, int> distance = new Dictionary<int, int>();
    private Dictionary<int, int> parent = new Dictionary<int, int>();
    private Queue<int> queue;

    public BreadthFirstSearch(IGraph graph)
    {
        this.graph = graph;
    }

    public void Explore(int start_node)
    {
        Init(start_node);
        Visit(start_node);
    }

    private void Init(int start_node)
    {
        this.start_node = start_node;

        for (int i = 1; i <= graph.NodeCount; i++)
        {
            color[i] = WHITE;
            distance[i] = -1;
            parent[i] = -1;
        }

        Visit(start_node);
    }

    private void Visit(int start_node)
    {
        distance[start_node] = 0;
        queue = new Queue<int>(new int[] { start_node });

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            foreach (var e in graph.GetEdgesFrom(u))
            {
                if (color[e.V] == WHITE)
                {
                    color[e.V] = GRAY;
                    distance[e.V] = distance[u] + 1;
                    parent[e.V] = u;
                    queue.Enqueue(e.V);
                }
            }

            color[u] = BLACK;
        }
    }

    public int GetDistance(int node)
    {
        return distance[node];
    }

    public int GetParent(int node)
    {
        return parent[node];
    }

    public bool HasPathFromStartNode(int dest_node)
    {
        return distance[dest_node] != -1;
    }

    public Path MakePath(int dest)
    {
        var path = new Path();
        while (parent[dest] != -1)
        {
            var weight = graph.GetWeight(parent[dest], dest);
            path.Insert(new Edge(graph.IsDirected, parent[dest], dest, weight));
            dest = parent[dest];
        }

        return path;
    }
}