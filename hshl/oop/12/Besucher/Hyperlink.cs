public class Hyperlink : DocumentPart 
{
    public string Url { get; set; }

    public Hyperlink(string text, string url)
    {
        Text = text;
        Url = url;
    }

    public override void Accept(IDocumentConverterVisitor visitor)
    {
        visitor.Visit(this);
    }
}