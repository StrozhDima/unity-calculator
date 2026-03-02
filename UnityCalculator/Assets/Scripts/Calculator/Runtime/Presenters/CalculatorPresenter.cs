using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityCalculator.MessageBox;

namespace UnityCalculator.Calculator
{
    public sealed class CalculatorPresenter : ICalculatorPresenter, IDisposable
    {
        private readonly ICalculatorView _view;
        private readonly ICalculatorModel _model;
        private readonly IHistoryModel _historyModel;
        private readonly ICalculatorProgress _progress;
        private readonly IMessageBoxPresenter _messageBoxPresenter;
        private readonly IHistoryItemViewFactory _historyItemViewFactory;
        private readonly List<HistoryItemView> _historyItems;
        private readonly CompositeDisposable _disposables;
        private readonly CancellationTokenSource _tokenSource;

        public CalculatorPresenter(
            ICalculatorView view,
            ICalculatorModel model,
            IHistoryModel historyModel,
            ICalculatorProgress progress,
            IMessageBoxPresenter messageBoxPresenter,
            IHistoryItemViewFactory historyItemViewFactory)
        {
            _view = view;
            _model = model;
            _historyModel = historyModel;
            _progress = progress;
            _messageBoxPresenter = messageBoxPresenter;
            _historyItemViewFactory = historyItemViewFactory;
            _historyItems = new List<HistoryItemView>();
            _disposables = new CompositeDisposable();
            _tokenSource = new CancellationTokenSource();
        }

        void ICalculatorPresenter.Initialize()
        {
            _view.Initialize();

            var state = _progress.Load();
            _historyModel.SetAll(state.History);
            _view.InputText = state.CurrentExpression;

            _historyModel.Entries
                .Subscribe(entries => _view.SetHistoryItems(SyncItems(entries)))
                .AddTo(_disposables);

            _view.InputTextChanged
                .Subscribe(text => _progress.Save(new CalculatorState(text, _historyModel.Entries.Value)))
                .AddTo(_disposables);

            _view.ResultClicked
                .Subscribe(_ => OnResultClickedAsync(_tokenSource.Token).Forget())
                .AddTo(_disposables);
        }

        private IReadOnlyList<HistoryItemView> SyncItems(IReadOnlyList<HistoryEntry> entries)
        {
            var delta = entries.Count - _historyItems.Count;

            for (var i = delta - 1; i >= 0; i--)
            {
                _historyItems.Insert(0, _historyItemViewFactory.Create(entries[i]));
            }

            return _historyItems;
        }

        private async UniTaskVoid OnResultClickedAsync(CancellationToken cancellationToken)
        {
            var expression = _view.InputText;
            var entry = _model.Calculate(expression);
            _historyModel.Add(entry);
            _view.InputText = string.Empty;
            _progress.Save(new CalculatorState(string.Empty, _historyModel.Entries.Value));

            if (entry.Type is CalculationResultType.Error)
            {
                await _messageBoxPresenter.ShowAndWaitForHideAsync(cancellationToken);
                _view.InputText = expression;
                _progress.Save(new CalculatorState(expression, _historyModel.Entries.Value));
            }
        }

        void IDisposable.Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _disposables.Dispose();
        }
    }
}