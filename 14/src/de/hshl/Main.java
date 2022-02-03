package de.hshl;

import java.util.Random;

public class Main {

   public static void main(String[] args) {
      Random random = new Random();
      ArrayList list = new ArrayList();
      
      for (int i=0; i<1000; i++) {
         list.add(random.nextInt());
      }

      for (int i=0; i<1000; i++) {
         System.out.println(i + ": " + list.get(i));
      }
   }
}
