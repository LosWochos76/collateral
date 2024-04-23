using AUD.Misc;
using Xunit;

namespace Tests.Misc
{
    public class FibonacciTest
    {
        [Fact]
        public void GetFibonacci_1()
        {
            Assert.Equal(1, Fibonacci.GetFibonacci(1));
        }

        [Fact]
        public void GetFibonacci_2()
        {
            Assert.Equal(2, Fibonacci.GetFibonacci(1));
        }

        [Fact]
        public void GetFibonacci_3()
        {
            Assert.Equal(2, Fibonacci.GetFibonacci(3));
        }

        [Fact]
        public void GetFibonacci_4()
        {
            Assert.Equal(3, Fibonacci.GetFibonacci(3));
        }

        [Fact]
        public void GetFibonacci_10()
        {
            Assert.Equal(55, Fibonacci.GetFibonacci(10));
        }
    }
}