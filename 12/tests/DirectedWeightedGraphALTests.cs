public class DirectedWeightedGraphALTests
{
    [Test]
    public void Test_GetWeight()
    {
        var graph = new DirectedWeightedGraphAL();
        graph.AddEdge(1,2,5);
        Assert.IsTrue(graph.HasEdge(1,2));
        Assert.IsFalse(graph.HasEdge(2,1));
        Assert.AreEqual(5, graph.GetWeight(1,2));
        Assert.AreEqual(0, graph.GetWeight(2,1));
    }
}