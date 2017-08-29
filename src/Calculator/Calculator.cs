using System;
using System.Linq.Expressions;

namespace Calculator
{
    public class ArithmeticExpressionEvaluator
    {
        private readonly IArithmeticSyntaxParser _syntaxParser;
        
        public static readonly ArithmeticExpressionEvaluator Instance = new ArithmeticExpressionEvaluator();

        private ArithmeticExpressionEvaluator()
        {
            _syntaxParser = new ShuntingYardSyntaxParser();
        }

        public double Eval(string statement)
        {
            // evaluation of expression could be written by hand, 
            // but i prefer to reuse infrastructure that .net has
            var expression = _syntaxParser.Parse(statement);

            var lambdaExpression = Expression.Lambda<Func<double>>(expression);

            return lambdaExpression.Compile()();
        }
    }
}
