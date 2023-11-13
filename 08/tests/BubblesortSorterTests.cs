public class BubblesortSorterTests
{
    [Test]
    public void Test_Array()
    {
        var numbers = ArrayHelper.RandomNumbers(1000);
        BubblesortSorter.Sort(numbers);
        Assert.IsTrue(ArrayHelper.IsSorted(numbers));
    }
}