namespace Calculator
{
    internal interface IArithmeticSyntaxParser
    {
        BinaryNode<ArithmeticToken> Parse(string statement);
    }
}