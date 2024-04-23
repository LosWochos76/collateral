using System;
using System.Collections.Generic;

namespace Beobachter
{
    public class Bankkonto : IObservable
    {
        private List<IObserver> beobachter = new List<IObserver>();
        private double kontostand;
        
        public double Kontostand 
        {
            get { return kontostand; }
        }

        public void Einzahlen(double betrag)
        {
            kontostand += betrag;
            BenachrichtigeAlleBeobachter();
        }

        public void Auszahlen(double betrag)
        {
            kontostand -= betrag;
            BenachrichtigeAlleBeobachter();
        }

        public void RegistriereBeobachter(IObserver b)
        {
            beobachter.Add(b);
        }

        public void EntferneBeobachter(IObserver b)
        {
            beobachter.Remove(b);
        }

        public void BenachrichtigeAlleBeobachter()
        {
            foreach (var b in beobachter)
                b.AenderungIstEingetreten(this);
        }
    }
}