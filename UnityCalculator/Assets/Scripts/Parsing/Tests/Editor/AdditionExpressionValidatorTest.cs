using NUnit.Framework;

namespace UnityCalculator.Parsing
{
    [TestFixture]
    public class AdditionExpressionValidatorTest
    {
        [TestCase("54+21")]
        [TestCase("45+00")]
        [TestCase("0+0")]
        [TestCase("999+1")]
        public void IsValid_WithValidExpression_ReturnsTrue(string expression)
        {
            var sut = CreateValidator();

            var actual = sut.IsValid(expression);

            Assert.That(actual, Is.True);
        }

        [TestCase("98.12+48.1")]
        [TestCase("45+-88")]
        [TestCase("5/5")]
        [TestCase("")]
        [TestCase("5+")]
        [TestCase("+5")]
        [TestCase("5-5")]
        public void IsValid_WithInvalidExpression_ReturnsFalse(string expression)
        {
            var sut = CreateValidator();

            var actual = sut.IsValid(expression);

            Assert.That(actual, Is.False);
        }

        private AdditionExpressionValidator CreateValidator()
        {
            return new AdditionExpressionValidator();
        }
    }
}
