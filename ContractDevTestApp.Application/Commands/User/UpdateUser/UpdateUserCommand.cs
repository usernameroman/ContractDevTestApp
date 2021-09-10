using System;
using ContractDevTestApp.Application.Common.Mappings.Interfaces;
using ContractDevTestApp.Application.Common.Models.Persistent;
using MediatR;

namespace ContractDevTestApp.Application.Commands.User.UpdateUser
{
	public class UpdateUserCommand : IRequest<UserDto>, IMapTo<Domain.Entities.User>
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
	}
}