using System;
using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        var l = new ArrayList();
        for (int i=0; i<20; i++)
            l.Add(new Bruch(1, i +1));

        for (int i=0; i<20; i++)
        {
            var b = (Bruch)l[i];
            b.Ausgeben();
        }
    }
}
