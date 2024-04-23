package de.hshl.visitor;

public class LatexDocumentConverter implements DocumentConverterVisitor {
   private StringBuilder content = new StringBuilder(); 
   
   @Override
   public void visit(PlainText docPart) {
      content.append(docPart.text + "\n");
   }

   @Override
   public void visit(BoldText docPart) {
      content.append("\\textbf{" + docPart.text + "}\n");
   }

   @Override
   public void visit(Hyperlink docPart) {
      content.append("\\href{" + docPart.url + "}{" + docPart.text + "}\n");
   }
   
   @Override
   public String toString() {
      return "\\documentclass[a4paper, 12pt]{article}\n" +
         "\\usepackage[utf8]{inputenc}\n" +
         "\\usepackage{hyperref}\n" +
         "\\begin{document}\n" +
         content +
         "\\end{document}";
   }
}