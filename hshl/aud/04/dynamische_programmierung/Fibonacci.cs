public class Fibonacci
{
    private static Dictionary<long, long> mem = new Dictionary<long, long>() { {1,1}, {2,1}};

    public static long Fib(long n)
    {
        if (n < 1)
            throw new ArgumentException("Only number > 0 allowed!");

        if (mem.ContainsKey(n))
            return mem[n];

        mem[n] = Fib(n-1) + Fib(n-2);
        return mem[n];
    }
}