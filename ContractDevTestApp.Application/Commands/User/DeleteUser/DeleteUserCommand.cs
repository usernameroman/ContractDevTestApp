using System;
using System.Text.Json.Serialization;
using MediatR;

namespace ContractDevTestApp.Application.Commands.User.DeleteUser
{
	public class DeleteUserCommand : IRequest
	{
		[JsonIgnore]
		public Guid Id { get; set; }
	}
}