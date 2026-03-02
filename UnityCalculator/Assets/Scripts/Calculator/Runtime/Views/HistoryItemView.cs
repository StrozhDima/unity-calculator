using TMPro;
using UnityEngine;

namespace UnityCalculator.Calculator
{
    public sealed class HistoryItemView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private HistoryItemViewConfig _config;

        public void Setup(HistoryEntry entry)
        {
            _text.text = $"{entry.Expression}={entry.ResultDisplay}";
            _text.color = entry.Type == CalculationResultType.Success
                ? _config.SuccessColor
                : _config.ErrorColor;
        }
    }
}
