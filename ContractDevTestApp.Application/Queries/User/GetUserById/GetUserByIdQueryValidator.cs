using FluentValidation;

namespace ContractDevTestApp.Application.Queries.User.GetUserById
{
	public class GetUserByIdQueryValidator:AbstractValidator<GetUserByIdQuery>
	{
		public GetUserByIdQueryValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				;
		}
	}
}