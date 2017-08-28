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
            yield return new object[] { "1+2*(3+1)+100", null };
        }
    }
}
