public class Fibonacci
{
    private static Dictionary<long, long> mem = new Dictionary<long, long>();

    public static long Fib(long n)
    {
        if (n < 1)
            throw new ArgumentException("Only number > 0 allowed!");

        if (n == 1 || n == 2)
            return 1;
        
        return Fib(n-1) + Fib(n-2);
    }

    public static long FibWithMemory(long n)
    {
        if (n < 1)
            throw new ArgumentException("Only number > 0 allowed!");

        if (n == 1 || n == 2)
            return 1;

        var n_1 = mem.ContainsKey(n-1) ? mem[n-1] : FibWithMemory(n-1);
        mem[n-1] = n_1;

        var n_2 = mem.ContainsKey(n-2) ? mem[n-1] : FibWithMemory(n-2);
        mem[n-2] = n_2;

        return n_1 + n_2;
    }
}