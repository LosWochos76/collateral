using System;
using System.Collections;
using System.Collections.Generic;

namespace OOP
{
    class MitgliedsListe : IEnumerator<Mitglied>, IEnumerable<Mitglied>
    {
        private List<Mitglied> mitglieder = new List<Mitglied>();
        private int pos = -1;

        public void Add(Mitglied m)
        {
            mitglieder.Add(m);
        }

        private List<Mitglied> ByGeschlecht(int geschlecht)
        {
            var result = new List<Mitglied>();
            foreach (var m in mitglieder)
            {
                if (m.Geschlecht == geschlecht)
                    result.Add(m);
            }

            return result;
        }

        public List<Mitglied> AlleMaenner()
        {
            return ByGeschlecht(0);
        }

        public List<Mitglied> AlleFrauen()
        {
            return ByGeschlecht(1);
        }

        public MitgliedsListe HeuteGeburtstag()
        {
            var result = new MitgliedsListe();
            foreach (var m in mitglieder)
            {
                if (m.Geburtsdatum.Month == DateTime.Today.Month 
                    && m.Geburtsdatum.Day == DateTime.Today.Day)
                    result.Add(m);
            }

            return result;
        }

        public Mitglied Current
        {
            get
            {
                return mitglieder[pos];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return mitglieder[pos];
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (pos + 2 > mitglieder.Count)
                return false;

            pos++;
            return true;
        }

        public void Reset()
        {
            pos = 0;
        }

        public IEnumerator<Mitglied> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}