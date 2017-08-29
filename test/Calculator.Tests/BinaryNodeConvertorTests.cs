using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Calculator.Tests
{
    public class BinaryNodeConverterTests
    {
        [Theory, MemberData(nameof(GetFromPostfixNotationTestData))]
        void FromPostfixNotation(IEnumerable<ArithmeticToken> tokens, Expression expectedExpression)
        {
            var root = ExpressionConverter.FromPostfixNotation(tokens);

            Assert.Equal(expectedExpression.ToString(), root.ToString());
        }

        public static IEnumerable<object> GetFromPostfixNotationTestData()
        {
            yield return new object[] {
                new [] {
                    ArithmeticToken.Create(ArithmeticTokenKind.Integer, "1"),
                    ArithmeticToken.Create(ArithmeticTokenKind.Integer, "2"),
                    ArithmeticToken.Create(ArithmeticTokenKind.Operator, "+")
                },
                Expression.Add(
                    Expression.Constant(1),
                    Expression.Constant(2)
                )
            };
        }
    }
}
