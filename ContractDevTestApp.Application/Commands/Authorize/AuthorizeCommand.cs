using ContractDevTestApp.Application.Common.Models.Transient;
using MediatR;

namespace ContractDevTestApp.Application.Commands.Authorize
{
	/// <summary>
	/// Model to auth
	/// </summary>
	public class AuthorizeCommand : IRequest<TokenDto>
	{
		public string Login { get; set; }

		public string Password { get; set; }
	}
}