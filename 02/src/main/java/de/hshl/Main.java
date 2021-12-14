package de.hshl;

import java.util.ArrayList;

public class Main {
    public static Bruch[] erzeugeBruchArray() {
        Bruch[] brueche = new Bruch[10];
        for (int i=0; i<10; i++) {
            brueche[i] = new Bruch(1, i+1);
        }

        return brueche;
    }

    public static double bildeSumme(Bruch[] brueche) {
        double summe = 0;
        for (int i=0; i<10; i++) {
            summe += brueche[i].getDezimalzahl();
        }

        return summe;
    }

    public static ArrayList erzeugeBruchArrayList() {
        ArrayList brueche = new ArrayList();
        for (int i=0; i<10; i++) {
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

    public static void main(String[] args) {
        Bruch b = new Bruch(1,3);
        Bruch c = new Bruch(1,4);
        b.ausgeben();
        c.ausgeben();
        b.multipliziere(c).ausgeben();
        System.out.println(b.equals(c));

        // Wirft eine Ausnahme:
        // Bruch d = new Bruch(1,0);

        Bruch[] brueche_array = erzeugeBruchArray();
        double summe1 = bildeSumme(brueche_array);
        System.out.println("Summe aus dem Array=" + summe1);

        ArrayList brueche_arraylist = erzeugeBruchArrayList();
        double summe2 = bildeSumme(brueche_arraylist);
        System.out.println("Summe aus der ArrayList=" + summe2);
    }
}
