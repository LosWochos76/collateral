public class EdgeTest
{
    [Test]
    public void Test_Directed_Equals()
    {
        var e1 = new Edge(true, 1, 2, 5);
        var e2 = new Edge(true, 1, 2, 5);
        var e3 = new Edge(true, 2, 1, 5);
        Assert.True(e1.Equals(e2));
        Assert.False(e1.Equals(e3));
    }

    [Test]
    public void Test_Undirected_Equals()
    {
        var e1 = new Edge(false, 1, 2, 5);
        var e2 = new Edge(false, 1, 2, 5);
        var e3 = new Edge(false, 2, 1, 5);
        Assert.True(e1.Equals(e2));
        Assert.True(e1.Equals(e3));
    }

    [Test]
    public void Test_CompareTo()
    {
        var e1 = new Edge(false, 1, 2, 5);
        var e2 = new Edge(false, 3, 4, 7);
        var e3 = new Edge(false, 9, 10, 7);

        Assert.AreEqual(-1, e1.CompareTo(e2));
        Assert.AreEqual(1, e2.CompareTo(e1));
        Assert.AreEqual(0, e2.CompareTo(e3));
    }
}