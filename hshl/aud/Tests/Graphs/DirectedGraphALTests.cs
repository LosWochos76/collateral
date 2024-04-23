using System.Collections.Generic;
using Xunit;

namespace AUD.Graphs.Tests
{
    public class DirectedGraphALTests
    {
        [Fact]
        public void DirectedGraphAL_HasEdgeTest()
        {
            var graph = new DirectedGraphAL(3);
            graph.AddEdge(new DirectedEdge(1, 2, 0.5));
            graph.AddEdge(new DirectedEdge(2, 3, 0.5));

            Assert.True(graph.HasEdge(new DirectedEdge(1, 2)));
            Assert.True(graph.HasEdge(new DirectedEdge(2, 3)));
        }

        [Fact]
        public void DirectedGraphAL_EdgesFromTest()
        {
            var graph = new DirectedGraphAL(3);
            graph.AddEdge(new DirectedEdge(1, 2, 0.5));
            graph.AddEdge(new DirectedEdge(1, 3, 0.5));

            var edges = graph.EdgesFrom(1);
            var list = new List<DirectedEdge>(edges);

            Assert.Equal(2, list.Count);
            Assert.Equal(1, list[0].Start);
            Assert.Equal(2, list[0].End);
            Assert.Equal(1, list[1].Start);
            Assert.Equal(3, list[1].End);
        }
    }
}