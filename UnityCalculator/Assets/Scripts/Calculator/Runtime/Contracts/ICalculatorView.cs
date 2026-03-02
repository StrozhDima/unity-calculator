using System;
using System.Collections.Generic;
using UniRx;

namespace UnityCalculator.Calculator
{
    public interface ICalculatorView
    {
        IObservable<Unit> ResultClicked { get; }
        IObservable<string> InputTextChanged { get; }

        string InputText { get; set; }
        void Initialize();
        void SetHistoryItems(IReadOnlyList<HistoryItemView> items);
    }
}