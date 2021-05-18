using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Persistence;
using System;
using System.Threading;

namespace CleanArchitecture.Example.Application.Services.Pipeline
{

    public class EntityIDValidator<TEntity> where TEntity : class
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public EntityIDValidator(IPersistenceContext persistenceContext)
            => this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public bool IsValidEntityID(EntityID entityID)
            => this.m_PersistenceContext.ExistsAsync<TEntity>(entityID, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();

        #endregion Methods

    }

}
