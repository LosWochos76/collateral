package de.hshl.visitor;

public class BoldText extends DocumentPart {

   public BoldText(String text) {
      super(text);
   }
   
   public void accept(DocumentConverterVisitor converter) {
      converter.visit(this);
   }
}