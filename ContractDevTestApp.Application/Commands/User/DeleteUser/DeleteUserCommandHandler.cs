using System.Threading;
using System.Threading.Tasks;
using ContractDevTestApp.Application.Common.Exceptions;
using ContractDevTestApp.Domain.Interfaces;
using MediatR;

namespace ContractDevTestApp.Application.Commands.User.DeleteUser
{
	public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand>
	{
		private readonly IRepository<Domain.Entities.User> _usersRepository;

		public DeleteUserCommandHandler(IRepository<Domain.Entities.User> usersRepository)
		{
			_usersRepository = usersRepository;
		}

		public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var userFromContext = await _usersRepository.GetByIdAsync(request.Id, cancellationToken);

			if (userFromContext == null)
			{
				throw new EntityNotFoundException();
			}

			await _usersRepository.DeleteAsync(userFromContext, cancellationToken);
			return Unit.Value;
		}
	}
}