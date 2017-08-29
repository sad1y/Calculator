using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Calculator
{
    public static class ExpressionConverter
    {
        public static Expression FromPostfixNotation(IEnumerable<ArithmeticToken> tokens)
        {
            var stack = new Stack<Expression>();

            foreach (var token in tokens)
            {
                // token is an operator
                if (token.Kind == ArithmeticTokenKind.Operator)
                {
                    var right = stack.Pop();
                    var left = stack.Pop();

                    // create node with left and right operand
                    var binaryNode = GetBinaryExpressionFactory(token.Value)(left, right);

                    // result back onto the stack
                    stack.Push(binaryNode);
                }
                // if token is an operand
                else
                {
                    object val = token.Kind == ArithmeticTokenKind.Integer ?
                        int.Parse(token.Value) :
                        double.Parse(token.Value);

                    stack.Push(Expression.Constant(val));
                }
            }

            // return root node
            return stack.Pop();
        }

        private static Func<Expression, Expression, BinaryExpression> GetBinaryExpressionFactory(string operatorName)
        {
            switch (operatorName)
            {
                case "-": return Expression.Subtract;
                case "+": return Expression.Add;
                case "/": return Expression.Divide;
                case "*": return Expression.Multiply;
                case "^": return Expression.Power;
                default:
                    {
                        throw new NotSupportedException($"operator {operatorName} does not supported");
                    }
            }
        }
    }
}
