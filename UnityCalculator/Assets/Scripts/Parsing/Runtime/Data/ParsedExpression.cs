namespace UnityCalculator.Parsing
{
    public readonly struct ParsedExpression
    {
        public readonly long Left;
        public readonly long Right;

        public ParsedExpression(long left, long right)
        {
            Left = left;
            Right = right;
        }
    }
}
