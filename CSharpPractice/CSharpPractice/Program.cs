using CSharpPractice.Classes;

namespace CSharpPractice
{
	internal class Program
	{
		static double numberTwo = 12.34;
		static void Main(string[] args)
		{
			double[] numbers = new double[] { 1, 2, 3, 42, 42154 };
			var result = SimpleMath.Add(numbers);
			Console.WriteLine(result);
			BankAccount bankAccount= new BankAccount(1000);
			bankAccount.AddToBalance(100);
			Console.WriteLine(bankAccount.Balance);
			ChildBankAccount childBackAccount= new ChildBankAccount();
			childBackAccount.AddToBalance(10);
			Console.WriteLine(childBackAccount.Balance);
		}
	}

	class SimpleMath
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
	}
}