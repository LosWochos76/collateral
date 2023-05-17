using System;

namespace Beobachter
{
    class Program
    {
        static void Main(string[] args)
        {
            var bank = new Bank();
            var konto1 = new Bankkonto();
            var konto2 = new Bankkonto();

            konto1.RegistriereBeobachter(bank);
            konto2.RegistriereBeobachter(bank);

            konto1.Einzahlen(1000);
            konto2.Auszahlen(1000);
        }
    }
}
