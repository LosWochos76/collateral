using System;

class Program
{
    static void Main(string[] args)
    {
        var p = new Person("Ingrid", "Müller");
        p.Wohnadresse = new Adresse("Im Rosenhang 12", 59063, "Hamm");

        var s = new Student("Demir", "Öztürk", 12345);
        s.Wohnadresse = new Adresse("Schlehenstraße 26", 59063, "Hamm");
    }
}