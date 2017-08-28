using System;
using System.Runtime.Serialization;

namespace Calculator
{
    [Serializable]
    public class InvalidArithmeticTokenException : ArithmeticException
    {
        public ArithmeticToken Token { get;  }

        public InvalidArithmeticTokenException(ArithmeticToken token)
        {
            Token = token;
        }
    }
}