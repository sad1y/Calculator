using System.Collections.Generic;

namespace Calculator
{
    // https://en.wikipedia.org/wiki/Shunting-yard_algorithm   
    internal class ShuntingYardSyntaxParser : ISyntaxParser
    {
        internal BinaryNode<ArithmeticToken> Parse(string statment)
        {
            var position = 0;
            var stack = new Stack<ArithmeticToken>();
            var queue = new Stack<ArithmeticToken>();

            while (true)
            {
                var token = ArithmeticToken.Parse(statment, ref position);

                if (token.Kind == ArithmeticToken.NotSupported)
                {
                    throw new InvalidArithmeticTokenException(token);
                }

                if (token.Kind == ArithmeticToken.None) break;

            }

        }
    }
}
