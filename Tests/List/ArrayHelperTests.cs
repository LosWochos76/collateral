using AUD.List;
using Xunit;

namespace AUD.Tests
{
    public class ArrayListTests
    {
        [Fact]
        public void ArrayList_Swap()
        {
            int[] data = new int[] { 1, 2 };
            var list = new ArrayList(data);
            list.Swap(0, 1);
            Assert.Equal<int>(2, data[0]);
            Assert.Equal<int>(1, data[1]);
        }
    }
}