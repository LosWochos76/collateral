public class MaxHeapTests
{
    [Test]
    public void Test_HasElements()
    {
        var heap = new MaxHeap<int>();
        Assert.IsFalse(heap.HasElements);

        heap.Insert(42);
        Assert.IsTrue(heap.HasElements);
    }

    [Test]
    public void Test_ExtractMax()
    {
        var heap = new MaxHeap<int>(new int[]{8,3,1,9,42,8,0,13});
        var max = heap.ExtractMax();
        Assert.AreEqual(42, max);
    }
}