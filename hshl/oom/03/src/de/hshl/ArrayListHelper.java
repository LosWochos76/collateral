package de.hshl;

import java.util.ArrayList;

public class ArrayListHelper {
    public static ArrayList erzeugeListe(int anzahl) {
        ArrayList brueche = new ArrayList();
        for (int i=0; i<anzahl; i++) {
            brueche.add(new Bruch(1, i+1));
        }

        return brueche;
    }

    public static double bildeSumme(ArrayList brueche) {
        double summe = 0;
        for (int i=0; i<brueche.size(); i++) {
            Bruch b = (Bruch)brueche.get(i);
            summe += b.getDezimalzahl();
        }

        return summe;
    }
}