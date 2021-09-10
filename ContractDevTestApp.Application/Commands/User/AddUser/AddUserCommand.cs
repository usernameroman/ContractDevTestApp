using ContractDevTestApp.Application.Common.Mappings.Interfaces;
using ContractDevTestApp.Application.Common.Models.Persistent;
using MediatR;

namespace ContractDevTestApp.Application.Commands.User.AddUser
{
	public class AddUserCommand : IRequest<UserDto>, IMapTo<Domain.Entities.User>
	{
		public string Login { get; set; }
		public string Password { get; set; }
	}
}