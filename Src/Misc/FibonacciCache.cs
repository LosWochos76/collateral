using AUD.HashMap;

namespace AUD.Misc
{
    public class FibonacciCache
    {
        private static GenericHashMap<int, int> fibonacciCache;

        public FibonacciCache(int count)
        {
            fibonacciCache = new GenericHashMap<int, int>(count);
            for (int i=0; i<count; i++)
            {
                fibonacciCache.Insert(i, Fibonacci.GetFibonacci(i));
            }
        }

        public int getIndexOfFirstFibonacciNumberLargerThan(int value)
        {
            int index = 0;
            while (true)
            {
                if (fibonacciCache.GetValue(index) > value)
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
