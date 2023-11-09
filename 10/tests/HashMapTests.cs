public class HashMapTests
{
    [Test]
    public void Test_ContainsKey()
    {
        var hm = new HashMap<char, int>();

        Assert.AreEqual(0, hm.Count);
        Assert.IsFalse(hm.ContainsKey('a'));

        hm.Insert('a', 100);
        Assert.AreEqual(1, hm.Count);
        Assert.IsTrue(hm.ContainsKey('a'));
    }

    [Test]
    public void Test_Insert()
    {
        var hm = new HashMap<char, int>();
        hm.Insert('a', 100);
        hm.Insert('b', 200);
        hm.Insert('c', 300);

        Assert.AreEqual(3, hm.Count);
        Assert.AreEqual(100, hm.GetValue('a'));
        Assert.AreEqual(200, hm.GetValue('b'));
        Assert.AreEqual(300, hm.GetValue('c'));
        Assert.Throws<Exception>(() => hm.GetValue('d'));
    }

    [Test]
    public void Test_Remove()
    {
        var hm = new HashMap<char, int>();

        hm.Insert('a', 100);
        Assert.IsTrue(hm.ContainsKey('a'));

        hm.Remove('a');
        Assert.IsFalse(hm.ContainsKey('a'));
        Assert.AreEqual(0, hm.Count);
    }

    [Test]
    public void Test_Reorganize()
    {
        var hm = new HashMap<char, int>(2);
        Assert.AreEqual(2, hm.Size);
        hm.Insert('a', 100);
        Assert.AreEqual(1, hm.Count);

        hm.Insert('b', 200);
        Assert.AreEqual(4, hm.Size);
        Assert.IsTrue(hm.ContainsKey('a'));
        Assert.IsTrue(hm.ContainsKey('b'));
    }
}