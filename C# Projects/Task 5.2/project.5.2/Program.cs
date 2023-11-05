using System;

class Program
{
	static void Main()
	{
		Console.Write("Введите целочисленное число: ");
		string input = Console.ReadLine();

		if (IsValidInteger(input))
		{
			int sum = CalculateDigitSum(input);
			Console.WriteLine($"Сумма цифр введенного числа: {sum}");
		}
		else
		{
			Console.WriteLine("Введено некорректное число.");
		}
	}

	// Проверяет, является ли введенная строка корректным целым числом
	static bool IsValidInteger(string input)
	{
		// Проверяем каждый символ в строке
		for (int i = 0; i < input.Length; i++)
		{
			if (i == 0 && (input[i] == '-' || input[i] == '+'))
			{
				continue; // Знак "+" или "-" разрешены только в начале строки
			}

			if (!char.IsDigit(input[i]))
			{
				return false; // Если найден нецифровой символ, число некорректно
			}
		}
		return true;
	}

	// Вычисляет сумму цифр в строке
	static int CalculateDigitSum(string input)
	{
		int sum = 0;

		// Учитываем знак, если он есть
		int startIndex = input[0] == '-' || input[0] == '+' ? 1 : 0;

		for (int i = startIndex; i < input.Length; i++)
		{
			int digit = input[i] - '0'; // Преобразуем символ в цифру
			sum += digit;
		}

		return sum;
	}
}