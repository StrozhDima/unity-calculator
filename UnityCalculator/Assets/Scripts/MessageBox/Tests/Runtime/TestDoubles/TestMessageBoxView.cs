using System;
using UniRx;

namespace UnityCalculator.MessageBox
{
    public sealed class TestMessageBoxView : IMessageBoxView
    {
        private readonly Subject<Unit> _onConfirmed = new();

        IObservable<Unit> IMessageBoxView.Confirmed => _onConfirmed;

        public bool IsVisible { get; private set; }
        public int ShowCallCount { get; private set; }
        public int HideCallCount { get; private set; }
        public int InitializeCallCount { get; private set; }

        void IMessageBoxView.Initialize() => InitializeCallCount++;

        void IMessageBoxView.Show()
        {
            IsVisible = true;
            ShowCallCount++;
        }

        void IMessageBoxView.Hide()
        {
            IsVisible = false;
            HideCallCount++;
        }

        public void SimulateConfirm() => _onConfirmed.OnNext(Unit.Default);
    }
}
