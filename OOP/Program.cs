using System;
using System.Collections;
using System.Collections.Generic;

namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            var b1 = Bigint.FromString("1000000000000000000000000000000");
            var b2 = Bigint.FromString("2000000000000000000000000000000");
            var b3 = b1.Add(b2);
        }
    }
}

