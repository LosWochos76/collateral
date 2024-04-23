package de.hshl;

import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        Bruch[] brueche_array = ArrayHelper.erzeugeListe(10);
        double summe1 = ArrayHelper.bildeSumme(brueche_array);
        System.out.println("1. Summe=" + summe1);

        ArrayList brueche_arraylist = ArrayListHelper.erzeugeListe(10);
        double summe2 = ArrayListHelper.bildeSumme(brueche_arraylist);
        System.out.println("2. Summe=" + summe2);

        ArrayList<Bruch> brueche_arraylist_generisch = GenericArrayListHelper.erzeugeListe(10);
        double summe3 = GenericArrayListHelper.bildeSumme(brueche_arraylist_generisch);
        System.out.println("3. Summe=" + summe3);
        
        var ll = new LinkedList<Integer>();
        ll.push(1);
        ll.push(2);
        ll.push(3);
        
        while (ll.hasMore()) {
           System.out.println(ll.pop());
        }

        Ausgebbar b = new Bruch(1,3);
        b.ausgeben();
    }
}
