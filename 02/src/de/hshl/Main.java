package de.hshl;

public class Main {
    public static void main(String[] args) {
        Bruch b = new Bruch(1,3);
        Bruch c = new Bruch(1,4);
        b.ausgeben();
        c.ausgeben();
        b.multipliziere(c).ausgeben();
        System.out.println(b.equals(c));
    }
}