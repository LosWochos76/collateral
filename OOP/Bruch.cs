using System;

namespace OOP
{
    class Bruch : IBruch, IAusgebbar, IDisposable, IComparable
    {
        private int _zaehler;
        private int _nenner;
        private static int anzahl_objekte = 0;

        public Bruch(int zaehler, int nenner)
        {
            Zaehler = zaehler;
            Nenner = nenner;
            anzahl_objekte++;
        }

        public Bruch(int zahl) : this(zahl, 1)
        {
        }

        public int Zaehler
        {
            get { return _zaehler; }
            set { _zaehler = value; }
        }

        public int Nenner
        {
            get { return _nenner; }
            set
            {
                if (value == 0)
                    throw new Exception("Der Nenner darf nicht 0 sein!");

                _nenner = value;
            }
        }

        public double Bruchzahl
        {
            get { return _zaehler / _nenner; }
        }

        public void ausgeben()
        {
            Console.WriteLine(_zaehler + "/" + _nenner);
        }

        public IBruch kehrwert()
        {
            return new Bruch(_nenner, _zaehler);
        }

        public IBruch multipliziere(int zahl)
        {
            return new Bruch(_zaehler * zahl, _nenner);
        }

        public IBruch multipliziere(IBruch b)
        {
            return new Bruch(_zaehler * b.Zaehler, _nenner * b.Nenner);
        }

        public void Dispose()
        {
            Console.WriteLine("Objekt vom Typ Bruch wird zerstört!");
        }

        public override bool Equals(Object o)
        {
            if (o == null)
                return false;

            Bruch b = o as Bruch;
            if (b != null)
                return b.Zaehler == Zaehler && b.Nenner == Nenner;
            else
                return false;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            var b = obj as Bruch;
            if (b == null)
                throw new ArgumentException("Übergebenes Objekt ist kein Bruch!");

            if (b.Bruchzahl > Bruchzahl)
                return 1;
            else if (b.Bruchzahl < Bruchzahl)
                return -1;
            else
                return 0;
        }
    }
}
