using System;
using System.Collections.Generic;
using UnityCalculator.Persistence;

namespace UnityCalculator.Calculator
{
    public sealed class PlayerPrefsCalculatorPersistence : ICalculatorProgress
    {
        private const string ExpressionKey = "calc_expression";
        private const string HistoryKey = "calc_history";

        private readonly IPersistenceProvider _provider;
        private readonly ISerializer _serializer;

        public PlayerPrefsCalculatorPersistence(IPersistenceProvider provider, ISerializer serializer)
        {
            _provider = provider;
            _serializer = serializer;
        }

        public void Save(CalculatorState state)
        {
            _provider.Save(ExpressionKey, state.CurrentExpression);

            var history = new SerializableList<SerializableHistoryEntry>();

            foreach (var entry in state.History)
            {
                history.Items.Add(new SerializableHistoryEntry
                {
                    Expression = entry.Expression,
                    ResultDisplay = entry.ResultDisplay,
                    Type = entry.Type
                });
            }

            _provider.Save(HistoryKey, _serializer.Serialize(history));
        }

        public CalculatorState Load()
        {
            var expression = _provider.Load(ExpressionKey, string.Empty);
            var historyJson = _provider.Load(HistoryKey, string.Empty);
            var history = new List<HistoryEntry>();

            if (!string.IsNullOrEmpty(historyJson))
            {
                var serializable = _serializer.Deserialize<SerializableList<SerializableHistoryEntry>>(historyJson);

                if (serializable?.Items != null)
                {
                    foreach (var entry in serializable.Items)
                    {
                        history.Add(new HistoryEntry(entry.Expression, entry.ResultDisplay, entry.Type));
                    }
                }
            }

            return new CalculatorState(expression, history);
        }

        [Serializable]
        private class SerializableHistoryEntry
        {
            public string Expression;
            public string ResultDisplay;
            public CalculationResultType Type;
        }
    }
}
