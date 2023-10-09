using System.Diagnostics;

Stopwatch ws = new Stopwatch();
ws.Start();
Console.WriteLine(Fibonacci.Fib(43));
ws.Stop();
Console.WriteLine(ws.Elapsed);

ws = new Stopwatch();
ws.Start();
Console.WriteLine(Fibonacci.FibWithMemory(43));
ws.Stop();
Console.WriteLine(ws.Elapsed);