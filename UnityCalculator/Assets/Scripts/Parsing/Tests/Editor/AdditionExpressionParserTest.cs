using NUnit.Framework;

namespace UnityCalculator.Parsing
{
    [TestFixture]
    public class AdditionExpressionParserTest
    {
        [TestCase("54+21", 54L)]
        [TestCase("45+00", 45L)]
        [TestCase("0+99", 0L)]
        public void Parse_WithValidExpression_ReturnsCorrectLeft(string expression, long expected)
        {
            IExpressionParser sut = new AdditionExpressionParser();
            sut.TryParse(expression, out var actual);

            Assert.That(actual.Left, Is.EqualTo(expected));
        }

        [TestCase("54+21", 21L)]
        [TestCase("45+00", 0L)]
        [TestCase("0+99", 99L)]
        public void Parse_WithValidExpression_ReturnsCorrectRight(string expression, long expected)
        {
            IExpressionParser sut = new AdditionExpressionParser();
            sut.TryParse(expression, out var actual);

            Assert.That(actual.Right, Is.EqualTo(expected));
        }
    }
}