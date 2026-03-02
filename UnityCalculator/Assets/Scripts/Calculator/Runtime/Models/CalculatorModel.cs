using UnityCalculator.Arithmetic;
using UnityCalculator.Parsing;

namespace UnityCalculator.Calculator
{
    public sealed class CalculatorModel : ICalculatorModel
    {
        private const string ErrorDisplay = "ERROR";

        private readonly IExpressionValidator _validator;
        private readonly IExpressionParser _parser;
        private readonly IArithmeticOperation _operation;

        public CalculatorModel(
            IExpressionValidator validator,
            IExpressionParser parser,
            IArithmeticOperation operation)
        {
            _validator = validator;
            _parser = parser;
            _operation = operation;
        }

        public HistoryEntry Calculate(string expression)
        {
            if (!_validator.IsValid(expression) || !_parser.TryParse(expression, out var parsed))
            {
                return new HistoryEntry(expression, ErrorDisplay, CalculationResultType.Error);
            }

            var result = _operation.Execute(parsed.Left, parsed.Right);
            return new HistoryEntry(expression, result.Value.ToString(), CalculationResultType.Success);
        }
    }
}