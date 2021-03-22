using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Bigint
    {
        private int[] number;

        public static Bigint FromString(string s)
        {
            int places = s.Length;
            var result = new Bigint(places);

            for (int i=0; i<places; i++)
                result.Set(places - i - 1, s[i] - 48);

            return result;
        }

        public Bigint(int places)
        {
            number = new int[places];
        }

        public int Places
        {
            get { return number.Length; }
        }

        public void Set(int place, int value)
        {
            number[number.Length - place - 1] = value;
        }

        public int Get(int place)
        {
            return number[number.Length - place - 1];
        }

        private Bigint BringToPlaces(int places)
        {
            var result = new Bigint(places);

            for (int i=0; i<Math.Min(Places, places); i++)
            {
                int n = Get(i);
                result.Set(i, n);
            }

            return result;
        }

        public Bigint Add(Bigint b)
        {
            var max_places = Math.Max(Places, b.Places);
            var result = new Bigint(max_places + 1);

            var a1 = BringToPlaces(max_places);
            var a2 = b.BringToPlaces(max_places);

            int last = 0;
            for (int i=0; i<max_places; i++)
            {
                int z = a1.Get(i) + a2.Get(i) + last;
                result.Set(i, z % 10);
                last = z / 10;
            }

            result.Set(max_places, last);

            if (result.Get(max_places) == 0)
                result = result.BringToPlaces(max_places);

            return result;
        }
    }
}
