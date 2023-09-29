public class Fibonacci
{
    public static int[] Fill(int n)
    {
        int[] fib = new int[n];
        fib[0] = 1;
        fib[1] = 1;

        for (int i=2; i<n; i++)
            fib[i] = fib[i-2] + fib[i-1];
        
        return fib;
    }
}