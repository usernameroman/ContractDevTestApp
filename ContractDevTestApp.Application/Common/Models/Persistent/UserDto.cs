using System;
using System.Collections.Generic;
using ContractDevTestApp.Application.Common.Mappings.Interfaces;
using ContractDevTestApp.Domain.Entities;

namespace ContractDevTestApp.Application.Common.Models.Persistent
{
	public class UserDto : IMapFrom<User>, IMapTo<User>
	{
		public Guid Id { get; set; }
		public string Login { get; set; }

		public IList<UserInfoDto> UserIpInfos { get; set; }
	}
}