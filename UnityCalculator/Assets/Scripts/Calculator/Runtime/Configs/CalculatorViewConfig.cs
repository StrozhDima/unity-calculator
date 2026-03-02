using UnityEngine;

namespace UnityCalculator.Calculator
{
    [CreateAssetMenu(fileName = "CalculatorViewConfig", menuName = "UnityCalculator/Calculator/View Config")]
    public sealed class CalculatorViewConfig : ScriptableObject
    {
        [SerializeField]
        private float _historyMaxHeight = 400f;

        public float HistoryMaxHeight => _historyMaxHeight;
    }
}
