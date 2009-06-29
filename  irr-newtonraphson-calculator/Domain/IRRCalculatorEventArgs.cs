using System;

namespace Zainco.NewtonRaphson.IRRCalculator.Domain
{
    public class IRRCalculatorEventArgs : EventArgs
    {
        private double _iterationCount;
        private double _result;

        public IRRCalculatorEventArgs(double result, double iterationCount)
        {
            _result = result;
            _iterationCount = iterationCount;
        }

        public double Result
        {
            get { return _result*100; }
            set { _result = value; }
        }

        public double IterationCount
        {
            get { return _iterationCount; }
            set { _iterationCount = value; }
        }
    }
}