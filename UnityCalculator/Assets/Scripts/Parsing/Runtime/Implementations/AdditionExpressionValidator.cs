using System.Text.RegularExpressions;

namespace UnityCalculator.Parsing
{
    public sealed class AdditionExpressionValidator : IExpressionValidator
    {
        private static readonly Regex ValidationRegex = new Regex(@"^\d+\+\d+$", RegexOptions.Compiled);

        public bool IsValid(string expression) => ValidationRegex.IsMatch(expression);
    }
}
