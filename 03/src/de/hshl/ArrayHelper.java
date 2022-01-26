package de.hshl;

public class ArrayHelper {
    public static Bruch[] erzeugeListe(int anzahl) {
        Bruch[] brueche = new Bruch[anzahl];
        for (int i=0; i<anzahl; i++) {
            brueche[i] = new Bruch(1, i+1);
        }

        return brueche;
    }

    public static double bildeSumme(Bruch[] brueche) {
        double summe = 0;
        for (int i=0; i<brueche.length; i++) {
            summe += brueche[i].getDezimalzahl();
        }

        return summe;
    }
}