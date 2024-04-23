public class MergerSorterTests
{
    [Test]
    public void Test_Array()
    {
        var numbers = ArrayHelper.RandomNumbers(1000);
        numbers = MergesortSorter.Sort(numbers);
        Assert.IsTrue(ArrayHelper.IsSorted(numbers));
    }
}