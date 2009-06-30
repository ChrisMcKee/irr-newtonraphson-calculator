using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Zainco.NewtonRaphson.IRRCalculator.Domain;

namespace Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void TestConvergence()
        {
            double[] cashFlows = new double[7] { -3000, 510, 131, -100, 9845, 43, 52867 };
            var calculator = new NewtonRaphsonIRRCalculator(cashFlows);

            var expectedRateOfReturn = 0.77d;
            var actualRateOfReturn = calculator.Execute();
            Assert.AreEqual(expectedRateOfReturn, Math.Round(actualRateOfReturn,2), "The expected rate of return did not match");
        }
    }
}
