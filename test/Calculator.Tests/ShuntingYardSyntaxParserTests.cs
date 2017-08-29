using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class ShuntingYardSyntaxParserTests
    {
        [Theory, MemberData(nameof(GetParseStatementData))]
        void Parse(string statement, BinaryNode<ArithmeticToken> expected)
        {
            var parser = new ShuntingYardSyntaxParser();

            var result = parser.Parse(statement);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object> GetParseStatementData()
        {
            yield return new object[] { "1+2*(3+1)+100",
                new BinaryNode<ArithmeticToken>(OperatorToken("+"), 
                    new BinaryNode<ArithmeticToken>(IntegerToken("1")),
                    new BinaryNode<ArithmeticToken>(OperatorToken("+"),
                        new BinaryNode<ArithmeticToken>(OperatorToken("*"),
                            new BinaryNode<ArithmeticToken>(IntegerToken("2")),
                            new BinaryNode<ArithmeticToken>(OperatorToken("+"),
                                new BinaryNode<ArithmeticToken>(IntegerToken("3")),
                                new BinaryNode<ArithmeticToken>(IntegerToken("1"))
                            ))
                        ,
                        new BinaryNode<ArithmeticToken>(IntegerToken("100"))
                    )
                )
            };
        }

        private static ArithmeticToken IntegerToken(string val)
        {
            return ArithmeticToken.Create(ArithmeticTokenKind.Integer, val);
        }

        private static ArithmeticToken OperatorToken(string val)
        {
            return ArithmeticToken.Create(ArithmeticTokenKind.Operator, val);
        }
    }
}
