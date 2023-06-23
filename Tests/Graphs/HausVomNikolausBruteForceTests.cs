using Xunit;

namespace AUD.Graphs.Tests
{
    public class HausVomNikolausBruteForceTests
    {
        [Fact]
        public void HausVomNikolausBruteForce_FindSolutions()
        {
            var hvn = new HausVomNikolausBruteForce();
            var solutions = hvn.FindSolutions();

            Assert.Equal(88, solutions.Count);
        }
    }
}