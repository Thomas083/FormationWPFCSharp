using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPractice.Classes
{
	public class BankAccount
	{
		private double balance;
		public double Balance
		{
			get 
			{ 
				if (balance < 1000000)
				{
					return balance;
				}
				else
				{
					return 1000000;
				}
			} 
			protected set 
			{
				if (value > 0)
				{
					balance = value;
				} 
				else
				{
					balance = 0;
				}
			}
		}

		public BankAccount()
		{
			Balance = 100;
		}

		public double AddToBalance(double balanceToBeAdded)
		{
			Balance += balanceToBeAdded;
			return Balance;
		}
	}

	public class ChildBankAccount : BankAccount
	{
		public ChildBankAccount()
		{
			Balance = 10;
		}
	}
}
