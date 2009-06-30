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
            calculator.CashFlows = new List<double> { -3000, 510, 131, -100, 9845, 43, 5267 };
            calculator.OnDataPointGenerated += new EventHandler<IRRCalculatorEventArgs>(calculator_OnDataPointGenerated);
            Console.WriteLine(calculator.Execute());
        }

        static void calculator_OnDataPointGenerated(object sender, IRRCalculatorEventArgs e)
        {
            //Plot results here
        }
    }
}