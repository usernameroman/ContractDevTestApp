using System;

namespace ContractDevTestApp.Domain.Interfaces
{
	public interface IBaseEntity
	{
		Guid Id { get; set; }
	}
}