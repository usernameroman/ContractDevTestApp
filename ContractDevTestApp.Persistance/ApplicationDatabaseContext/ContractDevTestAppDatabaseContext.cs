using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ContractDevTestApp.Domain.Entities;
using ContractDevTestApp.Domain.Interfaces;
using ContractDevTestApp.Persistence.ApplicationDatabaseContext.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractDevTestApp.Persistence.ApplicationDatabaseContext
{
	public class ContractDevTestAppDatabaseContext: DbContext, IContractDevTestAppDatabaseContext
	{
		public ContractDevTestAppDatabaseContext(DbContextOptions options)
			: base(options)
		{
		}

		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<UserIpInfo> UserIpInfos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}

		public DbContext Context => this;

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			var newEntities = ChangeTracker.Entries()
				.Where(
					x => x.State == EntityState.Added && x.Entity is IHasCreatedAt
				)
				.Select(x => x.Entity as IHasCreatedAt);

			foreach (var newEntity in newEntities)
			{
				newEntity.CreatedAt = DateTimeOffset.UtcNow;
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}