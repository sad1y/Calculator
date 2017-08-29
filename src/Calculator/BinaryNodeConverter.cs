using System.Collections.Generic;

namespace Calculator
{
    internal static class BinaryNodeConverter
    {
        public static BinaryNode<ArithmeticToken> FromPostfixNotation(IEnumerable<ArithmeticToken> tokens)
        {
            var stack = new Stack<BinaryNode<ArithmeticToken>>();

            foreach (var token in tokens)
            {
                // token is an operator
                if (token.Kind == ArithmeticTokenKind.Operator)
                {
                    var right = stack.Pop();
                    var left = stack.Pop();

                    // create node with left and right operand
                    var binaryNode = new BinaryNode<ArithmeticToken>(token, left, right);

                    // result back onto the stack
                    stack.Push(binaryNode);
                }
                // if token is an operand
                else
                {
                    stack.Push(new BinaryNode<ArithmeticToken>(token));
                }
            }

            // return root node
            return stack.Pop();
        }
    }
}
