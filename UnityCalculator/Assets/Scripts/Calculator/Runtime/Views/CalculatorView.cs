using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCalculator.Calculator
{
    public sealed class CalculatorView : MonoBehaviour, ICalculatorView
    {
        [SerializeField]
        private TMP_InputField _inputField;
        [SerializeField]
        private Button _resultButton;
        [SerializeField]
        private Transform _historyContainer;
        [SerializeField]
        private GameObject _historyScrollView;
        [SerializeField]
        private LayoutElement _historyLayoutElement;
        [SerializeField]
        private CalculatorViewConfig _config;

        private IObservable<Unit> _resultClicked;
        private IObservable<string> _inputTextChanged;

        IObservable<Unit> ICalculatorView.ResultClicked => _resultClicked;
        IObservable<string> ICalculatorView.InputTextChanged => _inputTextChanged;

        string ICalculatorView.InputText
        {
            get => _inputField.text;
            set => _inputField.text = value;
        }

        void ICalculatorView.Initialize()
        {
            _resultClicked = _resultButton.OnClickAsObservable().Share();
            _inputTextChanged = Observable.FromEvent<UnityEngine.Events.UnityAction<string>, string>(
                action => new UnityEngine.Events.UnityAction<string>(action),
                action => _inputField.onValueChanged.AddListener(action),
                action => _inputField.onValueChanged.RemoveListener(action)).Share();
        }

        void ICalculatorView.SetHistoryItems(IReadOnlyList<HistoryItemView> items)
        {
            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].transform.parent != _historyContainer)
                {
                    items[i].transform.SetParent(_historyContainer, false);
                }

                items[i].transform.SetSiblingIndex(i);
            }

            UpdateScrollView(items);
        }

        private void UpdateScrollView(IReadOnlyList<HistoryItemView> items)
        {
            _historyScrollView.SetActive(items.Count > 0);

            if (items.Count > 0)
            {
                var contentRect = (RectTransform)_historyContainer;
                LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
                _historyLayoutElement.preferredHeight = Mathf.Min(contentRect.rect.height, _config.HistoryMaxHeight);
            }
        }
    }
}