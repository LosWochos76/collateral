public class LinkedListTests
{
    [Test]
    public void Test_HasElemets()
    {
        var ll = new LinkedList<int>();
        Assert.IsFalse(ll.HasElements);
        ll.Push(42);
        Assert.IsTrue(ll.HasElements);
    }

    public void Test_Push_Pop()
    {
        var ll = new LinkedList<int>();
        ll.Push(42);
        ll.Push(12);
        ll.Push(3);
        Assert.AreEqual(3, ll.Pop());
        Assert.AreEqual(12, ll.Pop());
        Assert.AreEqual(42, ll.Pop());
        Assert.Throws<Exception>(() => { ll.Pop(); });
    }

    public void Test_Enqueue_Deque()
    {
        var ll = new LinkedList<int>();
        ll.Enqueue(42);
        ll.Enqueue(12);
        ll.Enqueue(3);
        Assert.AreEqual(42, ll.Dequeue());
        Assert.AreEqual(12, ll.Pop());
        Assert.AreEqual(3, ll.Pop());
        Assert.Throws<Exception>(() => { ll.Pop(); });
    }
}