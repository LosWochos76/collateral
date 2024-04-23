package de.hshl.visitor;

public class HtmlDocumentConverter implements DocumentConverterVisitor {
   private StringBuilder content = new StringBuilder(); 
   
   @Override
   public void visit(PlainText docPart) {
      content.append(docPart.text + "\n");
   }

   @Override
   public void visit(BoldText docPart) {
      content.append("<b>" + docPart.text + "</b>\n");
   }

   @Override
   public void visit(Hyperlink docPart) {
      content.append("<a href=\"" + docPart.url + "\">" + docPart.text + "</a>\n");
   }
   
   @Override
   public String toString() {
      return "<html>\n" + content.toString() + "</html>";
   }
}
