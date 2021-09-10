using System;
using System.Collections.Immutable;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ContractDevTestApp.Application.Commands.User.AddUser;
using ContractDevTestApp.Application.Commands.User.DeleteUser;
using ContractDevTestApp.Application.Commands.User.UpdateUser;
using ContractDevTestApp.Application.Common.Models.Persistent;
using ContractDevTestApp.Application.Queries.User.GetAllUsers;
using ContractDevTestApp.Application.Queries.User.GetUserById;
using ContractDevTestApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractDevTestApp.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Create new User
		/// </summary>
		/// <param name="addUserCommand"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize]
		[ProducesResponseType(typeof(CommonResponseDto<UserDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.UnprocessableEntity)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.InternalServerError)]
		public async Task<CommonResponseDto<UserDto>> Create(
			[FromBody] AddUserCommand addUserCommand, CancellationToken cancellationToken)
		{
			return new CommonResponseDto<UserDto>()
			{
				Data = await _mediator.Send(addUserCommand, cancellationToken),
				Status = "Success"
			};
		}

		/// <summary>
		/// Get all users
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet()]
		[Authorize]
		[ProducesResponseType(typeof(CommonResponseDto<IImmutableList<UserDto>>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.InternalServerError)]
		public async Task<CommonResponseDto<IImmutableList<UserDto>>> GetPredefined(CancellationToken cancellationToken)
		{
			return new CommonResponseDto<IImmutableList<UserDto>>()
			{
				Data = await _mediator.Send(new GetAllUsersQuery(), cancellationToken),
				Status = "Success"
			};
		}

		/// <summary>
		/// Get user by id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		[Authorize]
		[ProducesResponseType(typeof(CommonResponseDto<UserDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.InternalServerError)]
		public async Task<CommonResponseDto<UserDto>> GetById([FromRoute]Guid id, CancellationToken cancellationToken)
		{
			return new CommonResponseDto<UserDto>()
			{
				Data = await _mediator.Send(new GetUserByIdQuery(){Id = id}, cancellationToken),
				Status = "Success"
			};
		}

		/// <summary>
		/// Update existing user
		/// </summary>
		/// <param name="updateUserCommand"></param>
		/// <param name="cancelationToken"></param>
		/// <returns></returns>
		[HttpPut]
		[Authorize]
		[ProducesResponseType(typeof(CommonResponseDto<UserDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.UnprocessableEntity)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.InternalServerError)]
		public async Task<CommonResponseDto<UserDto>> Update(
			[FromBody] UpdateUserCommand updateUserCommand, CancellationToken cancelationToken)
		{
			return new CommonResponseDto<UserDto>()
			{
				Data = await _mediator.Send(updateUserCommand, cancelationToken),
				Status = "Success"
			};
		}


		/// <summary>
		/// Delete User
		/// </summary>
		/// <param name="id">User Id</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		[Authorize]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.UnprocessableEntity)]
		[ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.InternalServerError)]
		public async Task<CommonResponseDto> Delete(
			[FromRoute] Guid id, CancellationToken cancellationToken)
		{
			await _mediator.Send(new DeleteUserCommand() { Id = id }, cancellationToken);
			return new CommonResponseDto()
			{
				Status = "Success"
			};
		}
	}
}
