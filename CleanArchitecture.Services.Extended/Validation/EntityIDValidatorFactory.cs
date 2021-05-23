using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Persistence;
using System;
using System.Linq;
using System.Threading;

namespace CleanArchitecture.Services.Extended.Validation
{

    public class EntityIDValidatorFactory : IEntityIDValidatorFactory
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public EntityIDValidatorFactory(IPersistenceContext persistenceContext)
            => this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));

        #endregion Constructors

        #region - - - - - - IEntityIDValidatorFactory Implementation - - - - - -

        public EntityIDValidator<TEntity> GetValidator<TEntity>() where TEntity : class, IEntity
            => new Validator<TEntity>(this.m_PersistenceContext);

        #endregion IEntityIDValidatorFactory Implementation

        #region - - - - - - Nested Classes - - - - - -

        private class Validator<TEntity> : EntityIDValidator<TEntity> where TEntity : class, IEntity
        {

            #region - - - - - - Fields - - - - - -

            private readonly IPersistenceContext m_PersistenceContext;

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            public Validator(IPersistenceContext persistenceContext)
                => this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));

            #endregion Constructors

            #region - - - - - - IEntityIDValidator Implementation - - - - - -

            public override bool IsValidEntityID(EntityID entityID)
                => this.m_PersistenceContext.GetEntitiesAsync<TEntity>(CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult().Any(e => Equals(e.ID, entityID));

            #endregion IEntityIDValidator Implementation

        }

        #endregion Nested Classes

    }

}
