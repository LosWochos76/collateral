package de.hshl;

public class Main {
    public static void main(String[] args) {
        try {
            Sportverein s = new Sportverein("TUS Hamm");
            s.setVorsitzenden(new Mitglied("Erika", "Wischnowski", "2003-03-01"));
            s.mitgliedHinzufuegen(new Mitglied("Peter", "Pan", "1997-08-06"));

            for (Mitglied m : s.alleMitglieder()) {
                System.out.println(m.getVorname());
            }
        } catch (Exception e) {
            System.out.println(e.toString());
        }
    }
}
