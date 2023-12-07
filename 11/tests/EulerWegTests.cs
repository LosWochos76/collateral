public class EulerWegTests
{
    [Test]
    public void Test_FindSolutions()
    {
        var hvn = HausVomNikolaus.Erzeuge();
        var ew = new EulerWeg(hvn);
        var solutions = ew.FindSolutions();

        Assert.AreEqual(88, solutions.Count);
        Assert.IsTrue(solutions.Contains("1->2,2->3,3->1,1->4,4->3,3->5,5->4,4->2"));

        foreach (var s in solutions)
        {
            var start = s.Substring(0, 3);
            Assert.IsTrue(start.Equals("1->") || start.Equals("2->"));
        }
    }
}