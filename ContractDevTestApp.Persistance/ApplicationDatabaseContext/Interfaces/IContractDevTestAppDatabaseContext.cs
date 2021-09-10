using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ContractDevTestApp.Persistence.ApplicationDatabaseContext.Interfaces
{
	public interface IContractDevTestAppDatabaseContext
	{
		DbContext Context { get; }
		DatabaseFacade Database { get; }
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}