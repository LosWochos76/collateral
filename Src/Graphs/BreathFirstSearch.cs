using System.Collections.Generic;

namespace AUD.Graphs
{
    public class BreathFirstSearch
    {
        private const int WHITE = 0;
        private const int GRAY = 1;
        private const int BLACK = 2;
        private IDirectedGraph g;
        private int start_node;
        private Dictionary<int, int> color = new Dictionary<int, int>();
        private Dictionary<int, int> distance = new Dictionary<int, int>();
        private Dictionary<int, int> parent = new Dictionary<int, int>();
        private Queue<int> queue;

        public BreathFirstSearch(IDirectedGraph g)
        {
            this.g = g;
        }

        public void Explore(int start_node)
        {
            Init(start_node);
            BFS(start_node);
        }

        private void Init(int start_node)
        {
            this.start_node = start_node;

            for (int i = 1; i <= g.NodeCount; i++)
            {
                color[i] = WHITE;
                distance[i] = -1;
                parent[i] = -1;
            }

            distance[start_node] = 0;
            queue = new Queue<int>();
            queue.Enqueue(start_node);
        }

        private void BFS(int start_node)
        {
            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (var e in g.EdgesFrom(u))
                {
                    if (color[e.End] == WHITE)
                    {
                        color[e.End] = GRAY;
                        distance[e.End] = distance[u] + 1;
                        parent[e.End] = u;
                        queue.Enqueue(e.End);
                    }
                }

                color[u] = BLACK;
            }
        }

        public int getDistance(int node)
        {
            return distance[node];
        }

        public int getParent(int node)
        {
            return parent[node];
        }

        public bool hasPathFromStartNode(int dest_node)
        {
            return distance[dest_node] != 1;
        }

        public List<int> getPathFromStartNodeTo(int dest_node)
        {
            var path = new List<int>();
            if (!hasPathFromStartNode(dest_node))
                return path;

            while (dest_node != start_node)
            {
                path.Add(dest_node);
                dest_node = getParent(dest_node);
            }

            path.Add(start_node);
            path.Reverse();
            return path;
        }
    }
}