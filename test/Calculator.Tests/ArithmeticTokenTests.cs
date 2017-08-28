using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class ArithmeticTokenTests
    {
        [Theory, MemberData(nameof(GetParseTestData))]
        void ParseTest(string statement, int position, string expectedTokenVal, sbyte expectedTokenKind)
        {
            var token = ArithmeticToken.Parse(statement, ref position);

            Assert.Equal(expectedTokenKind, token.Kind);
            Assert.Equal(expectedTokenVal, token.Value);
        }

        public static IEnumerable<object> GetParseTestData()
        {
            yield return new object[] { "1+3", 0, "1", ArithmeticTokenKind.Integer };
        }
    }
}
