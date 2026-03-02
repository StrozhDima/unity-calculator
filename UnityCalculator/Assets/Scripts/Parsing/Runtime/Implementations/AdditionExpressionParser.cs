namespace UnityCalculator.Parsing
{
    public sealed class AdditionExpressionParser : IExpressionParser
    {
        bool IExpressionParser.TryParse(string expression, out ParsedExpression parsed)
        {
            try
            {
                var parts = expression.Split('+');
                var left = long.Parse(parts[0]);
                var right = long.Parse(parts[1]);
                parsed = new ParsedExpression(left, right);
            }
            catch
            {
                parsed = default;
                return false;
            }

            return true;
        }
    }
}