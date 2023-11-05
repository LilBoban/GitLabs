using System;
using System.Collections.Generic;

namespace Project5._11
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Введите число: ");
			string value = Console.ReadLine();
			List<float> floatNumbers = new List<float>();
			List<int> intNumbers = new List<int>();
			while (value != "q" || value != "Q") // проверка на выход
			{
				bool isInt = int.TryParse(value, out int integer);
				if (isInt)
				{
					intNumbers.Add(integer);
					char symbol = Convert.ToChar(integer);
					Console.WriteLine("Символ соответствующий числу: " + symbol);
				}
				else
				{
					floatNumbers.Add(Convert.ToSingle(value));
					float lastFloatElement = floatNumbers[(floatNumbers.Count) - 1];
					int lastIntlement = intNumbers[(intNumbers.Count) - 1];
					if (floatNumbers.Count > 1)
					{
						float preLastFloatElement = floatNumbers[(floatNumbers.Count) - 2];
						if ((lastIntlement == lastFloatElement) || (preLastFloatElement == lastFloatElement))
						{
							Console.WriteLine("Выход из программы");
							Environment.Exit(0);
						}
					}
				}
				Console.WriteLine("Введите число: ");
				value = Console.ReadLine();
				if (value == "q" || value == "Q")
				{
					Console.WriteLine("Выход из программы");
					Environment.Exit(0);
				}
			}
		}
	}
}