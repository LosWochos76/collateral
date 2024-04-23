public class PathTests
{
    [Test]
    public void Test_Directed_IsEdgeInPath()
    {
        var p = new Path();
        p.Add(new Edge(true, 1, 3));

        Assert.True(p.IsPartOf(new Edge(true, 1, 3)));
        Assert.False(p.IsPartOf(new Edge(true, 3, 1)));
    }

    [Test]
    public void Test_Undirected_IsEdgeInPath()
    {
        var p = new Path();
        p.Add(new Edge(false, 1, 3));
        p.Add(new Edge(false, 3, 4));
        p.Add(new Edge(false, 4, 6));

        Assert.True(p.IsPartOf(new Edge(false, 3, 4)));
        Assert.True(p.IsPartOf(new Edge(false, 4, 3)));
    }

    [Test]
    public void Test_Dircted_IsConnected()
    {
        var p = new Path();
        p.Add(new Edge(true, 1, 3));
        p.Add(new Edge(true, 3, 4));
        p.Add(new Edge(true, 4, 6));
        Assert.True(p.IsConnected);

        p.Clear();
        p.Add(new Edge(true, 1, 3));
        p.Add(new Edge(true, 4, 6));
        Assert.False(p.IsConnected);
    }

    [Test]
    public void Test_Dircted_IsCircle()
    {
        var p = new Path();
        p.Add(new Edge(true, 1, 3));
        p.Add(new Edge(true, 3, 4));
        p.Add(new Edge(true, 4, 6));
        Assert.False(p.IsCircle);

        p.Add(new Edge(true, 6, 1));
        Assert.True(p.IsConnected);
    }
}