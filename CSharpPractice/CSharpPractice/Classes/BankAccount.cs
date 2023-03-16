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
				if (Balance < 1000000)
				{
					return balance;
				}
				else
				{
					return 1000000;
				}
			} 
			private set 
			{
				if (value > 0)
				{
					Balance = value;
				} 
				else
				{
					Balance = 0;
				}
			}
		}

		public double AddToBalance(double balanceToBeAdded)
		{
			Balance += balanceToBeAdded;
			return Balance;
		}
	}
}
