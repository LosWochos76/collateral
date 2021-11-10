using System.Collections.Generic;

namespace AUD.Graphs
{
    public class UndirectedGraphAM
    {
        private int[,] adjacenz_matrix;

        public int NodeCount { get; private set; }

        public UndirectedGraphAM(int node_count)
        {
            NodeCount = node_count;
            adjacenz_matrix = new int[node_count, node_count];
        }

        public void AddEdge(UndirectedEdge e)
        {
            adjacenz_matrix[e.Start - 1, e.End - 1] = 1;
            adjacenz_matrix[e.End - 1, e.Start - 1] = 1;
        }

        public bool HasEdge(UndirectedEdge e)
        {
            return adjacenz_matrix[e.Start - 1, e.End - 1] == 1;
        }

        public void DeleteEdge(UndirectedEdge e)
        {
            adjacenz_matrix[e.Start - 1, e.End - 1] = 0;
            adjacenz_matrix[e.End - 1, e.Start - 1] = 0;
        }

        public IEnumerable<UndirectedEdge> EdgesFrom(int node)
        {
            var edges = new List<UndirectedEdge>();

            for (int i = 1; i <= NodeCount; i++)
            {
                var e = new UndirectedEdge(node, i);
                if (HasEdge(e))
                    edges.Add(e);
            }

            return edges;
        }
    }
}