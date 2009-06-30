using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Zainco.NewtonRaphson.IRRCalculator.Domain;
using Zainco.NewtonRaphson.IRRCalculator.Exceptions;

namespace Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Test_Should_Calculate_IRR()
        {
            var calculator = NewtonRaphsonIRRCalculator.Instance;
            calculator.CashFlows = new double[7] { -3000, 510, 131, -100, 9845, 43, 52867 };
            var expectedRateOfReturn = 0.77d;
            var actualRateOfReturn = calculator.Execute();
            Assert.AreEqual(expectedRateOfReturn, Math.Round(actualRateOfReturn,2), "The expected rate of return did not match");
        }

        [Test]
        [ExpectedException(typeof(IRRCalculationException))]
        public void Test_Should_Throw_Expected_Exception()
        {
            var calculator = NewtonRaphsonIRRCalculator.Instance;
            calculator.CashFlows = new double[7] { 3000, 510, 131, -100, 9845, 43, 52867 };
            calculator.Execute();
        }
    }
}
