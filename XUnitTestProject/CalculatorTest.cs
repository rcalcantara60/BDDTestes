using System;
using TesteBDDFullFramework;
using Xunit;

namespace XUnitTestProject
{
    public class CalculatorTest
    {
        [Fact]
        public void DeveCalcularASoma()
        {
            Calculator calculator = new Calculator();
            calculator.FirstNumber = 2;
            calculator.SecondNumber = 3;

            Assert.Equal(5, calculator.Add());
        }
    }
}
