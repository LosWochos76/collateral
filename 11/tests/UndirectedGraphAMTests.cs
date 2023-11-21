public class UndirectedGraphAMTests
{
    [Test]
    public void Test()
    {
        var hvn = new UndirectedGraphAM(5);
        hvn.AddEdge(1, 2);
        hvn.AddEdge(1, 3);
        hvn.AddEdge(1, 4);
        hvn.AddEdge(2, 4);
        hvn.AddEdge(2, 3);
        hvn.AddEdge(4, 3);
        hvn.AddEdge(4, 5);
        hvn.AddEdge(3, 5);

        Assert.True(hvn.HasEdge(1,2));
        Assert.True(hvn.HasEdge(2,1));
        Assert.True(hvn.HasEdge(1,3));
        Assert.True(hvn.HasEdge(3,1));
        Assert.False(hvn.HasEdge(1,5));
    }
}