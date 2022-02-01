package de.hshl.visitor;

import java.util.ArrayList;

public class Document extends ArrayList<DocumentPart> {
   public void accept(DocumentConverterVisitor converter) {
      for (var part : this)
         part.accept(converter);
   }
}