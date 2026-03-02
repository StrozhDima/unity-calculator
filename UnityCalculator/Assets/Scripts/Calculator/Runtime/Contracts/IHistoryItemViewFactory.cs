namespace UnityCalculator.Calculator
{
    public interface IHistoryItemViewFactory
    {
        HistoryItemView Create(HistoryEntry entry);
    }
}
