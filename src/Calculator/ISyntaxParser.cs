namespace Calculator
{
    internal interface ISyntaxParser
    {
        BinaryNode<ArithmeticToken> Parse(string statement);
    }
}