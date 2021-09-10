using System;
using ContractDevTestApp.Application.Common.Models.Persistent;
using MediatR;

namespace ContractDevTestApp.Application.Queries.User.GetUserById
{
	public class GetUserByIdQuery : IRequest<UserDto>
	{
		public Guid Id { get; set; }
	}
}