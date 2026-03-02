using UnityCalculator.Calculator;
using UnityCalculator.MessageBox;
using UnityEngine;
using Zenject;

namespace UnityCalculator.Core
{
    public sealed class EntryPoint : MonoBehaviour
    {
        private ICalculatorPresenter _calculatorPresenter;
        private IMessageBoxPresenter _messageBoxPresenter;

        [Inject]
        public void Construct(
            ICalculatorPresenter calculatorPresenter,
            IMessageBoxPresenter messageBoxPresenter)
        {
            _calculatorPresenter = calculatorPresenter;
            _messageBoxPresenter = messageBoxPresenter;
        }

        private void Awake()
        {
            _calculatorPresenter.Initialize();
            _messageBoxPresenter.Initialize();
            _messageBoxPresenter.Hide();
        }
    }
}