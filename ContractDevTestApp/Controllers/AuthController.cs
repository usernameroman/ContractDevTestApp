using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ContractDevTestApp.Application.Commands.Authorize;
using ContractDevTestApp.Application.Common.Models.Transient;
using ContractDevTestApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractDevTestApp.Controllers
{
	[Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(
	        IMediator mediator
            )
        {
	        _mediator = mediator;
        }

        /// <summary>
        /// Authorize user
        /// </summary>
        /// <param name="authorizeCommand">View model with authentication params</param>
        /// <param name="cancellationToken">Token can be passed to interrupt request</param>
        /// <returns>Token, its expiration date, user info</returns>
        [HttpPost("token")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CommonResponseDto<CommonResponseDto<TokenDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(CommonResponseDto), (int)HttpStatusCode.InternalServerError)]
        public async Task<CommonResponseDto<TokenDto>> Authorize([FromBody] AuthorizeCommand authorizeCommand, CancellationToken cancellationToken)
        {
            return new CommonResponseDto<TokenDto>()
            {
                Data = await _mediator.Send(authorizeCommand, cancellationToken),
                Status = "Success"
            };
        }
    }
}
