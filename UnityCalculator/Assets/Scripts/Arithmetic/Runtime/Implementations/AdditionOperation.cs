namespace UnityCalculator.Arithmetic
{
    public sealed class AdditionOperation : IArithmeticOperation
    {
        public OperationResult Execute(long left, long right) => new OperationResult(left + right);
    }
}
