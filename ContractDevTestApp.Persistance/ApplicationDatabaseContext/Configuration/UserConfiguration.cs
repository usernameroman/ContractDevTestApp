using ContractDevTestApp.Domain.Entities;
using ContractDevTestApp.Persistence.ApplicationDatabaseContext.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ContractDevTestApp.Persistence.ApplicationDatabaseContext.Configuration
{
	public class UserConfiguration : BaseEntityConfiguration<User>
	{
		public override void Configure(EntityTypeBuilder<User> builder)
		{
			base.Configure(builder);

			builder.HasData(
				new User()
				{
					Id = Guid.Parse("4BE16324-D72E-45C0-901D-F92C815DAFC6"),
					Login = "SeededUser",
					Password = "$2a$11$FahyvRctLiW0vxnsMcrLxuLvjNNn3zIcjgpZscCxY7opqOOoG2QiW" //1111111
				}
			);
		}
	}
}