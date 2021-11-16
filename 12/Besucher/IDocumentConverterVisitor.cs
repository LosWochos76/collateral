public interface IDocumentConverterVisitor 
{
  void Visit(PlainText docPart);
  void Visit(BoldText docPart);
  void Visit(Hyperlink docPart);
}