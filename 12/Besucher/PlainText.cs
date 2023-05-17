public class PlainText : DocumentPart 
{ 
    public PlainText(string text)
    {
        Text = text;
    }

    public override void Accept(IDocumentConverterVisitor visitor)
    {
        visitor.Visit(this);
    }
}