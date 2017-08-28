namespace Calculator
{
    public partial class ArithmeticToken
    {
        private static readonly ArithmeticToken NopeToken = new ArithmeticToken(ArithmeticTokenKind.Nope, string.Empty); 
        private static readonly ArithmeticToken EndToken = new ArithmeticToken(ArithmeticTokenKind.End, string.Empty);
        private static readonly ArithmeticToken LeftBracketToken = new ArithmeticToken(ArithmeticTokenKind.Priority, "(");
        private static readonly ArithmeticToken RightBracketToken = new ArithmeticToken(ArithmeticTokenKind.Priority, ")");
        private static readonly ArithmeticToken PowerToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "^");
        private static readonly ArithmeticToken MultiplicationToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "*");
        private static readonly ArithmeticToken DivisionToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "/");
        private static readonly ArithmeticToken SubstractionToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "-");
        private static readonly ArithmeticToken AdditionToken = new ArithmeticToken(ArithmeticTokenKind.Operator, "+");

        public sbyte Kind { get; }
        public string Value { get; }

        private ArithmeticToken(sbyte kind, string value)
        {
            Kind = kind;
            Value = value;
        }

        internal OperatorInfo GetOperatorInfo() => OperatorInfo.Find(Value);

        internal static ArithmeticToken Parse(string statement, ref int position)
        {
            if (position >= statement.Length) {
                return EndToken;
            }

            if (char.IsWhiteSpace(statement[position]))
            {
                position++;
                return NopeToken;
            }

            // if first char at string is digit try to parse it as number
            // in other cases try to parse operator
            return char.IsDigit(statement[position]) ? 
                ParseNumber(statement, ref position) :
                ParseOperator(statement, ref position);
        }

        private static ArithmeticToken ParseNumber(string statement, ref int position)
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

        private static ArithmeticToken ParseOperator(string statement, ref int position)
        {
            ArithmeticToken operatorToken = null;
            switch (statement[position])
            {
                case '(': { operatorToken = LeftBracketToken; break; }
                case ')': { operatorToken = RightBracketToken; break; } 
                case '^': { operatorToken = PowerToken; break; }
                case '*': { operatorToken = MultiplicationToken; break; }
                case '/': { operatorToken = DivisionToken; break; }
                case '-': { operatorToken = SubstractionToken; break; } 
                case '+': { operatorToken = AdditionToken; break; }
                default:
                    {
                        throw new InvalidArithmeticstatementException($"operator {statement[position]} does not supported");
                    }
            }

            position++;

            return operatorToken;
        }
    }
}