package de.hshl;

public class Bruch {
    private int zaehler;
    private int nenner;

    public Bruch(int zaehler, int nenner) {
        setZaehler(zaehler);
        setNenner(nenner);
    }

    public void setZaehler(int zaehler) {
        this.zaehler = zaehler;
    }

    public int getZaehler() {
        return zaehler;
    }

    public void setNenner(int nenner) {
        if (nenner == 0)
            throw new IllegalArgumentException("Nenner darf nicht 0 sein!");

        this.nenner = nenner;
    }

    public double getDezimalzahl() {
        return (double)zaehler / nenner;
    }

    public void ausgeben() {
        System.out.println(zaehler + "/" + nenner);
    }

    public Bruch bildeKehrwert() {
        return new Bruch(nenner, zaehler);
    }

    public Bruch multipliziere(int zahl) {
        return new Bruch(zaehler * zahl, nenner);
    }

    public Bruch multipliziere(Bruch b) {
        return new Bruch(zaehler * b.zaehler, nenner * b.nenner);
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == null && !(obj instanceof Bruch))
            return false;
        
        Bruch b = (Bruch)obj;
        return b.zaehler == zaehler && b.nenner == nenner;
    }
}
