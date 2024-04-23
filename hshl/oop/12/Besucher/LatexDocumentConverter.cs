using System.Text;

public class LatexDocumentConverter : IDocumentConverterVisitor
{
    private StringBuilder output = new StringBuilder();
    
    public void Visit(PlainText docPart)
    {
        output.Append($"{docPart.Text}\n");
    }

    public void Visit(BoldText docPart)
    {
        output.Append("\\textbf{" + docPart.Text + "}\n");
    }

    public void Visit(Hyperlink docPart)
    {
        output.Append("\\href{" + docPart.Url + "}{" + docPart.Text + "}");
    }

    public override string ToString()
    {
        return output.ToString();
    }
}