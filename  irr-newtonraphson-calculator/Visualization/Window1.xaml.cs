using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Zainco.NewtonRaphson.IRRCalculator.Domain;
using System.Collections.Generic;

namespace Simulation
{
    
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ObservableDataSource<Point> irrResults = null;

        public Window1()
        {
            InitializeComponent();
        }

        private void Simulation()
        {
            var calculator = NewtonRaphsonIRRCalculator.Instance;
            //TODO Move to GUI entry
            calculator.CashFlows = new List<double> { -6500,1500,500,140,2,-580,456,12,150,350,-2250,105,-25000, 510, 131, -100, 98,140,101,50,45, 43, 5067};
            
            Console.WriteLine(calculator.Execute());

            var results = calculator.Results;
            foreach (var result in results)
            {
                double x = result.Key;
                double y1 = result.Value;

                Point p1 = new Point(x, y1);

                irrResults.AppendAsync(Dispatcher, p1);
                Thread.Sleep(20);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            irrResults = new ObservableDataSource<Point>();
            irrResults.SetXYMapping(p => p);

            plotter.AddLineGraph(irrResults, 1, "IRR Convergence");

            Thread simThread = new Thread(new ThreadStart(Simulation));
            simThread.IsBackground = true;
            simThread.Start();
        }
    }
}
