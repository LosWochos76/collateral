public class PathTests
{
    [Test]
    public void Test_Directed_IsEdgeInPath()
    {
        var p = new Path();
        p.Push(new Edge(true, 1, 3, 1));

        Assert.True(p.IsInPath(new Edge(true, 1, 3, 1)));
        Assert.False(p.IsInPath(new Edge(true, 3, 1, 1)));
    }

    [Test]
    public void Test_Undirected_IsEdgeInPath()
    {
        var p = new Path();
        p.Push(new Edge(false, 1, 3, 1));

        Assert.True(p.IsInPath(new Edge(false, 1, 3, 1)));
        Assert.True(p.IsInPath(new Edge(false, 3, 1, 1)));
    }
}