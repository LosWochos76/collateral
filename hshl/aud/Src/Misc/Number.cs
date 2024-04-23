using System;

namespace AUD
{
    public class Number
    {
        public static int Sum(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException("Sum of negative numbers is not defined!");

            if (n == 1)
                return 1;
            else
                return n + Sum(n - 1);
        }

        public static long Factorial(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException("Factorial of negative numbers is not defined!");

            if (n == 0)
                return 1;
            else
                return n * Factorial(n - 1);
        }

        public static bool IsPrime(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                    return false;
            }

            return true;
        }

        public static int DigitSum(int n)
        {
            if (n < 10)
                return n;
            else
                return n % 10 + DigitSum(n / 10);
        }

        public static int GetFibonacci(int index)
        {
            // Was ist der Rekursionsanker? Was ist der Rekursionsschritt?

            return 0;
        }
    }
}
