using NUnit.Framework;

namespace UnityCalculator.Arithmetic
{
    [TestFixture]
    public class AdditionOperationTest
    {
        [TestCase(54L, 21L, 75L)]
        [TestCase(45L, 0L, 45L)]
        [TestCase(0L, 0L, 0L)]
        [TestCase(0L, 99L, 99L)]
        public void Execute_WithValidOperands_ReturnsSum(long left, long right, long expected)
        {
            var sut = CreateAdditionOperation();

            var actual = sut.Execute(left, right);

            Assert.That(actual.Value, Is.EqualTo(expected));
        }

        private AdditionOperation CreateAdditionOperation()
        {
            return new AdditionOperation();
        }
    }
}
