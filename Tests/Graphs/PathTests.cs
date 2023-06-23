using Xunit;

namespace AUD.Graphs.Tests
{
    public class PathTests
    {
        [Fact]
        public void Path_Length()
        {
            var p = new Path();
            p.PushBack(new DirectedEdge(1, 2, 0.5));
            p.PushBack(new DirectedEdge(2, 3, 0.5));

            Assert.Equal(1, p.Length);
        }
    }
}