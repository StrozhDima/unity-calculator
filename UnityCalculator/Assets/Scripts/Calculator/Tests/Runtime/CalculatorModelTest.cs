using NUnit.Framework;
using UnityCalculator.Arithmetic;
using UnityCalculator.Parsing;

namespace UnityCalculator.Calculator
{
    [TestFixture]
    public class CalculatorModelTest
    {
        [TestCase("54+21")]
        [TestCase("45+00")]
        public void Calculate_ValidExpression_ReturnsSuccess(string expression)
        {
            var sut = CreateCalculatorModel();

            var actual = sut.Calculate(expression);

            Assert.That(actual.Type, Is.EqualTo(CalculationResultType.Success));
        }

        [TestCase("54+21", "75")]
        [TestCase("45+00", "45")]
        public void Calculate_ValidExpression_ReturnsCorrectResult(string expression, string expectedResult)
        {
            var sut = CreateCalculatorModel();

            var actual = sut.Calculate(expression);

            Assert.That(actual.ResultDisplay, Is.EqualTo(expectedResult));
        }

        [TestCase("98.12+48.1")]
        [TestCase("45+-88")]
        [TestCase("5/5")]
        [TestCase("")]
        public void Calculate_WithInvalidExpression_ReturnsError(string expression)
        {
            var sut = CreateCalculatorModel();

            var actual = sut.Calculate(expression);

            Assert.That(actual.Type, Is.EqualTo(CalculationResultType.Error));
        }

        private CalculatorModel CreateCalculatorModel()
        {
            return new CalculatorModel(new AdditionExpressionValidator(), new AdditionExpressionParser(), new AdditionOperation());
        }
    }
}
