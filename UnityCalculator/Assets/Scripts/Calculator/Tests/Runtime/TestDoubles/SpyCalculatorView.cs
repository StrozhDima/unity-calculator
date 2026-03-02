using System;
using System.Collections.Generic;
using UniRx;

namespace UnityCalculator.Calculator
{
    public sealed class SpyCalculatorView : ICalculatorView
    {
        public readonly Subject<Unit> ResultClickedSubject = new();
        public readonly Subject<string> InputTextChangedSubject = new();

        public IObservable<Unit> ResultClicked => ResultClickedSubject;
        public IObservable<string> InputTextChanged => InputTextChangedSubject;
        public string InputText { get; set; } = string.Empty;

        public int InitializeCallCount { get; private set; }

        public void Initialize() => InitializeCallCount++;

        public void SetHistoryItems(IReadOnlyList<HistoryItemView> items) { }
    }
}
