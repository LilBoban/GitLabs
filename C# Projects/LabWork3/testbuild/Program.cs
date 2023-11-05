using System;
using System.Collections.Generic;

namespace testbuild
{
	class Calculator
	{
		static void Main()
		{
			string inputData = Console.ReadLine().Replace(" ", "");
			float result = EvaluateExpression(inputData);
			Console.WriteLine($"Результат: {result}");
		}

		static float EvaluateExpression(string inputData)
		{
			List<float> numbers = new List<float>();
			List<(char Operator, int Priority)> operators = new List<(char, int)>();
			int currentPriority = 0;

			for (int i = 0; i < inputData.Length; i++)
			{
				char currentChar = inputData[i];

				if (char.IsDigit(currentChar) || currentChar == ',')
				{
					string buffer = currentChar.ToString();
					while ((i + 1) < inputData.Length && (char.IsDigit(inputData[i + 1]) || inputData[i + 1] == ','))
					{
						buffer += inputData[i + 1];
						i++;
					}

					if (float.TryParse(buffer, out float parsedValue))
					{
						numbers.Add(parsedValue);
					}
					else
					{
						Console.WriteLine("Ошибка: Неправильный формат числа");
					}
				}
				else if (currentChar == '(')
				{
					currentPriority += 2;
				}
				else if (currentChar == ')')
				{
					currentPriority -= 2;
				}
				else if (IsOperator(currentChar))
				{
					int priority = currentPriority;

					if (currentChar == '*' || currentChar == '/')
					{
						priority++;
					}

					while (operators.Count > 0 && operators[operators.Count - 1].Priority >= priority)
					{
						PerformCalculations(operators, numbers);
					}

					operators.Add((currentChar, priority));
				}
			}

			while (operators.Count > 0)
			{
				PerformCalculations(operators, numbers);
			}

			return numbers[0];
		}

		private static bool IsOperator(char currentChar)
		{
			return currentChar == '+' || currentChar == '-' || currentChar == '*' || currentChar == '/';
		}

		public static void PerformCalculations(List<(char Operator, int Priority)> operators, List<float> operands)
		{
			char op = operators[operators.Count - 1].Operator;
			operators.RemoveAt(operators.Count - 1);

			float operand2 = operands[operands.Count - 1];
			operands.RemoveAt(operands.Count - 1);

			float operand1 = operands[operands.Count - 1];
			operands.RemoveAt(operands.Count - 1);

			operands.Add(PerformOperation(op, operand1, operand2));
		}

		static float PerformOperation(char op, float operand1, float operand2)
		{
			switch (op)
			{
				case '+': return operand1 + operand2;
				case '-': return operand1 - operand2;
				case '*': return operand1 * operand2;
				case '/': return operand1 / operand2;
			}

			throw new Exception("Неизвестная операция");
		}
	}
}