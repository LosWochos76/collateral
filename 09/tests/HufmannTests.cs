public class HufmannTests
{
    private Dictionary<char, int> frequencies = new ()
    {
        {'a', 50}, {'b', 20}, {'c', 30}
    };

    [Test]
    public void Test_ToHufmannTree()
    {
        var h = new Hufman(frequencies);
        var hn = h.Root;

        Assert.NotNull(hn);
        Assert.AreEqual(hn.Freq, 50 + 20 + 30);
        Assert.AreEqual(hn.Left.Character, 'a');
        Assert.AreEqual(hn.Left.Freq, 50);
    }

    [Test]
    public void Test_HasElements()
    {
        var h = new Hufman(frequencies);
        var original = "aaabc";
        
        var compressed = h.Compress(original);
        Assert.AreEqual("0001011", compressed);

        var decompressed = h.Decompress(compressed);
        Assert.AreEqual(original, decompressed);
    }
}