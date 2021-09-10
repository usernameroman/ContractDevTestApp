using System;
using System.Security.Claims;
using ContractDevTestApp.Domain.Entities;

namespace ContractDevTestApp.Application.Common.Extensions
{
	public static class ClaimsExtensions
	{
		public static Guid GetId(this ClaimsPrincipal user)
		{
			Guid.TryParse(user.FindFirst(nameof(User.Id))?.Value, out var userId);
			return userId;
		}

	}
}