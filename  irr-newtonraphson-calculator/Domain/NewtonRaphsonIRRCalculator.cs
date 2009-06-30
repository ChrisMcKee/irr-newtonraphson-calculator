// NewtonRaphsonIRRCalculator.cs - Calculate the Internal rate of return for a given set of cashflows.
// Zainco Ltd
// Author: Joseph A. Nyirenda <joseph.nyirenda@gmail.com>
//             Mai Kalange<code5p@yahoo.co.uk>
// Copyright (c) 2008 Joseph A. Nyirenda, Mai Kalange, Zainco Ltd
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of version 2 of the GNU General Public
// License as published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public
// License along with this program; if not, write to the
// Free Software Foundation, Inc., 59 Temple Place - Suite 330,
// Boston, MA 02111-1307, USA.


using System;
using Zainco.NewtonRaphson.IRRCalculator.Exceptions;

namespace Zainco.NewtonRaphson.IRRCalculator.Domain
{
    public class NewtonRaphsonIRRCalculator : ICalculator
    {
        //private readonly double[] _cashFlows;
        internal NewtonRaphsonIRRCalculator() { }
        private int _numberOfIterations;
        private double _result;

        public double[] CashFlows { get; set; }

        public static ICalculator Instance
        {
            get
            { return new NewtonRaphsonIRRCalculator(); }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is valid cash flows.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is valid cash flows; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidCashFlows
        {
            //Cash flows for the first period must be positive
            //There should be at least two cash flow periods         
            get
            {
                const int MIN_NO_CASH_FLOW_PERIODS = 2;

                if (CashFlows.Length < MIN_NO_CASH_FLOW_PERIODS || (CashFlows[0] > 0))
                {
                    throw new ArgumentOutOfRangeException(
                        "Cash flow for the first period  must be negative and there should");
                }
                return true;
            }
        }

        /// <summary>
        /// Gets the initial guess.
        /// </summary>
        /// <value>The initial guess.</value>
        private double InitialGuess
        {
            get
            {
                double initialGuess = -1 * (1 + (CashFlows[1] / CashFlows[0]));
                return initialGuess;
            }
        }

        #region ICalculator Members

        public double Execute()
        {
            if (IsValidCashFlows)
            {
                DoNewtonRapshonCalculation(InitialGuess);

                if (_result > 1)
                    throw new IRRCalculationException(
                        "Failed to calculate the IRR for the cash flow series. Please provide a valid cash flow sequence");
            }
            return _result;
        }

        private void RaiseEvent()
        {
            if (OnDataPointGenerated != null)
                OnDataPointGenerated(this, new IRRCalculatorEventArgs(_result, _numberOfIterations));
        }

        public event OnDataPointGeneratedHandler OnDataPointGenerated;

        #endregion

        /// <summary>
        /// Does the newton rapshon calculation.
        /// </summary>
        /// <param name="estimatedReturn">The estimated return.</param>
        /// <returns></returns>
        private void DoNewtonRapshonCalculation(double estimatedReturn)
        {
            _numberOfIterations++;
            _result = estimatedReturn - SumOfIRRPolynomial(estimatedReturn)/IRRDerivativeSum(estimatedReturn);

            while (!HasConverged(_result) && ConfigurationHelper.MaxIterations != _numberOfIterations)
            {
                RaiseEvent();
                DoNewtonRapshonCalculation(_result);
            }
        }


        /// <summary>
        /// Sums the of IRR polynomial.
        /// </summary>
        /// <param name="estimatedReturnRate">The estimated return rate.</param>
        /// <returns></returns>
        private double SumOfIRRPolynomial(double estimatedReturnRate)
        {
            double sumOfPolynomial = 0;
            if (IsValidIterationBounds(estimatedReturnRate))
                for (int j = 0; j < CashFlows.Length; j++)
                {
                    sumOfPolynomial += CashFlows[j] / (Math.Pow((1 + estimatedReturnRate), j));
                }
            return sumOfPolynomial;
        }

        /// <summary>
        /// Determines whether the specified estimated return rate has converged.
        /// </summary>
        /// <param name="estimatedReturnRate">The estimated return rate.</param>
        /// <returns>
        /// 	<c>true</c> if the specified estimated return rate has converged; otherwise, <c>false</c>.
        /// </returns>
        private bool HasConverged(double estimatedReturnRate)
        {
            //Check that the calculated value makes the IRR polynomial zero.
            bool isWithinTolerance = Math.Abs(SumOfIRRPolynomial(estimatedReturnRate)) <= ConfigurationHelper.Tolerance;
            return (isWithinTolerance) ? true : false;
        }

        /// <summary>
        /// IRRs the derivative sum.
        /// </summary>
        /// <param name="estimatedReturnRate">The estimated return rate.</param>
        /// <returns></returns>
        private double IRRDerivativeSum(double estimatedReturnRate)
        {
            double sumOfDerivative = 0;
            if (IsValidIterationBounds(estimatedReturnRate))
                for (int i = 1; i < CashFlows.Length; i++)
                {
                    sumOfDerivative += CashFlows[i] * (i) / Math.Pow((1 + estimatedReturnRate), i);
                }
            return sumOfDerivative*-1;
        }

        /// <summary>
        /// Determines whether [is valid iteration bounds] [the specified estimated return rate].
        /// </summary>
        /// <param name="estimatedReturnRate">The estimated return rate.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid iteration bounds] [the specified estimated return rate]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsValidIterationBounds(double estimatedReturnRate)
        {
            return estimatedReturnRate != -1 && (estimatedReturnRate < int.MaxValue) &&
                   (estimatedReturnRate > int.MinValue);
        }
    }
}