using System;

namespace Calculator
{
    public struct ArithmeticToken
    {
        public const sbyte None = sbyte.MinValue;
        public const sbyte NotSupported = sbyte.MaxValue;

        public sbyte Kind;

        internal static ArithmeticToken Parse(string statment, ref int position)
        {
            
        }
    }
}