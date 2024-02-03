using System;

class Program
{
    static void Main(string[] args)
    {
        var konto = new Bankkonto();
        konto.Einzahlen(1000);
        var kunde = new Bankkunde("Edeltraut", "Klimczack", konto);
        kunde.Ausgeben();
    }
}