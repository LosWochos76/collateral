using System;
using AUD.Graphs;
using AUD.Search;

namespace AUD
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new int[] { 1 };
            var search = new FibonacciSearch(data, 20);
            search.Contains(1);
        }
    }
}
