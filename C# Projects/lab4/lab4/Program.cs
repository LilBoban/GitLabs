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
			long result = CalculateRPN(rpnString);
			Console.WriteLine(result);
		}

		static string SaveBuffer(List<object> tokens, string buffer)
		{
			if (!string.IsNullOrEmpty(buffer))
			{
				tokens.Add(int.Parse(buffer));
			}

			return string.Empty;
		}

		static List<object> TokensCreation(string input)
		{
			List<object> tokens = new List<object>();
			string buffer = string.Empty;
			for (int i = 0; i < input.Length; i++)
			{
				if (char.IsDigit(input[i]))
				{
					buffer += input[i];
				}
				else
				{
					buffer = SaveBuffer(tokens, buffer);
					tokens.Add(input[i]);
				}
			}

			SaveBuffer(tokens, buffer);

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
				}
				else if (stk.Count != 0 && IsOperation(token.ToString()[0]))
				{
					object lastOperation = stk.Peek();
					if (GetOperatonPriority(lastOperation) < GetOperatonPriority(token))
					{
						stk.Push(token);
						continue;
					}
					else
					{
						result.Add(stk.Pop());
						stk.Push(token);
						continue;
					}
				}
				else if (stk.Count() == 0)
				{
					stk.Push(token);
					continue;
				}

				if (token.ToString()[0] == '(')
				{
					stk.Push(token);
					continue;
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
					case '+': return 1;
					case '-': return 1;
					case '*': return 2;
					case '/': return 2;
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

		static int CalculateRPN(List<object> rpnString)
		{
			Stack<int> operationStack = new Stack<int>();
			//List<long> numbers = new List<long>();
			//List<char> operands = new List<char>();
			foreach (var token in rpnString)
			{
				
				if (char.IsDigit(token.ToString()[0]))
				{
					operationStack.Push(Convert.ToInt32(token));
					//numbers.Add(Convert.ToInt64(token));
				}
				else
				{
					char operation = token.ToString()[0];
					int num2 = operationStack.Pop(); 
					int num1 = operationStack.Pop();
					operationStack.Push(ApplyOperation(operation, num1, num2));
					//operands.Add(Convert.ToChar(token));
				}
				
			}
			
			static int ApplyOperation(char operation, int op1, int op2)
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

			static bool IsOperation(char c)
			{
				if (c == '+' ||
				    c == '-' ||
				    c == '*' ||
				    c == '/')
					return true;
				else
					return false;
			}

			return operationStack.Pop();
		}
	}
}