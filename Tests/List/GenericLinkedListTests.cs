using AUD.List;
using Xunit;

namespace AUD.Tests
{
    public class GenericLinkedListTests
    {
        [Fact]
        public void GenericLinkedList_removeAllTest_remove_at_head()
        {
            var list = new GenericLinkedList<int>();
            list.PushBack(1);
            list.RemoveAll(1);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void GenericLinkedList_removeAllTest_remove_in_midle()
        {
            var list = new GenericLinkedList<int>();
            list.PushBack(1);
            list.PushBack(2);
            list.PushBack(3);
            list.RemoveAll(2);
            Assert.Equal(2, list.Count);
            Assert.Equal(1, list.PopFront());
            Assert.Equal(3, list.PopFront());
        }

        [Fact]
        public void GenericLinkedList_removeAllTest_remove_at_end()
        {
            var list = new GenericLinkedList<int>();
            list.PushBack(1);
            list.PushBack(2);
            list.RemoveAll(2);
            Assert.Equal(1, list.Count);
            Assert.Equal(1, list.PopFront());
        }

        [Fact]
        public void GenericLinkedList_IEnumerable()
        {
            var list = new GenericLinkedList<int>();
            list.PushBack(1);
            list.PushBack(2);
            list.PushBack(3);
            list.PushBack(4);

            var e = list.GetEnumerator();
            Assert.True(e.MoveNext());
            Assert.Equal(1, e.Current);
            Assert.True(e.MoveNext());
            Assert.Equal(2, e.Current);
            Assert.True(e.MoveNext());
            Assert.Equal(3, e.Current);
            Assert.True(e.MoveNext());
            Assert.Equal(4, e.Current);
            Assert.False(e.MoveNext());
        }
    }
}