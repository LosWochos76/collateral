using System.Collections.Generic;

namespace AUD.Graphs
{
    public class DirectedGraphAM : IDirectedGraph
    {
        private double[,] adjacenz_matrix;

        public int NodeCount { get; private set; }

        public DirectedGraphAM(int node_count)
        {
            NodeCount = node_count;
            adjacenz_matrix = new double[node_count, node_count];
        }

        public void AddEdge(DirectedEdge e)
        {
            adjacenz_matrix[e.Start - 1, e.End - 1] = e.Length;
        }

        public bool HasEdge(DirectedEdge e)
        {
            return adjacenz_matrix[e.Start - 1, e.End - 1] != 0;
        }

        public void DeleteEdge(DirectedEdge e)
        {
            adjacenz_matrix[e.Start - 1, e.End - 1] = 0;
        }

        public double GetLength(DirectedEdge e)
        {
            return adjacenz_matrix[e.Start - 1, e.End - 1];
        }

        public IEnumerable<DirectedEdge> EdgesFrom(int node)
        {
            var edges = new List<DirectedEdge>();

            for (int i = 1; i <= NodeCount; i++)
            {
                double length = adjacenz_matrix[node - 1, i - 1];
                if (length != 0)
                    edges.Add(new DirectedEdge(node, i, length));
            }

            return edges;
        }
    }
}
