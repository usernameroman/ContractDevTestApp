using System.Collections.Immutable;
using ContractDevTestApp.Application.Common.Models.Persistent;
using MediatR;

namespace ContractDevTestApp.Application.Queries.User.GetAllUsers
{
	public class GetAllUsersQuery: IRequest<IImmutableList<UserDto>>
	{
		
	}
}