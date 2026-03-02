using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace UnityCalculator.MessageBox
{
    public sealed class MessageBoxPresenter : IMessageBoxPresenter
    {
        private readonly IMessageBoxView _view;

        public MessageBoxPresenter(IMessageBoxView view) => _view = view;

        void IMessageBoxPresenter.Initialize() => _view.Initialize();

        async UniTask IMessageBoxPresenter.ShowAndWaitForHideAsync(CancellationToken cancellationToken)
        {
            var completionSource = new UniTaskCompletionSource();
            _view.Show();

            using (_view.Confirmed.Subscribe(_ => completionSource.TrySetResult()))
            {
                await completionSource.Task.AttachExternalCancellation(cancellationToken);
            }

            _view.Hide();
        }

        void IMessageBoxPresenter.Hide() => _view.Hide();
    }
}