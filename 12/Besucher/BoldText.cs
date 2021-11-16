public class BoldText : DocumentPart 
{ 
    public BoldText(string text)
    {
        Text = text;
    }

    public override void Accept(IDocumentConverterVisitor visitor)
    {
        visitor.Visit(this);
    }
}