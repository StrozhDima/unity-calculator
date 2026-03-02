using System.Collections.Generic;

namespace UnityCalculator.Calculator
{
    public sealed class StubCalculatorProgress : ICalculatorProgress
    {
        public CalculatorState StateToLoad { get; set; } =
            new CalculatorState(string.Empty, new List<HistoryEntry>());

        public int SaveCallCount { get; private set; }
        public CalculatorState LastSavedState { get; private set; }

        public void Save(CalculatorState state)
        {
            SaveCallCount++;
            LastSavedState = state;
        }

        public CalculatorState Load() => StateToLoad;
    }
}
