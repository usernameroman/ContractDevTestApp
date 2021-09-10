using System;
using ContractDevTestApp.Domain.Entities;
using ContractDevTestApp.Persistence.ApplicationDatabaseContext.Configuration.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ContractDevTestApp.Persistence.ApplicationDatabaseContext.Configuration
{
	public class UserIpInfoConfiguration : BaseEntityConfiguration<UserIpInfo>
	{
		public override void Configure(EntityTypeBuilder<UserIpInfo> builder)
		{
			base.Configure(builder);

			builder
				.HasOne(x => x.User)
				.WithMany(x => x.UserIpInfos)
				.HasForeignKey(x => x.UserId);
		}
	}
}