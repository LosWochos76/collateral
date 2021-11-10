using System.Collections.Generic;

namespace AUD.Graphs
{
    public interface IDirectedGraph
    {
        int NodeCount { get; }
        void AddEdge(DirectedEdge e);
        bool HasEdge(DirectedEdge e);
        void DeleteEdge(DirectedEdge e);
        double GetLength(DirectedEdge e);
        IEnumerable<DirectedEdge> EdgesFrom(int node);
    }
}
