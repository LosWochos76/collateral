using System;
using AUD.Graphs;

namespace AUD
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = new DirectedGraphAL(6);
            g.AddEdge(new DirectedEdge(1, 2, 7));
            g.AddEdge(new DirectedEdge(1, 3, 9));
            g.AddEdge(new DirectedEdge(1, 6, 14));
            g.AddEdge(new DirectedEdge(2, 3, 10));
            g.AddEdge(new DirectedEdge(2, 4, 15));
            g.AddEdge(new DirectedEdge(3, 6, 2));
            g.AddEdge(new DirectedEdge(3, 4, 11));
            g.AddEdge(new DirectedEdge(6, 5, 9));
            g.AddEdge(new DirectedEdge(4, 5, 6));

            var shortest = new Dijkstra(g);
            var path = shortest.FindShortestPath(1, 5);

            Console.ReadLine();
        }
    }
}
