namespace Calculator
{
    internal static class ArithmeticTokenKind
    {
        public const sbyte End = 0;
        public const sbyte Nope = 1;
        public const sbyte Integer = 2;
        public const sbyte Double = 2;
        public const sbyte Operator = 4;
        public const sbyte Priority = 5;
        public const sbyte NotSupported = sbyte.MaxValue;
    }
}