using CSharpPractice.Classes;

namespace CSharpPractice
{
	internal class Program
	{
		static double numberTwo = 12.34;
		static void Main(string[] args)
		{
			BankAccount bankAccount= new BankAccount();
			bankAccount.AddToBalance(100);
			Console.WriteLine(bankAccount.Balance);
		}
	}

	class SimpleMath
	{
		public static double Add(double n1, double n2)
		{
			return n1 + n2;
		}
	}
}