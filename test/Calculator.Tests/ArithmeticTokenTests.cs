using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class ArithmeticTokenTests
    {
        [Theory, MemberData(nameof(GetParseTestData))]
        void ParseTest(string statement, int position, ArithmeticToken expectedToken)
        {
            var token = ArithmeticToken.GetNextToken(statement, position);

            Assert.Equal(expectedToken, token);
        }

        public static IEnumerable<object> GetParseTestData()
        {
            yield return new object[] { "1+3", 0, ArithmeticToken.Create(ArithmeticTokenKind.Integer, "1") };
            yield return new object[] { string.Empty, 0, ArithmeticToken.Create(ArithmeticTokenKind.End, string.Empty) };
            yield return new object[] { " ", 0, ArithmeticToken.Create(ArithmeticTokenKind.Nope, " ") };
        }
    }
}
