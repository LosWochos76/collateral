namespace AUD.Misc
{
    public class Fibonacci
    {
        public static int GetFibonacci(int n)
        {
            if (n <= 0)
                return 0;

            if (n == 1 || n == 2)
                return 1;

            return GetFibonacci(n - 1) + GetFibonacci(n - 2);
        }
    }
}
