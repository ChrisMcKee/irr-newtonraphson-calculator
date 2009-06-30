using System;
using Zainco.NewtonRaphson.IRRCalculator.Domain;

namespace Zainco.NewtonRaphson.IRRCalculator
{
    internal class Program
    {
        public static void Main()
        {
            var cashFlows = new double[7] {-3000, 510, 131, -100, 9845, 43, 52867};
            var calculator = new NewtonRaphsonIRRCalculator(cashFlows);

            Console.WriteLine(calculator.Execute());
        }
    }
}