using System;

namespace ContractDevTestApp.Application.Common.Exceptions
{
	public class InvalidLoginException : Exception
	{
		public InvalidLoginException(string message) : base(message)
		{

		}
	}
}