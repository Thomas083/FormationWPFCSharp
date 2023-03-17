using CSharpPractice.Classes;
using CSharpPractice.Interfaces;

namespace CSharpPractice
{
	internal class Program
	{
		static double numberTwo = 12.34;
		static void Main(string[] args)
		{
			BankAccount bankAccount = new BankAccount(1000);
			bankAccount.AddToBalance(100);

			SimpleMath simpleMath = new SimpleMath();

			Console.WriteLine(Information(bankAccount));
			Console.WriteLine(Information(simpleMath));
		}

		private static string Information(IInformation information)
		{
			return information.GetInformation();
		}
	}

	class SimpleMath : IInformation
	{
		public static double Add(double n1, double n2)
		{
			return n1 + n2;
		}
		
		public static double Add(double[] numbers)
		{
			double result = 0;

			foreach(double number in numbers)
			{
				result += number;
			}
			return result;
		}

		public string GetInformation()
		{
			return "Class that solves simple math.";
		}
	}
}