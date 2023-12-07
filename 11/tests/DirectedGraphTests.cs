public class DirectedGraphTests
{
    public static object[] TestCases =
    {
        new object[] { new GraphAM(4, true) },
        new object[] { new GraphAL(4, true) }
    };

    [TestCaseSource(nameof(TestCases))]
    public void Test_AddEdge_HasEdge(IGraph g)
    {
        g.Clear();
        Assert.AreEqual(4, g.NodeCount);
        Assert.AreEqual(0, g.EdgeCount);
        g.AddEdge(1, 2, 1);
        Assert.AreEqual(1, g.EdgeCount);
        Assert.IsTrue(g.HasEdge(1,2));
        Assert.IsFalse(g.HasEdge(2,1));
    }

    [TestCaseSource(nameof(TestCases))]
    public void Test_DeleteEdge(IGraph g)
    {
        g.Clear();
        g.AddEdge(1, 2, 1);
        Assert.True(g.HasEdge(1,2));
        Assert.False(g.HasEdge(2,1));
        g.DeleteEdge(1, 2);
        Assert.False(g.HasEdge(2,1));
        Assert.False(g.HasEdge(2,1));
    }

    [TestCaseSource(nameof(TestCases))]
    public void Test_GetNeighborsOf(IGraph g)
    {
        g.Clear();
        g.AddEdge(1, 2, 1);
        g.AddEdge(1, 3, 1);
        g.AddEdge(1, 4, 1);

        var n = new HashSet<int>(g.GetNeighborsOf(1));
        Assert.True(n.Contains(2));
        Assert.True(n.Contains(3));
        Assert.True(n.Contains(4));
        Assert.False(n.Contains(5));

        n = new HashSet<int>(g.GetNeighborsOf(2));
        Assert.False(n.Contains(1));
        Assert.False(n.Contains(3));
        Assert.False(n.Contains(4));
    }

    [TestCaseSource(nameof(TestCases))]
    public void Test_GetWeight(IGraph g)
    {
        g.Clear();
        g.AddEdge(1,2,5);
        Assert.AreEqual(5d, g.GetWeight(1,2));
        Assert.AreEqual(0d, g.GetWeight(2,1));
    }
}