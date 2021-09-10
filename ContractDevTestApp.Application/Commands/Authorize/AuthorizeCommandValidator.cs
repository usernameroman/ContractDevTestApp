using FluentValidation;

namespace ContractDevTestApp.Application.Commands.Authorize
{
	public class AuthorizeCommandValidator : AbstractValidator<AuthorizeCommand>
	{
		public AuthorizeCommandValidator()
		{
			RuleFor(x => x.Login)
				.NotNull()
				.NotEmpty()
				;


			RuleFor(x => x.Password)
				.NotNull()
				.NotEmpty()
				;
		}
	}
}