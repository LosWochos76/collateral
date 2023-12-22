using System.Collections.Generic;

public class Dijkstra
{
    private const int WHITE = 0;
    private const int GRAY = 1;
    private const int BLACK = 2;

    private IGraph graph;
    private Dictionary<int, int> color = new Dictionary<int, int>();
    private Dictionary<int, double> distance = new Dictionary<int, double>();
    private Dictionary<int, int> parent = new Dictionary<int, int>();
    private List<int> queue;

    public Dijkstra(IGraph graph)
    {
        this.graph = graph;
    }

    public Path FindShortestPath(int start, int dest)
    {
        Init();
        BFS(start);
        return MakePath(dest);
    }

    private void Init()
    {
        queue = new List<int>();
        for (int i=1; i<=graph.NodeCount; i++)
        {
            color[i] = WHITE;
            distance[i] = int.MaxValue;
            parent[i] = -1;
        }
    }

    private void BFS(int start_node)
    {
        distance[start_node] = 0;
        queue = new List<int>() { start_node };

        while (queue.Count > 0)
        {
            var n = Dequeue();
            foreach (var e in graph.GetEdgesFrom(n))
            {
                if (color[e.V] != BLACK)
                {
                    color[e.V] = GRAY;
                    double dist = distance[n] + e.Weight;
                    if (dist < distance[e.V])
                    {
                        distance[e.V] = dist;
                        parent[e.V] = n;
                    }

                    queue.Add(e.V);
                }
            }

            color[n] = BLACK;
        }
    }

    // Get the node from the list with the lowest distance
    // A bad implementation, as this has linear costs
    private int Dequeue()
    {
        double dist = distance[queue[0]];
        int index = 0;

        for (int i=1; i< queue.Count; i++)
        {
            if (distance[queue[i]] < distance[queue[i-1]])
            {
                index = i;
                dist = distance[queue[i]];
            }
        }

        int node = queue[index];
        queue.RemoveAt(index);
        return node;
    }

    // Create the path from the parents
    private Path MakePath(int dest)
    {
        var result = new Path();
        while (parent[dest] != -1)
        {
            var weight = graph.GetWeight(parent[dest], dest);
            result.Insert(new Edge(graph.IsDirected, parent[dest], dest, weight));
            dest = parent[dest];
        }

        return result;
    }
}