using System.Collections.Generic;
using AUD.List;

namespace AUD.Graphs
{
    public class DirectedGraphAL : IDirectedGraph
    {
        private GenericLinkedList<int>[] adjacency_list;
        private Dictionary<DirectedEdge, double> lenghts = new Dictionary<DirectedEdge, double>();

        public DirectedGraphAL(int node_count)
        {
            NodeCount = node_count;
            adjacency_list = new GenericLinkedList<int>[node_count];

            for (int i = 0; i < node_count; i++)
                adjacency_list[i] = new GenericLinkedList<int>();
        }

        public int NodeCount { get; private set; }

        public void AddEdge(DirectedEdge e)
        {
            adjacency_list[e.Start - 1].PushBack(e.End - 1);
            lenghts.Add(e, e.Length);
        }

        public void DeleteEdge(DirectedEdge e)
        {
            adjacency_list[e.Start - 1].RemoveAll(e.End - 1);
            lenghts.Remove(e);
        }

        public IEnumerable<DirectedEdge> EdgesFrom(int node)
        {
            var edges = new List<DirectedEdge>();

            foreach (var n in adjacency_list[node - 1].AsList())
            {
                var edge = new DirectedEdge(node, n + 1);
                edge.Length = GetLength(edge);
                edges.Add(edge);
            }

            return edges;
        }

        public double GetLength(DirectedEdge e)
        {
            return lenghts[e];
        }

        public bool HasEdge(DirectedEdge e)
        {
            return adjacency_list[e.Start - 1].Contains(e.End - 1);
        }
    }
}