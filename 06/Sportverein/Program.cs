using System;

class Program
{
    static void Main(string[] args)
    {
        Sportverein s = new Sportverein("TUS Hamm");
        s.Vorsitzender = new Mitglied("Erika", "Wischnowski", "3.2.2003");
        s.MitgliedHinzufuegen(new Mitglied("Peter", "Pan", "6.8.1997"));

        s.Ausgeben();
    }
}