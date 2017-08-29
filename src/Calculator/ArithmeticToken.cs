using System;
using System.Collections.Generic;

namespace Calculator
{
    public partial class ArithmeticToken : IEquatable<ArithmeticToken>
    {
        private static readonly ArithmeticToken NopeToken = new ArithmeticToken(ArithmeticTokenKind.Nope, " ");
        private static readonly ArithmeticToken EndToken = new ArithmeticToken(ArithmeticTokenKind.End, string.Empty);
        private static readonly ArithmeticToken LeftBracketToken = new ArithmeticToken(ArithmeticTokenKind.Priority, "(");
        private static readonly ArithmeticToken RightBracketToken = new ArithmeticToken(ArithmeticTokenKind.Priority, ")");
        private static readonly ArithmeticToken PowerToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "^");
        private static readonly ArithmeticToken MultiplicationToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "*");
        private static readonly ArithmeticToken DivisionToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "/");
        private static readonly ArithmeticToken SubstractionToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "-");
        private static readonly ArithmeticToken AdditionToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "+");

        internal ArithmeticTokenKind Kind { get; }
        public string Value { get; }

        private ArithmeticToken(ArithmeticTokenKind kind, string value)
        {
            Kind = kind;
            Value = value;
        }

        internal OperatorInfo GetOperatorInfo() => OperatorInfo.Find(Value);

        // only for tests
        internal static ArithmeticToken Create(ArithmeticTokenKind kind, string value)
        {
            return new ArithmeticToken(kind, value);
        }

        internal static ArithmeticToken GetNextToken(string statement, int position)
        {
            if (position >= statement.Length) {
                return EndToken;
            }

            if (char.IsWhiteSpace(statement[position]))
            {
                position++;
                return NopeToken;
            }

            // if first char at string is a digit lets try to parse it as a number
            // in other cases try to parse operator
            return char.IsDigit(statement[position]) ? 
                ParseNumber(statement, position) :
                ParseOperator(statement, position);
        }

        private static ArithmeticToken ParseNumber(string statement, int position)
        {
            const char DoubleDelimiter = '.';

            bool isDouble = false;

            var startPosition = position;

            // we move while we have digital or delimiter
            while(position < statement.Length)
            {
                var chr = statement[position];
                
                if (chr == DoubleDelimiter)
                {
                    isDouble = true;
                    continue;
                }

                if (!char.IsDigit(chr)) break;

                position++;
            }
            
            // substract number
            var number = statement.Substring(startPosition, position - startPosition);

            return new ArithmeticToken(
                isDouble ? ArithmeticTokenKind.Double : ArithmeticTokenKind.Integer,
                number);
        }

        private static ArithmeticToken ParseOperator(string statement, int position)
        {
            switch (statement[position])
            {
                case '(': return LeftBracketToken; 
                case ')': return RightBracketToken;
                case '^': return PowerToken; 
                case '*': return MultiplicationToken; 
                case '/': return DivisionToken; 
                case '-': return SubstractionToken; 
                case '+': return AdditionToken;
                default:
                    {
                        throw new InvalidArithmeticStatementException($"operator {statement[position]} does not supported");
                    }
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ArithmeticToken);
        }

        public bool Equals(ArithmeticToken other)
        {
            return other != null &&
                   Kind == other.Kind &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = 1484969029;
            hashCode = hashCode * -1521134295 + Kind.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            return hashCode;
        }

        public static bool operator ==(ArithmeticToken token1, ArithmeticToken token2)
        {
            return EqualityComparer<ArithmeticToken>.Default.Equals(token1, token2);
        }

        public static bool operator !=(ArithmeticToken token1, ArithmeticToken token2)
        {
            return !(token1 == token2);
        }
    }
}