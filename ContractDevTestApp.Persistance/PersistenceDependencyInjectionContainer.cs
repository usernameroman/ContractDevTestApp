using ContractDevTestApp.Domain.Interfaces;
using ContractDevTestApp.Persistence.ApplicationDatabaseContext;
using ContractDevTestApp.Persistence.ApplicationDatabaseContext.Interfaces;
using ContractDevTestApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContractDevTestApp.Persistence
{
	public static class PersistenceDependencyInjectionContainer
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<IContractDevTestAppDatabaseContext, ContractDevTestAppDatabaseContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

			return services;
		}
	}
}