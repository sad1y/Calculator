using System;

namespace Calculator
{
    [Serializable]
    public class InvalidArithmeticStatementException : ArithmeticstatementException
    {
        public InvalidArithmeticStatementException(string message) : base(message)
        {
        }
    }
}