package de.hshl;

import java.util.ArrayList;

public class Sportverein {
    private String name;
    private Mitglied vorsitzender;
    private ArrayList<Mitglied> mitglieder = new ArrayList<Mitglied>();
  
    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
  
    public Sportverein(String n) {
        setName(n);
    }

    public Mitglied getVorsitzenden() { 
        return vorsitzender; 
    }

    public void setVorsitzenden(Mitglied v) throws Exception {
        if (v == null)
            throw new Exception("Der Verein muss einen Vorsitzenden haben!");

        vorsitzender = v;
    }

    public void mitgliedHinzufuegen(Mitglied m) {
        mitglieder.add(m);
    }

    public Iterable<Mitglied> alleMitglieder() {
        return mitglieder;
    }
}