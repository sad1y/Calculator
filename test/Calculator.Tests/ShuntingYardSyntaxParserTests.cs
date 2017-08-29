using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Calculator.Tests
{
    public class ShuntingYardSyntaxParserTests
    {
        [Theory, MemberData(nameof(GetParseStatementData))]
        void Parse(string statement, Expression expected)
        {
            var parser = new ShuntingYardSyntaxParser();

            var result = parser.Parse(statement);

            Assert.Equal(expected.ToString(), result.ToString());
        }

        [Theory, MemberData(nameof(GetParseInvalidStatementData))]
        void ParseInvalidStatment(string statement, Type expectedExceptionType)
        {
            var parser = new ShuntingYardSyntaxParser();

            Assert.Throws(expectedExceptionType, () => parser.Parse(statement));
        }

        public static IEnumerable<object> GetParseStatementData()
        {
            yield return new object[] { "1+2 *(3+1) + 100",
                Expression.Add(
                    Expression.Constant(1),
                    Expression.Add(
                        Expression.Multiply(
                            Expression.Constant(2),
                            Expression.Add(
                                Expression.Constant(3),
                                Expression.Constant(1)
                             )
                         ),
                        Expression.Constant(100)
                    )
                )
            };
        }

        public static IEnumerable<object> GetParseInvalidStatementData()
        {
            yield return new object[] { "(1+2", typeof(InvalidArithmeticStatementException) };
            yield return new object[] { "1+2)", typeof(InvalidArithmeticStatementException) };
        }
    }
}
