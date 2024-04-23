public class ArrayHelperTests
{
    [Test]
    public void Test_Swap()
    {
        var array = new int[]{1,2};
        ArrayHelper.Swap(array, 0, 1);
        Assert.AreEqual(2, array[0]);
        Assert.AreEqual(1, array[1]);
    }

    [Test]
    public void Test_IsSorted()
    {
        Assert.IsTrue(ArrayHelper.IsSorted(new int[0]));
        Assert.IsTrue(ArrayHelper.IsSorted(new int[]{1}));
        Assert.IsTrue(ArrayHelper.IsSorted(new int[]{1,2,3}));
        Assert.IsFalse(ArrayHelper.IsSorted(new int[]{1,3,2}));
    }
}