using AUD.Sort;
using Xunit;

namespace AUD.Tests
{
    public class QuickSortTest
    {
        [Fact]
        public void QuickSort_random_data()
        {
            var list = new QuickSort(1000);

            list.FillWithRandomNumbers(1000);
            Assert.False(list.IsSorted());

            list.Sort();
            Assert.True(list.IsSorted());
        }
    }
}