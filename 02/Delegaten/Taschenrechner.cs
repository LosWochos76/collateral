public delegate double BerechnungsVerarbeiter(double a, double b);

public class Taschenrechner
{
    public double ZahlEinlesen()
    {
        Console.WriteLine("Bitte 1. Zahl eingeben:");
        return Convert.ToDouble(Console.ReadLine());
    }

    public char BerechnungsmethodeEinlesen()
    {
        Console.WriteLine("Bitte Berechnungsmethode eingeben (+,-,*,/): ");
        return Convert.ToChar(Console.ReadLine());
    }

    public double Addiere(double a, double b)
    {
        return a + b;
    }

    public double Multipliziere(double a, double b)
    {
        return a * b;
    }

    public double Subtrahiere(double a, double b)
    {
        return a - b;
    }

    public double Dividiere(double a, double b)
    {
        return a / b;
    }

    public BerechnungsVerarbeiter ToHandler(char c)
    {
        switch (c) {
            case '+': return Addiere;
            case '-': return Subtrahiere;
            case '*': return Multipliziere;
            default: return Dividiere;
        }
    }

    public void Rechne()
    {
        var zahl1 = ZahlEinlesen();
        var methode = ToHandler(BerechnungsmethodeEinlesen());
        var zahl2 = ZahlEinlesen();

        var ergebnis = methode(zahl1, zahl2);
        Console.WriteLine("Ergebnis={0}", ergebnis);
    }
}