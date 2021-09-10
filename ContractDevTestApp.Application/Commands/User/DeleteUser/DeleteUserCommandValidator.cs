using FluentValidation;

namespace ContractDevTestApp.Application.Commands.User.DeleteUser
{
	public class DeleteUserCommandValidator:AbstractValidator<DeleteUserCommand>
	{
		public DeleteUserCommandValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				;
		}
	}
}