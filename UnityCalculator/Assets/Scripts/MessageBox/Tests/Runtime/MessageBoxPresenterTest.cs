using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

namespace UnityCalculator.MessageBox
{
    [TestFixture]
    public class MessageBoxPresenterTest
    {
        [Test]
        public void Initialize_WhenCalled_InitializesView()
        {
            var spyView = new TestMessageBoxView();
            IMessageBoxPresenter sut = new MessageBoxPresenter(spyView);
            sut.Initialize();

            Assert.That(spyView.InitializeCallCount, Is.EqualTo(1));
        }

        [Test]
        public async Task ShowAsync_WhenCalled_ShowsView()
        {
            var spyView = new TestMessageBoxView();
            var sut = CreatePresenter(spyView);
            var task = sut.ShowAndWaitForHideAsync(CancellationToken.None).AsTask();

            Assert.That(spyView.ShowCallCount, Is.EqualTo(1));

            spyView.SimulateConfirm();
            await task;
        }

        [Test]
        public async Task ShowAsync_WhenConfirmed_HidesView()
        {
            var spyView = new TestMessageBoxView();
            var sut = CreatePresenter(spyView);
            var task = sut.ShowAndWaitForHideAsync(CancellationToken.None).AsTask();
            spyView.SimulateConfirm();
            await task;

            Assert.That(spyView.IsVisible, Is.False);
        }

        [Test]
        public async Task ShowAsync_WhenCancelled_HidesView()
        {
            var spyView = new TestMessageBoxView();
            var sut = CreatePresenter(spyView);
            var tokenSource = new CancellationTokenSource();
            var task = sut.ShowAndWaitForHideAsync(tokenSource.Token).AsTask();
            tokenSource.Cancel();

            try
            {
                await task;
                Assert.Fail("Expected OperationCanceledException was not thrown");
            }
            catch (OperationCanceledException)
            {
            }

            Assert.That(spyView.IsVisible, Is.False);
        }

        private static IMessageBoxPresenter CreatePresenter(IMessageBoxView view)
        {
            view ??= new TestMessageBoxView();
            IMessageBoxPresenter presenter = new MessageBoxPresenter(view);
            presenter.Initialize();
            return presenter;
        }
    }
}