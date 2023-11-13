public class QuicksortSorterTests
{
    [Test]
    public void Test_Array()
    {
        var numbers = ArrayHelper.RandomNumbers(1000);
        QuicksortSorter.Sort(numbers);
        Assert.IsTrue(ArrayHelper.IsSorted(numbers));
    }
}