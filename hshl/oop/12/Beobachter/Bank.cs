using System;

namespace Beobachter
{
    public class Bank : IObserver
    {
        public void AenderungIstEingetreten(IObservable quelle)
        {
            var konto = quelle as Bankkonto;

            if (konto != null)
            {
                Console.WriteLine("Eine Änderung an einem Bankkonto ist eingetreten!");
                Console.WriteLine("Neuer Kontostand: " + konto.Kontostand);
            }
        }
    }
}