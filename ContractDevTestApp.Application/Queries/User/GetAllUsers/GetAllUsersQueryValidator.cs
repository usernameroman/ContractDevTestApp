using FluentValidation;

namespace ContractDevTestApp.Application.Queries.User.GetAllUsers
{
	public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
	{
		public GetAllUsersQueryValidator()
		{
			// Query has no params
		}
	}
}