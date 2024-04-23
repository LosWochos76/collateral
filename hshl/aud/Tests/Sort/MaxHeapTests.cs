using AUD.List;
using Xunit;

namespace AUD.Tests
{
    public class MaxHeapTests
    {
        [Fact]
        public void MaxHeap_Sort()
        {
            var array = new ArrayList(1000);
            array.FillWithRandomNumbers(1000);
            Assert.False(array.IsSorted());

            var max_heap = new MaxHeap(array.Data);
            max_heap.Sort();

            Assert.True(max_heap.IsSorted());
        }
    }
}