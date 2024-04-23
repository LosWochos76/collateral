package de.hshl;

import java.util.ArrayList;

public class GenericArrayListHelper {
    public static ArrayList<Bruch> erzeugeListe(int anzahl) {
        ArrayList<Bruch> brueche = new ArrayList<Bruch>();
        for (int i=0; i<anzahl; i++) {
            brueche.add(new Bruch(1, i+1));
        }

        return brueche;
    }

    public static double bildeSumme(ArrayList<Bruch> brueche) {
        double summe = 0;
        for (int i=0; i<brueche.size(); i++) {
            summe += brueche.get(i).getDezimalzahl();
        }

        return summe;
    }
}
