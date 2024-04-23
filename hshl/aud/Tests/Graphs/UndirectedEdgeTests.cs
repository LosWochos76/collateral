using Xunit;

namespace AUD.Graphs.Tests
{
    public class UndirectedEdgeTests
    {
        [Fact]
        public void UndirectedEdge_EqualsTest()
        {
            var e1 = new UndirectedEdge(1, 3);
            var e2 = new UndirectedEdge(3, 1);
            Assert.True(e1.Equals(e2));
        }
    }
}