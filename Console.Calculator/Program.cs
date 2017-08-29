using Calculator;
using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Calc v0.01");
            Console.WriteLine("Double and Integer\t- supported");
            Console.WriteLine("Bracket\t\t\t- supported");
            Console.WriteLine("Supported operation: \t*, /, ^, -, +, ");
            Console.ForegroundColor = ConsoleColor.White;
            
            while (true)
            {
                CalcEnteredExpression();
            }
        }

        private static void CalcEnteredExpression()
        {
            var calc = ArithmeticExpressionEvaluator.Instance;

            try
            {
                Console.WriteLine();
                Console.WriteLine("Enter your expression:");
                var statement = Console.ReadLine();
                var answer = calc.Eval(statement);
                Console.WriteLine("Answer: {0}", answer);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name} {ex.Message}");
            }
        }
    }
}
