public class AvlTreeTests
{
    [Test]
    public void Test_Min_Max()
    {
        var tree = new AvlTree<int>();
        tree.Insert(17);
        tree.Insert(9);
        tree.Insert(23);
        tree.Insert(7);
        tree.Insert(8);
        Assert.AreEqual(7, tree.GetMinimumValue());
        Assert.AreEqual(23, tree.GetMaximumValue());
    }

    [Test]
    public void Test_Insert_Delete_Contains()
    {
        var tree = new AvlTree<int>();
        Assert.IsFalse(tree.Contains(17));
        tree.Insert(17);
        Assert.IsTrue(tree.Contains(17));
        tree.Delete(17);
        Assert.IsFalse(tree.Contains(17));
    }

    [Test]
    public void Test_Rebalance()
    {
        var tree = new AvlTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(4);
        tree.Insert(5);
        tree.Insert(6);
        Assert.AreEqual(3, tree.Height);
    }
}