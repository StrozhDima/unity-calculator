using System;
using System.Collections.Generic;
using UniRx;

namespace UnityCalculator.Calculator
{
    public sealed class HistoryModel : IHistoryModel, IDisposable
    {
        private readonly ReactiveProperty<IReadOnlyList<HistoryEntry>> _entries = new(new List<HistoryEntry>());

        IReadOnlyReactiveProperty<IReadOnlyList<HistoryEntry>> IHistoryModel.Entries => _entries;

        void IHistoryModel.Add(HistoryEntry entry)
        {
            var newList = new List<HistoryEntry> { entry };
            newList.AddRange(_entries.Value);
            _entries.Value = newList;
        }

        void IHistoryModel.SetAll(IReadOnlyList<HistoryEntry> entries) => _entries.Value = entries;

        void IDisposable.Dispose() => _entries.Dispose();
    }
}