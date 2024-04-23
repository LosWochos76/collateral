using System;
using System.Collections.Generic;

namespace AUD.Graphs
{
    public class Path
    {
        private List<IEdge> edges = new List<IEdge>();

        public int Count { get { return edges.Count; } }

        public void PushBack(IEdge e)
        {
            edges.Add(e);
        }

        public void PushFront(IEdge e)
        {
            edges.Insert(0, e);
        }

        public void RemoveLast()
        {
            if (edges.Count > 0)
                edges.RemoveAt(edges.Count - 1);
        }

        public bool Contains(IEdge e)
        {
            return edges.Contains(e);
        }

        public override string ToString()
        {
            return string.Join("-", Array.ConvertAll(edges.ToArray(), x => x.ToString()));
        }

        public double Length
        {
            get
            {
                double length = 0;

                foreach (var e in edges)
                {
                    var de = e as DirectedEdge;
                    length += de != null ? de.Length : 1;
                }

                return length;
            }
        }

        public Path Clone()
        {
            var p = new Path();
            foreach (var e in edges)
                p.PushBack(e);

            return p;
        }
    }
}