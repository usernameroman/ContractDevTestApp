using System;
using ContractDevTestApp.Application.Common.Mappings.Interfaces;
using ContractDevTestApp.Application.Common.Models.Persistent;
using ContractDevTestApp.Domain.Entities;

namespace ContractDevTestApp.Application.Common.Models.Transient
{
	public class TokenDto : IMapFrom<User>
	{
		public UserDto User { get; set; }
		public DateTimeOffset ValidTo { get; set; }
		public string Token { get; set; }
	}
}