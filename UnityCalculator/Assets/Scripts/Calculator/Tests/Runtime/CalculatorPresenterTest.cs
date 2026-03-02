using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UniRx;
using UnityEngine.TestTools;
using UnityCalculator.Arithmetic;
using UnityCalculator.MessageBox;
using UnityCalculator.Parsing;

namespace UnityCalculator.Calculator
{
    [TestFixture]
    public class CalculatorPresenterTest
    {
        [Test]
        public void Initialize_WithSavedExpression_RestoresViewInput()
        {
            var spyView = new SpyCalculatorView();
            var stubProgress = new StubCalculatorProgress
            {
                StateToLoad = new CalculatorState("5+3", new List<HistoryEntry>())
            };
            var sut = CreatePresenter(view: spyView, progress: stubProgress);

            sut.Initialize();

            Assert.That(spyView.InputText, Is.EqualTo("5+3"));
        }

        [Test]
        public void OnResultClicked_ValidExpression_AddsSuccessEntryToHistory()
        {
            var spyView = new SpyCalculatorView();
            IHistoryModel historyModel = new HistoryModel();
            var sut = CreatePresenter(view: spyView, historyModel: historyModel);
            sut.Initialize();
            spyView.InputText = "5+3";

            spyView.ResultClickedSubject.OnNext(Unit.Default);

            Assert.That(historyModel.Entries.Value, Has.Count.EqualTo(1));
            Assert.That(historyModel.Entries.Value[0].Type, Is.EqualTo(CalculationResultType.Success));
        }

        [Test]
        public void OnResultClicked_ValidExpression_ClearsInput()
        {
            var spyView = new SpyCalculatorView();
            var sut = CreatePresenter(view: spyView);
            sut.Initialize();
            spyView.InputText = "5+3";

            spyView.ResultClickedSubject.OnNext(Unit.Default);

            Assert.That(spyView.InputText, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OnResultClicked_InvalidExpression_AddsErrorEntryToHistory()
        {
            var spyView = new SpyCalculatorView();
            IHistoryModel historyModel = new HistoryModel();
            var sut = CreatePresenter(view: spyView, historyModel: historyModel);
            sut.Initialize();
            spyView.InputText = "abc";

            spyView.ResultClickedSubject.OnNext(Unit.Default);

            Assert.That(historyModel.Entries.Value[0].Type, Is.EqualTo(CalculationResultType.Error));
        }

        [Test]
        public void OnResultClicked_InvalidExpression_ShowsMessageBox()
        {
            var spyView = new SpyCalculatorView();
            var spyMessageBoxView = new SpyMessageBoxView();
            var messageBoxPresenter = new MessageBoxPresenter(spyMessageBoxView);
            var sut = CreatePresenter(view: spyView, messageBoxPresenter: messageBoxPresenter);
            sut.Initialize();
            spyView.InputText = "abc";

            spyView.ResultClickedSubject.OnNext(Unit.Default);

            Assert.That(spyMessageBoxView.ShowCallCount, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator OnResultClicked_AfterErrorDialogClosed_RestoresExpression()
        {
            var spyView = new SpyCalculatorView();
            var spyMessageBoxView = new SpyMessageBoxView();
            var messageBoxPresenter = new MessageBoxPresenter(spyMessageBoxView);
            var sut = CreatePresenter(view: spyView, messageBoxPresenter: messageBoxPresenter);
            sut.Initialize();
            spyView.InputText = "abc";

            spyView.ResultClickedSubject.OnNext(Unit.Default);
            spyMessageBoxView.ConfirmedSubject.OnNext(Unit.Default);

            yield return null;

            Assert.That(spyView.InputText, Is.EqualTo("abc"));
        }

        private ICalculatorPresenter CreatePresenter(
            ICalculatorView view = null,
            ICalculatorModel calculatorModel = null,
            IHistoryModel historyModel = null,
            ICalculatorProgress progress = null,
            IMessageBoxPresenter messageBoxPresenter = null,
            IHistoryItemViewFactory historyItemViewFactory = null) => new CalculatorPresenter(
            view ?? new SpyCalculatorView(),
            calculatorModel ?? new CalculatorModel(new AdditionExpressionValidator(), new AdditionExpressionParser(), new AdditionOperation()),
            historyModel ?? new HistoryModel(),
            progress ?? new StubCalculatorProgress(),
            messageBoxPresenter ?? new MessageBoxPresenter(new SpyMessageBoxView()),
            historyItemViewFactory ?? new StubHistoryItemViewFactory());
    }
}