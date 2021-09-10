using System;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ContractDevTestApp.Domain.Interfaces
{
	public interface IRepository<TEntity> where TEntity : IBaseEntity
	{
		Task<IImmutableList<TEntity>> GetBy(Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken);
		Task<IImmutableList<TEntity>> GetAll(CancellationToken cancellationToken);

		Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

		Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

		Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
	}
}