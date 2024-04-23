public class KontoBeobachter : IObserver
{
    public void AenderungEingetreten(IObservable quelle)
    {
        var k = quelle as Bankkonto;
        Console.WriteLine("Änderung an Konto {0} eingetreten!", 
            k.GetHashCode());
        Console.WriteLine("Neuer Kontostand: {0:C}.", k.Kontostand);
    }
}