using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();
            input = input.Replace(" ", string.Empty);
            List<object> tokensList = TokensCreation(input);
            List<object> rpnString = Transformation(tokensList);
            double result = CalculateRpn(rpnString);
            Console.WriteLine(result);
        }

        static List<object> TokensCreation(string input)
        {
            List<object> tokens = new List<object>();
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
                        tokens.Add(parsedValue);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Неправильный формат числа");
                    }
                }
                else
                {
                    tokens.Add(currentChar.ToString());
                }
            }

            return tokens;
        }

        static List<object> Transformation(List<object> tokens)
        {
            List<object> result = new List<object>();
            Stack<object> stk = new Stack<object>();
            foreach (var token in tokens)
            {
                if (char.IsDigit(token.ToString()[0]))
                {
                    result.Add(token);
                    continue;
                }

                if (stk.Count != 0 && IsOperation(token.ToString()[0]))
                {
                    object lastOperation = stk.Peek();
                    if (GetOperatonPriority(lastOperation) < GetOperatonPriority(token))
                    {
                        stk.Push(token);
                    }
                    else
                    {
                        result.Add(stk.Pop());
                        stk.Push(token);
                    }

                    continue;
                }

                if (stk.Count() == 0)
                {
                    stk.Push(token);
                    continue;
                }

                if (token.ToString()[0] == '(')
                {
                    stk.Push(token);
                }
                else if (token.ToString()[0] == ')')
                {
                    while (stk.Peek().ToString()[0] != '(')
                    {
                        result.Add(stk.Pop());
                    }

                    stk.Pop();
                }
            }

            while (stk.Count() != 0)
            {
                result.Add(stk.Pop());
            }

            static int GetOperatonPriority(object operation)
            {
                switch (operation)
                {
                    case "+": return 1;
                    case "-": return 1;
                    case "*": return 2;
                    case "/": return 2;
                    default: return 0;
                }
            }

            static bool IsOperation(char c)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/')
                    return true;
                else
                    return false;
            }

            return result;
        }

        static double CalculateRpn(List<object> rpnString)
        {
            Stack<double> operationStack = new Stack<double>();

            foreach (var token in rpnString)
            {
                if (char.IsDigit(token.ToString()[0]))
                {
                    operationStack.Push(double.Parse(token.ToString()));
                }
                else
                {
                    char operation = token.ToString()[0];
                    double num2 = operationStack.Pop();
                    double num1 = operationStack.Pop();
                    operationStack.Push(ApplyOperation(operation, num1, num2));
                }
            }

            static double ApplyOperation(char operation, double op1, double op2)
            {
                switch (operation)
                {
                    case '+': return (op1 + op2);
                    case '-': return (op1 - op2);
                    case '*': return (op1 * op2);
                    case '/': return (op1 / op2);
                    default: return 0;
                }
            }

            return operationStack.Pop();
        }
    }
}