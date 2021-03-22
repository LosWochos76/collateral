namespace OOP
{
    interface IBruch
    {
        double Bruchzahl { get; }
        int Nenner { get; set; }
        int Zaehler { get; set; }

        IBruch kehrwert();
        IBruch multipliziere(int zahl);
        IBruch multipliziere(IBruch b);
    }
}