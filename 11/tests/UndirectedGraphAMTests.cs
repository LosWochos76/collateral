public class UndirectedGraphAMTests
{
    [Test]
    public void Test_AddEdge_HasEdge()
    {
        var hvn = new UndirectedGraphAM(5);

        Assert.False(hvn.HasEdge(1,2));
        Assert.False(hvn.HasEdge(2,1));
        Assert.False(hvn.HasEdge(1,3));
        Assert.False(hvn.HasEdge(3,1));
        Assert.False(hvn.HasEdge(1,5));

        hvn.AddEdge(1, 2);
        hvn.AddEdge(1, 3);
        hvn.AddEdge(1, 4);

        Assert.True(hvn.HasEdge(1,2));
        Assert.True(hvn.HasEdge(2,1));
        Assert.True(hvn.HasEdge(1,3));
        Assert.True(hvn.HasEdge(3,1));
        Assert.False(hvn.HasEdge(1,5));

        hvn.DeleteEdge(1, 2);
        hvn.DeleteEdge(1, 3);
        hvn.DeleteEdge(1, 4);

        Assert.False(hvn.HasEdge(1,2));
        Assert.False(hvn.HasEdge(2,1));
        Assert.False(hvn.HasEdge(1,3));
        Assert.False(hvn.HasEdge(3,1));
        Assert.False(hvn.HasEdge(1,5));
    }

    [Test]
    public void Test_GetNeighborsOf()
    {
        var hvn = new UndirectedGraphAM(5);
        hvn.AddEdge(1, 2);
        hvn.AddEdge(1, 3);
        hvn.AddEdge(1, 4);

        var n = hvn.GetNeighborsOf(1);
        var nhs = new HashSet<int>(n);
        Assert.True(nhs.Contains(2));
        Assert.True(nhs.Contains(3));
        Assert.True(nhs.Contains(4));
        Assert.False(nhs.Contains(5));
    }

    [Test]
    public void Test_EdgeCount_NodeCount()
    {
        var hvn = new UndirectedGraphAM(4);
        Assert.AreEqual(4, hvn.NodeCount);
        Assert.AreEqual(0, hvn.EdgeCount);

        hvn.AddEdge(1, 2);
        hvn.AddEdge(1, 3);
        hvn.AddEdge(1, 4);

        Assert.AreEqual(4, hvn.NodeCount);
        Assert.AreEqual(3, hvn.EdgeCount);
    }
}