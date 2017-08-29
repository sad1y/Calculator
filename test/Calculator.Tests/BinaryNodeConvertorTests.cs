using System;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class BinaryNodeConverterTests
    {
        [Theory, MemberData(nameof(GetFromPostfixNotationTestData))]
        void FromPostfixNotation(IEnumerable<ArithmeticToken> tokens, BinaryNode<ArithmeticToken> expectedStructure)
        {
            var root = BinaryNodeConverter.FromPostfixNotation(tokens);

            Assert.Equal(expectedStructure, root);
        }

        public static IEnumerable<object> GetFromPostfixNotationTestData()
        {
            yield return new object[] {
                new [] { ArithmeticToken.Create(ArithmeticTokenKind.Integer, "1"), ArithmeticToken.Create(ArithmeticTokenKind.Integer, "2"), ArithmeticToken.Create(ArithmeticTokenKind.Operator, "+") },
                new BinaryNode<ArithmeticToken>(
                    ArithmeticToken.Create(ArithmeticTokenKind.Operator, "+"),
                    new BinaryNode<ArithmeticToken>(ArithmeticToken.Create(ArithmeticTokenKind.Integer, "1")),
                    new BinaryNode<ArithmeticToken>(ArithmeticToken.Create(ArithmeticTokenKind.Integer, "2"))
                )
            };
        }
    }
}
