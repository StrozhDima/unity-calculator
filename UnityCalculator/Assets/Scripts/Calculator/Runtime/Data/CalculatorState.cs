using System.Collections.Generic;

namespace UnityCalculator.Calculator
{
    public readonly struct CalculatorState
    {
        public readonly string CurrentExpression;
        public readonly IReadOnlyList<HistoryEntry> History;

        public CalculatorState(string currentExpression, IReadOnlyList<HistoryEntry> history)
        {
            CurrentExpression = currentExpression;
            History = history;
        }
    }
}
