using AUD.List;
using AUD.Misc;

namespace AUD.Search
{
    public class FibonacciSearch : ArrayList
    {
        private FibonacciCache cache;

        public FibonacciSearch(int[] data, int count) : base(data)
        {
            cache = new FibonacciCache(count);
        }

        public bool Contains(int search_value)
        {
            int index = cache.getIndexOfFirstFibonacciNumberLargerThan(data.Length);
            return containsFibonacciRecursive(search_value, 0, index);
        }

        private bool containsFibonacciRecursive(int search_value, int offset, int fib_idx)
        {
            int fib2 = 0;

            if (fib_idx < 0)
                return false;
            else
                fib2 = cache.GetFibonacci(fib_idx - 2);

            if (offset + fib2 < data.Length && data[offset + fib2] == search_value)
                return true;
            else if (offset + fib2 < data.Length && data[offset + fib2] < search_value)
                return containsFibonacciRecursive(search_value, offset + fib2, fib_idx - 1);
            else
                return containsFibonacciRecursive(search_value, offset, fib_idx - 2);
        }
    }
}
