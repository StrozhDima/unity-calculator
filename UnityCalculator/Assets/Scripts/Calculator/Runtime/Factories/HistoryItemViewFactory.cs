using Zenject;

namespace UnityCalculator.Calculator
{
    public sealed class HistoryItemViewFactory : IHistoryItemViewFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly HistoryItemView _prefab;

        public HistoryItemViewFactory(IInstantiator instantiator, HistoryItemView prefab)
        {
            _instantiator = instantiator;
            _prefab = prefab;
        }

        public HistoryItemView Create(HistoryEntry entry)
        {
            var item = _instantiator.InstantiatePrefabForComponent<HistoryItemView>(_prefab);
            item.Setup(entry);
            return item;
        }
    }
}
