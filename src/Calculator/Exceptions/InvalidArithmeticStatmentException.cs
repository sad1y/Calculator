using System;

namespace Calculator
{
    [Serializable]
    public class InvalidArithmeticstatementException : ArithmeticstatementException
    {
        public InvalidArithmeticstatementException(string message) : base(message)
        {
        }
    }
}