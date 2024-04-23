using AUD.HashMap;

namespace AUD.Misc
{
    public class FibonacciCache
    {
        private static GenericHashMap<int, int> fibonacciCache;

        public FibonacciCache(int count)
        {
            fibonacciCache = new GenericHashMap<int, int>(count);
            for (int i = 0; i < count; i++)
            {
                var fib = Fibonacci.GetFibonacci(i);
                fibonacciCache.Insert(i, fib);
            }
        }

        public int getIndexOfFirstFibonacciNumberLargerThan(int value)
        {
            int index = 0;
            while (true)
            {
                var fib = fibonacciCache.GetValue(index);
                if (fib > value)
                    return index;

                index++;
            }
        }

        public int GetFibonacci(int index)
        {
            return fibonacciCache.GetValue(index);
        }
    }
}
