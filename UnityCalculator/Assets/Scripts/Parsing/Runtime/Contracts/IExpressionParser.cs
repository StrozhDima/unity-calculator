namespace UnityCalculator.Parsing
{
    public interface IExpressionParser
    {
        bool TryParse(string expression, out ParsedExpression parsed);
    }
}
