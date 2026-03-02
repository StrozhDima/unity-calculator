using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityCalculator.MessageBox
{
    public interface IMessageBoxPresenter
    {
        void Initialize();
        UniTask ShowAndWaitForHideAsync(CancellationToken cancellationToken);
        void Hide();
    }
}
