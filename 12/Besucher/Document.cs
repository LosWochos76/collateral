using System.Collections.Generic;

public class Document
{
  public List<DocumentPart> Parts { get; private set; } = new List<DocumentPart>();

  public void Accept(IDocumentConverterVisitor visitor)
  {
    foreach (var part in Parts)
      part.Accept(visitor);
  }
}