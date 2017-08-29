using System.Linq.Expressions;

namespace Calculator
{
    internal interface IArithmeticSyntaxParser
    {
        Expression Parse(string statement);
    }
}