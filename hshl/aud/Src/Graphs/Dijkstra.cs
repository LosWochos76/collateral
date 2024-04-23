using System.Collections.Generic;

namespace AUD.Graphs
{
    public class Dijkstra
    {
        private IDirectedGraph g;
        private Dictionary<int, double> distance = new Dictionary<int, double>();
        private Dictionary<int, int> parent = new Dictionary<int, int>();
        private List<int> queue;

        public Dijkstra(IDirectedGraph g)
        {
            this.g = g;
        }

        public Path FindShortestPath(int start, int dest)
        {
            Init(start);
            BFS(start, dest);
            return MakePath(dest);
        }

        private void Init(int start)
        {
            queue = new List<int>();

            for (int i=1; i<=g.NodeCount; i++)
            {
                queue.Add(i);
                distance[i] = int.MaxValue;
                parent[i] = -1;
            }

            distance[start] = 0;
        }

        private void BFS(int start, int dest)
        {
            while (queue.Count > 0)
            {
                var n = Dequeue();
                foreach (var e in g.EdgesFrom(n))
                {
                    double dist = distance[n] + e.Length;
                    if (dist < distance[e.End])
                    {
                        distance[e.End] = dist;
                        parent[e.End] = n;
                    }
                }
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
                var e = new DirectedEdge(parent[dest], dest);
                e.Length = g.GetLength(e);
                result.PushFront(e);
                dest = parent[dest];
            }

            return result;
        }
    }
}