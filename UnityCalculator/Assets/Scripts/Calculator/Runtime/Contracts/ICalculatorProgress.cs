namespace UnityCalculator.Calculator
{
    public interface ICalculatorProgress
    {
        void Save(CalculatorState state);
        CalculatorState Load();
    }
}
