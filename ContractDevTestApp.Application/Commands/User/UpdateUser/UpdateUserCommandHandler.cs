using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContractDevTestApp.Application.Common.Exceptions;
using ContractDevTestApp.Application.Common.Models.Persistent;
using ContractDevTestApp.Application.Common.Services.Interfaces;
using ContractDevTestApp.Domain.Interfaces;
using MediatR;

namespace ContractDevTestApp.Application.Commands.User.UpdateUser
{
	public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, UserDto>
	{
		private readonly IRepository<Domain.Entities.User> _usersRepository;
		private readonly IAuthorizationService _authorizationService;
		private readonly IMapper _mapper;

		public UpdateUserCommandHandler(IRepository<Domain.Entities.User> usersRepository, IAuthorizationService authorizationService, IMapper mapper)
		{
			_usersRepository = usersRepository;
			_authorizationService = authorizationService;
			_mapper = mapper;
		}

		public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			//TODO: Check login on uniqueness

			var userFromContext = await
				_usersRepository.GetByIdAsync(request.Id, cancellationToken);

			if (userFromContext == null)
			{
				throw new EntityNotFoundException();
			}

			if (!string.IsNullOrEmpty(request.Password))
			{
				request.Password = _authorizationService.HashPassword(request.Password);
			}

			return _mapper.Map<UserDto>(await _usersRepository.UpdateAsync(
				_mapper.Map(request, userFromContext), cancellationToken));
		}
	}
}