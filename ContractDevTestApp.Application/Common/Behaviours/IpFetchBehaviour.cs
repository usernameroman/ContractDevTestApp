using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContractDevTestApp.Application.Common.Extensions;
using ContractDevTestApp.Application.Common.Models.Persistent;
using ContractDevTestApp.Domain.Entities;
using ContractDevTestApp.Domain.Interfaces;
using ContractDevTestApp.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ContractDevTestApp.Application.Common.Behaviours
{
	public class IpFetchBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IIpStackService _ipStackService;
		private readonly IRepository<UserIpInfo> _userInfoRepository;
		private readonly IMapper _mapper;

		public IpFetchBehaviour(
			IHttpContextAccessor httpContextAccessor,
			IIpStackService ipStackService,
			IRepository<UserIpInfo> userInfoRepository,
			IMapper mapper
		)
		{
			_httpContextAccessor = httpContextAccessor;
			_ipStackService = ipStackService;
			_userInfoRepository = userInfoRepository;
			_mapper = mapper;
		}
		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var userId = _httpContextAccessor.HttpContext.User.GetId();
			if (userId != Guid.Empty)
			{
				var userIpInfo = await _ipStackService
					.GetIpInfoAsync(
						_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
						cancellationToken
					);

				var userIpInfoDomain = _mapper.Map<UserIpInfo>(_mapper.Map<UserInfoDto>(userIpInfo));
				userIpInfoDomain.UserId = userId;

				await _userInfoRepository.CreateAsync(userIpInfoDomain, cancellationToken);
			}
			return await next();
		}
	}
}