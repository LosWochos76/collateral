using System;

class Program
{
    static void Main(string[] args)
    {
        var doc = new Document();
        doc.Parts.Add(new BoldText("Überschrift"));
        doc.Parts.Add(new PlainText("Dies ist ein normaler Textabschnitt"));
        doc.Parts.Add(new Hyperlink("Dies ist ein Link", "https://www.hshl.de"));

        var html = new HtmlDocumentConverter();
        doc.Accept(html);
        Console.WriteLine("Als Html:\n" + html.ToString() + "\n");

        var latex = new LatexDocumentConverter();
        doc.Accept(latex);
        Console.WriteLine("Als Latex:\n" + latex.ToString() + "\n");
    }
}