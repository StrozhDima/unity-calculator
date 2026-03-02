namespace UnityCalculator.Arithmetic
{
    public interface IArithmeticOperation
    {
        OperationResult Execute(long left, long right);
    }
}
