using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCalculator.MessageBox
{
    public sealed class MessageBoxView : MonoBehaviour, IMessageBoxView
    {
        [SerializeField]
        private Button _confirmButton;

        private IObservable<Unit> _confirmed;

        IObservable<Unit> IMessageBoxView.Confirmed => _confirmed;

        void IMessageBoxView.Initialize() => _confirmed = _confirmButton.OnClickAsObservable().Share();

        void IMessageBoxView.Show() => gameObject.SetActive(true);

        void IMessageBoxView.Hide() => gameObject.SetActive(false);
    }
}