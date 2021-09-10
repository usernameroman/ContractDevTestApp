using System;

namespace ContractDevTestApp.Domain.Interfaces
{
	public interface IHasCreatedAt
	{
		public DateTimeOffset CreatedAt { get; set; }
	}
}