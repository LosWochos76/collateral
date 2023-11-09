public class HufmannTests
{
    [Test]
    public void Test_ToHufmannTree()
    {
        var h = new Hufman(new Dictionary<char, int>{ {'a', 50} });
        var hn = h.Root;

        Assert.NotNull(hn);
        Assert.AreEqual(hn.Freq, 50);
        Assert.AreEqual(hn.Character, 'a');
        Assert.IsNull(hn.Left);
        Assert.IsNull(hn.Right);
    }

    [Test]
    public void Test_GetCode()
    {
        var h = new Hufman(new ()
        {
            {'a', 50}, {'b', 20}, {'c', 30}
        });

        Assert.AreEqual("0", h.GetCodeFor('a'));
        Assert.AreEqual("10", h.GetCodeFor('b'));
        Assert.AreEqual("11", h.GetCodeFor('c'));
    }

    [Test]
    public void Test_Compress_Decompress()
    {
        var h = new Hufman(new ()
        {
            {'a', 50}, {'b', 20}, {'c', 30}
        });

        var original = "aaabc";
        var compressed = h.Compress(original);
        Assert.AreEqual("0001011", compressed);
        var decompressed = h.Decompress(compressed);
        Assert.AreEqual(original, decompressed);
    }
}