namespace Zainco.NewtonRaphson.IRRCalculator.Domain
{
    public delegate void OnDataPointGeneratedHandler(object sender, IRRCalculatorEventArgs args);
    public interface ICalculator
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        double Execute();

        event OnDataPointGeneratedHandler OnDataPointGenerated;
    }
}