using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContractDevTestApp.Application.Common.Exceptions;
using ContractDevTestApp.Application.Common.Models.Persistent;
using ContractDevTestApp.Domain.Interfaces;
using MediatR;

namespace ContractDevTestApp.Application.Queries.User.GetUserById
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
	{
		private readonly IRepository<Domain.Entities.User> _usersRepository;
		private readonly IMapper _mapper;

		public GetUserByIdQueryHandler(IRepository<Domain.Entities.User> usersRepository, IMapper mapper)
		{
			_usersRepository = usersRepository;
			_mapper = mapper;
		}
		public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var userFromContext = await _usersRepository.GetByIdAsync(request.Id, cancellationToken);

			if (userFromContext == null)
			{
				throw new EntityNotFoundException("Cannot find specified user");
			}

			return _mapper.Map<UserDto>(userFromContext);
		}
	}
}