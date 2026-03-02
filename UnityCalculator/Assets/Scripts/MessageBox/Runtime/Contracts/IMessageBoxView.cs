using System;
using UniRx;

namespace UnityCalculator.MessageBox
{
    public interface IMessageBoxView
    {
        IObservable<Unit> Confirmed { get; }

        void Initialize();
        void Show();
        void Hide();
    }
}
