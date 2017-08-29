using System;
using System.Collections.Generic;

namespace Calculator
{
    // https://en.wikipedia.org/wiki/Shunting-yard_algorithm   
    internal class ShuntingYardSyntaxParser : IArithmeticSyntaxParser
    {
        public BinaryNode<ArithmeticToken> Parse(string statement)
        {
            if (string.IsNullOrEmpty(statement))
                throw new ArgumentNullException(nameof(statement));

            var position = 0;
            var operatorStack = new Stack<ArithmeticToken>();
            var outputQueue = new Queue<ArithmeticToken>();

            while (true)
            {
                var currentToken = ArithmeticToken.GetNextToken(statement, position); // position could be passed by ref, but for sake of single responsibility...

                if (currentToken.Kind == ArithmeticTokenKind.NotSupported)
                {
                    throw new InvalidArithmeticTokenException(currentToken);
                }

                if (currentToken.Kind == ArithmeticTokenKind.End) break;

                // while we have to copy position every time 
                position += currentToken.Value.Length;

                if (currentToken.Kind == ArithmeticTokenKind.Nope) continue;

                if (currentToken.Kind == ArithmeticTokenKind.Double ||
                    currentToken.Kind == ArithmeticTokenKind.Integer)
                {
                    outputQueue.Enqueue(currentToken);
                    continue;
                }

                if (currentToken.Kind == ArithmeticTokenKind.Operator)
                {
                    // there is an operator at the top of the operator stack
                    if (operatorStack.Count > 0)
                    {
                        var op = operatorStack.Peek();

                        // with greater than or equal to precedence 
                        // and the operator is left associative
                        var topStackOperatorInfo = op.GetOperatorInfo();
                        var currentOperatorInfo = currentToken.GetOperatorInfo();

                        if (topStackOperatorInfo != null &&
                            topStackOperatorInfo.IsLeft &&
                            topStackOperatorInfo.Precedence >= currentOperatorInfo.Precedence)

                            // pop operators from the operator stack, onto the output queue.
                            outputQueue.Enqueue(operatorStack.Pop());
                    }

                    // push the read operator onto the operator stack
                    operatorStack.Push(currentToken);

                    continue;
                }

                if (currentToken.Kind == ArithmeticTokenKind.Priority)
                {
                    const string LeftBracket = "(";

                    // if the token is a left bracket (i.e. "(")
                    if (currentToken.Value == LeftBracket)
                    {
                        // push it onto the operator stack.
                        operatorStack.Push(currentToken);
                        continue;
                    }

                    // if the token is a right bracket (i.e. ")")
                    // while the operator at the top of the operator stack is not a left bracket:
                    while (true)
                    {
                        // if the stack runs out without finding a left bracket, then there are
                        // mismatched parentheses
                        if (operatorStack.Count == 0)
                        {
                            throw new InvalidArithmeticstatementException("parentheses mismatched");
                        }

                        // pop operator
                        var op = operatorStack.Pop();

                        if (op.Value == LeftBracket)
                        {
                            break;
                        }

                        outputQueue.Enqueue(op);
                    }
                }
            }

            // if there are no more tokens
            // while there are still operator tokens on the stack
            if (operatorStack.Count > 0)
            {
                var topOperator = operatorStack.Peek();

                // if the operator token on the top of the stack is a bracket, then
                if (topOperator != null && topOperator.Kind == ArithmeticTokenKind.Priority) {
                    // there are mismatched parentheses
                    throw new InvalidArithmeticstatementException("parentheses mismatched");
                }
            }

            // pop the operator onto the output queue
            while (operatorStack.Count > 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            return BinaryNodeConverter.FromPostfixNotation(outputQueue);
        }
    }
}
