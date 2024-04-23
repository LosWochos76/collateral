public class SelectionSorterTests
{
    [Test]
    public void Test_Array()
    {
        var numbers = ArrayHelper.RandomNumbers(1000);
        SelectionsortSorter.Sort(numbers);
        Assert.IsTrue(ArrayHelper.IsSorted(numbers));
    }

    [Test]
    public void Test_LinkedList()
    {
        var numbers = LinkedListHelper.RandomNumbers(1000);
        SelectionsortSorter.Sort(numbers);
        Assert.IsTrue(LinkedListHelper.IsSorted(numbers));
    }
}