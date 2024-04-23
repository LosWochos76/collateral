using System.Collections.Generic;

namespace AUD.Graphs
{
    public class DepthFirstSearch
    {
        private const int WHITE = 0;
        private const int GRAY = 1;
        private const int BLACK = 2;
        private IDirectedGraph g;
        private Dictionary<int, int> color = new Dictionary<int, int>();
        private Dictionary<int, int> parent = new Dictionary<int, int>();
        private bool has_cycle = false;
        private Dictionary<int, int> component = new Dictionary<int, int>();
        private int components = 0;

        public DepthFirstSearch(IDirectedGraph g)
        {
            this.g = g;
        }

        public void Explore()
        {
            // initialize
            for (int i = 1; i <= g.NodeCount; i++)
            {
                color[i] = WHITE;
                parent[i] = -1;
                component[i] = 0;
            }

            // Depth-first-search
            for (int i = 1; i <= g.NodeCount; i++)
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

            foreach (var e in g.EdgesFrom(u))
            {
                if (color[e.End] == WHITE)
                {
                    parent[e.End] = u;
                    Visit(e.End);
                }
                else if (color[e.End] == GRAY)
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

        public int getParentOf(int node)
        {
            return parent[node];
        }

        public int Components
        {
            get { return components; }
        }

        public int getComponentOf(int node)
        {
            return component[node];
        }
    }
}