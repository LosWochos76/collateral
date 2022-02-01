package de.hshl.visitor;

public interface DocumentConverterVisitor {
   void visit(PlainText docPart);
   void visit(BoldText docPart);
   void visit(Hyperlink docPart);
}
