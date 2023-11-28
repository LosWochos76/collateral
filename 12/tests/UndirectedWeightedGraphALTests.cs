public class UndirectedWeightedGraphALTests
{
    [Test]
    public void Test_GetWeight()
    {
        var graph = new UndirectedWeightedGraphAL();
        graph.AddEdge(1,2,5);
        Assert.IsTrue(graph.HasEdge(1,2));
        Assert.IsTrue(graph.HasEdge(2,1));
        Assert.AreEqual(5, graph.GetWeight(1,2));
        Assert.AreEqual(5, graph.GetWeight(2,1));
    }
}