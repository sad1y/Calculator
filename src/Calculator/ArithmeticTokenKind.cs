namespace Calculator
{
    internal enum ArithmeticTokenKind : sbyte
    {
        End = 0,
        Nope = 1,
        Integer = 2,
        Double = 3,
        Operator = 4,
        Priority = 5,
        NotSupported = sbyte.MaxValue
    }
}