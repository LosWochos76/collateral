using AUD.List;
using Xunit;

namespace AUD.Tests
{
    public class LinkedListTests
    {
        [Fact]
        public void LinkedList_InsertSorted()
        {
            var list = new LinkedList();
            list.InsertSorted(5);
            list.InsertSorted(2);
            list.InsertSorted(1);
            list.InsertSorted(7);
            list.InsertSorted(0);
            Assert.True(list.IsSorted);
        }
    }
}