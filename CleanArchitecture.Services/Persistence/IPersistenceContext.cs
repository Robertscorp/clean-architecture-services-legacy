using CleanArchitecture.Services.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Persistence
{

    public interface IPersistenceContext
    {

        #region - - - - - - Methods - - - - - -

        Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        Task<TEntity> FindAsync<TEntity>(EntityID entityID, CancellationToken cancellationToken) where TEntity : class;

        Task<IQueryable<TEntity>> GetEntitiesAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class;

        Task<EntityID> GetEntityIDAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        Task RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        #endregion Methods

    }

}
