using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContractDevTestApp.Application.Common.Models.Persistent;
using ContractDevTestApp.Application.Common.Services.Interfaces;
using ContractDevTestApp.Domain.Interfaces;
using MediatR;

namespace ContractDevTestApp.Application.Commands.User.AddUser
{
	public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
	{
		private readonly IRepository<Domain.Entities.User> _usersRepository;
		private readonly IAuthorizationService _authorizationService;
		private readonly IMapper _mapper;

		public AddUserCommandHandler(IRepository<Domain.Entities.User> usersRepository, IAuthorizationService authorizationService, IMapper mapper)
		{
			_usersRepository = usersRepository;
			_authorizationService = authorizationService;
			_mapper = mapper;
		}

		public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
		{
			//TODO: Check login on uniqueness

			request.Password = _authorizationService.HashPassword(request.Password);

			return _mapper.Map<UserDto>(await _usersRepository.CreateAsync(
				_mapper.Map<Domain.Entities.User>(request), cancellationToken));
		}
	}
}