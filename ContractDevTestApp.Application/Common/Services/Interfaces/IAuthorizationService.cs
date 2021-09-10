using System.Diagnostics.CodeAnalysis;
using System.Threading;
using ContractDevTestApp.Application.Common.Models.Transient;
using ContractDevTestApp.Domain.Entities;

namespace ContractDevTestApp.Application.Common.Services.Interfaces
{
	public interface IAuthorizationService
	{
		TokenDto CreateToken(User user, CancellationToken cancellationToken);
		string HashPassword([NotNull] string password);
		bool VerifyPassword([NotNull] string plainPassword, [NotNull] string hashedPassword);
	}
}