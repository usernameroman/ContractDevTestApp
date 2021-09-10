using FluentValidation;

namespace ContractDevTestApp.Application.Commands.User.UpdateUser
{
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				;
		}
	}
}