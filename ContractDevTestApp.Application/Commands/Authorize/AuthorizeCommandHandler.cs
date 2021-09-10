using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContractDevTestApp.Application.Common.Exceptions;
using ContractDevTestApp.Application.Common.Models.Transient;
using ContractDevTestApp.Application.Common.Services.Interfaces;
using ContractDevTestApp.Domain.Interfaces;
using MediatR;

namespace ContractDevTestApp.Application.Commands.Authorize
{
	public class AuthorizeCommandHandler : IRequestHandler<AuthorizeCommand, TokenDto>
	{
		private readonly IAuthorizationService _authorizationService;
		private readonly IRepository<Domain.Entities.User> _usersRepository;

		public AuthorizeCommandHandler(IRepository<Domain.Entities.User> usersRepository, IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
			_usersRepository = usersRepository;
		}
		public async Task<TokenDto> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
		{

			var user = (await _usersRepository.GetBy(x => x.Login == request.Login, cancellationToken)).FirstOrDefault();

			if (user == null || !_authorizationService.VerifyPassword(request.Password, user.Password))
			{
				throw new InvalidLoginException("Invalid login credentials");
			}

			return _authorizationService.CreateToken(user, cancellationToken);
		}
	}
}