package de.hshl;

import java.util.Random;
import de.hshl.factory.PersonFactory;
import de.hshl.iterator.LinkedList;
import de.hshl.iterator.LinkedListIterator;
import de.hshl.observer.Bankkonto;
import de.hshl.observer.BankkontoBeobachter;
import de.hshl.singleton.DatabaseConnection;
import de.hshl.visitor.BoldText;
import de.hshl.visitor.Document;
import de.hshl.visitor.HtmlDocumentConverter;
import de.hshl.visitor.Hyperlink;
import de.hshl.visitor.LatexDocumentConverter;
import de.hshl.visitor.PlainText;
import strategy.FastestRoutingStrategy;
import strategy.Routeplaner;
import strategy.ShortestRoutingStrategy;

public class Main {

   private static void testeFabrik() {
      var p1 = PersonFactory.erzeugePersonAusDaten("Alexander;Stuckenholz;Praktische Informatik");
      System.out.println(p1.getClass().toString() + ": " + p1);

      var p2 = PersonFactory.erzeugePersonAusDaten("Elfride;Jelinek;12345");
      System.out.println(p2.getClass().toString() + ": " + p2);

      var p3 = PersonFactory.erzeugePersonAusDaten("Eberhard;Julicek");
      System.out.println(p3.getClass().toString() + ": " + p3);
   }

   private static void testeSingleton() {
      var db1 = DatabaseConnection.getInstance();
      var db2 = DatabaseConnection.getInstance();

      if (db1 == db2)
         System.out.println("Das Singleton hat zwei Mal das selbe Objekt geliefert!");
   }

   private static void testeIterator() {
      var ll = new LinkedList<Integer>();
      var rnd = new Random();
      for (int i = 0; i < 10; i++) {
         ll.push(rnd.nextInt(100));
      }

      System.out.println("Inhalt der Liste über Interatot:");
      var lli = new LinkedListIterator<>(ll);
      while (lli.hasNext())
         System.out.println(lli.next());

      System.out.println("Inhalt der Liste über for-Schleife mittels Iterator:");
      for (var value : ll) {
         System.out.println(value);
      }
   }

   public static void testeStrategy() {
      var rp = new Routeplaner();
      rp.setStrategy(new FastestRoutingStrategy());
      var route1 = rp.navigate("Hamm", "Hamburg");
      System.out.println("Dauer Route1: " + route1.getDuration());

      rp.setStrategy(new ShortestRoutingStrategy());
      var route2 = rp.navigate("Hamm", "Hamburg");
      System.out.println("Dauer Route2: " + route2.getDuration());
   }

   public static void testeVisitor() {
      var doc = new Document();
      doc.add(new BoldText("Überschrift"));
      doc.add(new PlainText("Dies ist ein normaler Textabschnitt"));
      doc.add(new Hyperlink("Dies ist ein Link", "https://www.hshl.de"));

      var html = new HtmlDocumentConverter();
      doc.accept(html);
      System.out.println("Html:\n" + html);
      
      var latex = new LatexDocumentConverter();
      doc.accept(latex);
      System.out.println("Latex:\n" + latex);
   }
   
   public static void testeObserver() {
      var k = new Bankkonto();
      var l = new BankkontoBeobachter();
      k.addPropertyChangeListener(l);
      
      k.einzahlen(1000);
      k.auszahlen(500);
      k.auszahlen(50);
   }

   public static void main(String[] args) {
      testeFabrik();
      testeSingleton();
      testeIterator();
      testeStrategy();
      testeVisitor();
      testeObserver();
   }
}