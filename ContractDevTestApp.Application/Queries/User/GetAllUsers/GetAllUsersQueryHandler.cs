using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContractDevTestApp.Application.Common.Models.Persistent;
using ContractDevTestApp.Domain.Interfaces;
using MediatR;

namespace ContractDevTestApp.Application.Queries.User.GetAllUsers
{
	public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IImmutableList<UserDto>>
	{
		private readonly IRepository<Domain.Entities.User> _usersRepository;
		private readonly IMapper _mapper;

		public GetAllUsersQueryHandler(IRepository<Domain.Entities.User> usersRepository, IMapper mapper)
		{
			_usersRepository = usersRepository;
			_mapper = mapper;
		}

		public async Task<IImmutableList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
		{
			return (await _usersRepository.GetAll(cancellationToken))
				.Select(_mapper.Map<UserDto>)
				.ToImmutableList();
		}
	}
}