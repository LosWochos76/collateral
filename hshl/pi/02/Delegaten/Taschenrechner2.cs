public class Taschenrechner2
{
    public double ZahlEinlesen()
    {
        Console.WriteLine("Bitte Zahl eingeben:");
        return Convert.ToDouble(Console.ReadLine());
    }

    public char BerechnungsmethodeEinlesen()
    {
        Console.WriteLine("Bitte Berechnungsmethode eingeben (+,-,*,/): ");
        return Convert.ToChar(Console.ReadLine());
    }

    public Func<double, double, double> ToHandler(char c)
    {
        switch (c) {
            case '+': return (double a, double b) => a + b;
            case '-': return (double a, double b) => a - b;
            case '*': return (double a, double b) => a * b;
            default: return (double a, double b) => a / b;
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