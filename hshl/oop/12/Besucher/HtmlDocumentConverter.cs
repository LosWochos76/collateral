using System.Text;

public class HtmlDocumentConverter : IDocumentConverterVisitor
{
    private StringBuilder output = new StringBuilder();
    
    public void Visit(PlainText docPart)
    {
        output.Append($"{docPart.Text}\n");
    }

    public void Visit(BoldText docPart)
    {
        output.Append($"<b>{docPart.Text}</b>\n");
    }

    public void Visit(Hyperlink docPart)
    {
        output.Append($"<a href=\"{docPart.Url}\">{docPart.Text}</a>");
    }

    public override string ToString()
    {
        return output.ToString();
    }
}