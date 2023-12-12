using System;
using System.Collections.Generic;

namespace Lab_num_5
{
    internal class Program
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            input = input.Replace(" ", string.Empty);
            List<Token> tokensList = TokensCreation(input);
            List<Token> rpnTokens = RpnCalculator.TransformToRpn(tokensList);
            double result = RpnCalculator.CalculateRpn(rpnTokens);
            Console.WriteLine(result);
        }

        static List<Token> TokensCreation(string input)
        {
            List<Token> tokens = new List<Token>();
            string buffer = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                if (char.IsDigit(currentChar) || currentChar == ',')
                {
                    buffer = currentChar.ToString();
                    while ((i + 1) < input.Length && (char.IsDigit(input[i + 1]) || input[i + 1] == ','))
                    {
                        buffer += input[i + 1];
                        i++;
                    }

                    if (double.TryParse(buffer, out var parsedValue))
                    {
                        tokens.Add(new Token(TokenType.Number, parsedValue));
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Неправильный формат числа");
                    }
                }
                else if (currentChar == '+' || currentChar == '-' || currentChar == '*' || currentChar == '/')
                {
                    tokens.Add(new Token(TokenType.Operator, null, currentChar));
                }
                else if (currentChar == '(')
                {
                    tokens.Add(new Token(TokenType.LeftParenthesis));
                }
                else if (currentChar == ')')
                {
                    tokens.Add(new Token(TokenType.RightParenthesis));
                }
            }

            return tokens;
        }
        public class Token
    {
        public TokenType Type { get; set; }
        public double? Value { get; set; }
        public char? Operator { get; set; }

        public Token(TokenType type, double? value = null, char? op = null)
        {
            Type = type;
            Value = value;
            Operator = op;
        }
    }

    public enum TokenType
    {
        Number,
        Operator,
        LeftParenthesis,
        RightParenthesis
    }

    public class RpnCalculator
    { 
        public static List<Token> TransformToRpn(List<Token> tokens)
        {
            List<Token> result = new List<Token>();
            Stack<Token> stack = new Stack<Token>();

            foreach (var token in tokens)
            {
                if (token.Type == TokenType.Number)
                {
                    result.Add(token);
                }
                else if (token.Type == TokenType.Operator)
                {
                    if (token.Operator == '+' || token.Operator == '-')
                    {
                        // Обработка операторов + и -
                        while (stack.Count != 0 && (stack.Peek().Operator == '+' || stack.Peek().Operator == '-' ||
                                                    stack.Peek().Operator == '*' || stack.Peek().Operator == '/'))
                        {
                            result.Add(stack.Pop());
                        }
                        stack.Push(token);
                    }
                    else if (token.Operator == '*' || token.Operator == '/')
                    {
                        // Обработка операторов * и /
                        while (stack.Count != 0 && (stack.Peek().Operator == '*' || stack.Peek().Operator == '/'))
                        {
                            result.Add(stack.Pop());
                        }
                        stack.Push(token);
                    }
                }
                else if (token.Type == TokenType.LeftParenthesis)
                {
                    stack.Push(token);
                }
                else if (token.Type == TokenType.RightParenthesis)
                {
                    while (stack.Count != 0 && stack.Peek().Type != TokenType.LeftParenthesis)
                    {
                        result.Add(stack.Pop());
                    }
                    stack.Pop(); // Pop the left parenthesis
                }
            }

            while (stack.Count != 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }

        public static double CalculateRpn(List<Token> rpnTokens)
        {
            Stack<double> operandStack = new Stack<double>();

            foreach (var token in rpnTokens)
            {
                if (token.Type == TokenType.Number)
                {
                    if (token.Value.HasValue)
                    {
                        operandStack.Push(token.Value.Value);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Неправильный формат числа");
                        return 0; 
                    }
                }
                else if (token.Type == TokenType.Operator)
                {
                    if (operandStack.Count >= 2) // Убедимся, что в стеке достаточно операндов
                    {
                        double num2 = operandStack.Pop();
                        double num1 = operandStack.Pop();
                        operandStack.Push(ApplyOperation(token.Operator.Value, num1, num2));
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Недостаточно операндов для операции");
                        return 0; 
                    }
                }
            }

            if (operandStack.Count == 1)
            {
                return operandStack.Pop();
            }
            else
            {
                Console.WriteLine("Ошибка: Некорректное выражение");
                return 0;
                
            }
        }

        private static double ApplyOperation(char operation, double op1, double op2)
        {
            switch (operation)
            {
                case '+':
                    return op1 + op2;
                case '-':
                    return op1 - op2;
                case '*':
                    return op1 * op2;
                case '/':
                    return op1 / op2;
                default:
                    return 0;
            }
        }
    }
    }
}
