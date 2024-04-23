package de.hshl.visitor;

public class PlainText extends DocumentPart {

   public PlainText(String text) {
      super(text);
   }

   public void accept(DocumentConverterVisitor converter) {
      converter.visit(this);
   }
}