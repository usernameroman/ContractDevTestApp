using System;
using ContractDevTestApp.Domain.Interfaces;

namespace ContractDevTestApp.Domain.Entities.Base
{
	public class BaseEntity: IBaseEntity
	{
		public Guid Id { get; set; }
	}
}