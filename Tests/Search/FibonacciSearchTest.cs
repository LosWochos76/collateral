using AUD.Search;
using Xunit;

namespace AUD.Tests
{
    public class FibonacciSearchTest
    {
        [Fact]
        public void FibonacciSearch_Contains_in_one()
        {
            var data = new int[] { 1 };
            var search = new FibonacciSearch(data, 20);
            Assert.True(search.Contains(1));
        }
    }
}
