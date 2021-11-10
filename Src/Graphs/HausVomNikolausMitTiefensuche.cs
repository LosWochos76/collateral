using System;
using System.Collections.Generic;

namespace AUD.Graphs
{
    public class HausVomNikolausMitTiefensuche
    {
        private UndirectedGraphAM graph;
        private List<Path> solutions = new List<Path>();

        public HausVomNikolausMitTiefensuche()
        {
            graph = new UndirectedGraphAM(5);
            graph.AddEdge(new UndirectedEdge(1, 2));
            graph.AddEdge(new UndirectedEdge(1, 3));
            graph.AddEdge(new UndirectedEdge(1, 4));
            graph.AddEdge(new UndirectedEdge(2, 3));
            graph.AddEdge(new UndirectedEdge(2, 4));
            graph.AddEdge(new UndirectedEdge(3, 4));
            graph.AddEdge(new UndirectedEdge(3, 5));
            graph.AddEdge(new UndirectedEdge(4, 5));
        }

        public List<Path> FindSolutions()
        {
            for (int i = 1; i <= graph.NodeCount; i++)
                Visit(i, new  Path());

            return solutions;
        }

        private void Visit(int node, Path p)
        {
            //WriteLine(p);

            foreach (var e in graph.EdgesFrom(node))
            {
                if (!p.Contains(e))
                {
                    p.PushBack(e);

                    if (p.Count < 8)
                        Visit(e.End, p);
                    else
                        FoundPath(p);

                    p.RemoveLast();
                }
            }
        }

        private void FoundPath(Path p)
        {
            solutions.Add(p.Clone());
        }
    }
}