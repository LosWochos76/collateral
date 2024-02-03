using System;

class Program
{
    static void Main(string[] args)
    {
        var fastest = new FastestRoutingStrategy();
        var planer = new Routeplaner(fastest);
        var route = planer.ExecuteStrategy();
        Console.WriteLine(route.Path + " --> " + route.Duration);

        planer.Strategy = new ShortestRoutingStrategy();
        route = planer.ExecuteStrategy();
        Console.WriteLine(route.Path + " --> " + route.Duration);
    }
}