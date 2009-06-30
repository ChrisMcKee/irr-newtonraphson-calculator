using System;
using Zainco.NewtonRaphson.IRRCalculator.Domain;
using System.Collections.Generic;

namespace Zainco.NewtonRaphson.IRRCalculator
{
    internal class Program
    {
        public static void Main()
        {
            var calculator = NewtonRaphsonIRRCalculator.Instance;
            calculator.CashFlows = new List<double> { -3000, 510, 131, -100, 9845, 43, 52867 };
            Console.WriteLine(calculator.Execute());
        }
    }
}