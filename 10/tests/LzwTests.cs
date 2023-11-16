public class LzwTests
{
    [Test]
    public void Test_Encode_Decode()
    {
        var text = "bananas-bananas";
        var code = LZW.Compress(text);
        var text1 = LZW.Decompress(code);
        Assert.AreEqual(text, text1);
    }
}