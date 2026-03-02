namespace UnityCalculator.Calculator
{
    public readonly struct HistoryEntry
    {
        public readonly string Expression;
        public readonly string ResultDisplay;
        public readonly CalculationResultType Type;

        public HistoryEntry(string expression, string resultDisplay, CalculationResultType type)
        {
            Expression = expression;
            ResultDisplay = resultDisplay;
            Type = type;
        }
    }
}
