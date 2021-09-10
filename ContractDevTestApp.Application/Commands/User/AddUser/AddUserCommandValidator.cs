using FluentValidation;

namespace ContractDevTestApp.Application.Commands.User.AddUser
{
	public class AddUserCommandValidator:AbstractValidator<AddUserCommand>
	{
		public AddUserCommandValidator()
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