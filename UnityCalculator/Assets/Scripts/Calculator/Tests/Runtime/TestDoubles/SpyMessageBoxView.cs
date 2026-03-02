using System;
using UniRx;
using UnityCalculator.MessageBox;

namespace UnityCalculator.Calculator
{
    public sealed class SpyMessageBoxView : IMessageBoxView
    {
        public readonly Subject<Unit> ConfirmedSubject = new Subject<Unit>();

        public IObservable<Unit> Confirmed => ConfirmedSubject;
        public int ShowCallCount { get; private set; }
        public int HideCallCount { get; private set; }

        void IMessageBoxView.Initialize() { }

        void IMessageBoxView.Show() => ShowCallCount++;

        void IMessageBoxView.Hide() => HideCallCount++;
    }
}
