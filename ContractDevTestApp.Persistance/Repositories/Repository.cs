using ContractDevTestApp.Domain.Interfaces;
using ContractDevTestApp.Persistence.ApplicationDatabaseContext.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ContractDevTestApp.Persistence.Extensions;

namespace ContractDevTestApp.Persistence.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
	{
		private readonly IContractDevTestAppDatabaseContext _databaseContext;

		public Repository(IContractDevTestAppDatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public async Task<IImmutableList<TEntity>> GetBy(Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken)
		{
			return (await QueryWithAllIncludes.Where(filterBy).ToListAsync(cancellationToken)).ToImmutableList();
		}

		public async Task<IImmutableList<TEntity>> GetAll(CancellationToken cancellationToken)
		{
			return (await QueryWithAllIncludes.ToListAsync(cancellationToken)).ToImmutableList();
		}

		public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			return await QueryWithAllIncludes.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
		}

		public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			await _databaseContext.Set<TEntity>().AddAsync(entity, cancellationToken);
			await _databaseContext.SaveChangesAsync(cancellationToken);
			return entity;
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_databaseContext.Set<TEntity>().Update(entity);
			await _databaseContext.SaveChangesAsync(cancellationToken);
			return entity;
		}

		public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_databaseContext.Set<TEntity>().Remove(entity);
			await _databaseContext.SaveChangesAsync(cancellationToken);
		}

		private IQueryable<TEntity> QueryWithAllIncludes => _databaseContext.Set<TEntity>()
				.Include(_databaseContext.Context.GetIncludePaths(typeof(TEntity)));
	}
}