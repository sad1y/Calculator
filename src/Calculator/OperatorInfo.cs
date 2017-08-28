using System;
using System.Collections.Generic;

namespace Calculator
{
    internal class OperatorInfo
    {
        private readonly static IDictionary<string, OperatorInfo> operatorDefinitions;

        public sbyte Precedence { get; }
        public bool IsLeft { get; }

        private OperatorInfo(sbyte precedence, bool isLeft)
        {
            Precedence = precedence;
            IsLeft = isLeft;
        }

        static OperatorInfo()
        {
            operatorDefinitions = new Dictionary<string, OperatorInfo>()
                {
                    { "^", new OperatorInfo(4, false) },
                    { "*", new OperatorInfo(3, true) },
                    { "/", new OperatorInfo(3, true) },
                    { "+", new OperatorInfo(2, true) },
                    { "-", new OperatorInfo(2, true) },
                };
        }

        public static OperatorInfo Find(string operatorName)
        {
            return operatorDefinitions.ContainsKey(operatorName) ?
                operatorDefinitions[operatorName]:
                null;
        }
    }
}