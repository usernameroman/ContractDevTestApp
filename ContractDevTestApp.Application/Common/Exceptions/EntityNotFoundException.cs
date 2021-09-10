using System;

namespace ContractDevTestApp.Application.Common.Exceptions
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException(string message) : base(message)
		{
		}

		public EntityNotFoundException() : base("Specified entity not found")
		{
		}
	}
}