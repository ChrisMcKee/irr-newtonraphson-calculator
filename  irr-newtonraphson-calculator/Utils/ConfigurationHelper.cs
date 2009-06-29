// ConfigurationHelper.cs - Calculate the Internal rate of return for a given set of cashflows.
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

using System.Configuration;

namespace Zainco.NewtonRaphson.IRRCalculator
{
    public static class ConfigurationHelper
    {
        public static int MaxIterations
        {
            get { return int.Parse(ConfigurationManager.AppSettings["max.iterations"]); }
        }

        public static double Tolerance
        {
            get { return double.Parse(ConfigurationManager.AppSettings["tolerance"]); }
        }
    }
}