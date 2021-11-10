using AUD.Misc;
using System;
using Xunit;

namespace AUD.Tests
{
    public class NumberTests
    {
        [Fact]
        public void Number_IsPrime_0_is_not_prime()
        {
            Assert.False(Number.IsPrime(0));
        }

        [Fact]
        public void Number_IsPrime_2_is_prime()
        {
            Assert.True(Number.IsPrime(2));
        }

        [Fact]
        public void Number_IsPrime_4_is_not_prime()
        {
            Assert.False(Number.IsPrime(4));
        }

        [Fact]
        public void Number_IsPrime_23_is_prime()
        {
            Assert.True(Number.IsPrime(23));
        }

        [Fact]
        public void Number_IsPrime_997_is_prime()
        {
            Assert.True(Number.IsPrime(997));
        }

        [Fact]
        public void Number_Factorial_negative_Zahl()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Number.Factorial(-1));
        }

        [Fact]
        public void Number_Factorial_1()
        {
            Assert.Equal<long>(1, Number.Factorial(1));
        }

        [Fact]
        public void Number_Factorial_3()
        {
            Assert.Equal<long>(6, Number.Factorial(3));
        }

        [Fact]
        public void Number_Factorial_5()
        {
            Assert.Equal<long>(120, Number.Factorial(5));
        }

        [Fact]
        public void Number_Sum_negative_Zahl()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Number.Sum(-1));
        }

        [Fact]
        public void Number_Sum_1()
        {
            Assert.Equal<int>(1, Number.Sum(1));
        }

        [Fact]
        public void Number_Sum_5()
        {
            Assert.Equal<int>(5 + 4 + 3 + 2 + 1, Number.Sum(5));
        }
    }
}