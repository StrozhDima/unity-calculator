using UnityCalculator.Arithmetic;
using UnityCalculator.Calculator;
using UnityCalculator.MessageBox;
using UnityCalculator.Parsing;
using UnityCalculator.Persistence;
using UnityEngine;
using Zenject;

namespace UnityCalculator.Core
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private EntryPoint _entryPoint;
        [SerializeField]
        private MessageBoxView _messageBoxView;
        [SerializeField]
        private CalculatorView _calculatorView;
        [SerializeField]
        private HistoryItemView _historyItemPrefab;

        public override void InstallBindings()
        {
            BindFactories();
            BindMessageBox();
            BindCalculator();
            Container.QueueForInject(_entryPoint);
        }

        private void BindFactories() =>
            Container.Bind<IHistoryItemViewFactory>().To<HistoryItemViewFactory>().AsSingle().WithArguments(_historyItemPrefab);

        private void BindMessageBox() =>
            Container.BindInterfacesTo<MessageBoxPresenter>().AsSingle().WithArguments(_messageBoxView);

        private void BindCalculator()
        {
            Container.BindInterfacesTo<HistoryModel>().AsSingle();
            Container.BindInterfacesTo<CalculatorModel>().AsSingle();
            Container.BindInterfacesTo<CalculatorPresenter>().AsSingle().WithArguments(_calculatorView);
            Container.BindInterfacesTo<PlayerPrefsCalculatorPersistence>().AsSingle();
            Container.BindInterfacesTo<AdditionExpressionValidator>().AsSingle();
            Container.BindInterfacesTo<AdditionExpressionParser>().AsSingle();
            Container.BindInterfacesTo<AdditionOperation>().AsSingle();
            Container.BindInterfacesTo<PlayerPrefsPersistenceProvider>().AsSingle();
            Container.BindInterfacesTo<JsonUtilitySerializer>().AsSingle();
        }
    }
}