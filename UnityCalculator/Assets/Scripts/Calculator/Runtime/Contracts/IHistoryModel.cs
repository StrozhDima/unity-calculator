using System.Collections.Generic;
using UniRx;

namespace UnityCalculator.Calculator
{
    public interface IHistoryModel
    {
        IReadOnlyReactiveProperty<IReadOnlyList<HistoryEntry>> Entries { get; }
        void Add(HistoryEntry entry);
        void SetAll(IReadOnlyList<HistoryEntry> entries);
    }
}
