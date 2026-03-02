using UnityEngine;

namespace UnityCalculator.Calculator
{
    [CreateAssetMenu(fileName = "HistoryItemViewConfig", menuName = "UnityCalculator/Calculator/History Item Config")]
    public sealed class HistoryItemViewConfig : ScriptableObject
    {
        [SerializeField]
        private Color _successColor = Color.white;

        [SerializeField]
        private Color _errorColor = Color.red;

        public Color SuccessColor => _successColor;
        public Color ErrorColor => _errorColor;
    }
}
