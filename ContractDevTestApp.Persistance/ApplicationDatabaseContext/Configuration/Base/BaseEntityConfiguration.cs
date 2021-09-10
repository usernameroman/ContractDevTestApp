﻿using ContractDevTestApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractDevTestApp.Persistence.ApplicationDatabaseContext.Configuration.Base
{
	public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
		where TEntity : class, IBaseEntity
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder
				.HasKey(e => e.Id);

			builder
				.Property(e => e.Id)
				.ValueGeneratedOnAdd();
		}
	}
}