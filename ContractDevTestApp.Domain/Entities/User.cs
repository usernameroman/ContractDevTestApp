using System;
using System.Collections.Generic;
using ContractDevTestApp.Domain.Entities.Base;
using ContractDevTestApp.Domain.Interfaces;

namespace ContractDevTestApp.Domain.Entities
{
	public class User : BaseEntity, IHasCreatedAt
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public DateTimeOffset CreatedAt { get; set; }

		public virtual ICollection<UserIpInfo> UserIpInfos { get; set; }
	}
}