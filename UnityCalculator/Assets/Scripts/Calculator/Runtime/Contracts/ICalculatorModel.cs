namespace UnityCalculator.Calculator
{
    public interface ICalculatorModel
    {
        HistoryEntry Calculate(string expression);
    }
}
