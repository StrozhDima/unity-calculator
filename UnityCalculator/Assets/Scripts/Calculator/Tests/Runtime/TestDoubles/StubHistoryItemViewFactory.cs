namespace UnityCalculator.Calculator
{
    public sealed class StubHistoryItemViewFactory : IHistoryItemViewFactory
    {
        public HistoryItemView Create(HistoryEntry entry) => null;
    }
}
