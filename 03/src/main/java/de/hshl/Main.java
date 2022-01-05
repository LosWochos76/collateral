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

        Tuple<Integer, Integer> t1 = new Tuple<Integer, Integer>(5, 7);
    }
}
