using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Services.Extended.Pipeline;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Extended.Presenters
{

    public class SingleEntityPresenter<TEntity> : IPresenter<IQueryable<TEntity>>
    {

        #region - - - - - - Fields - - - - - -

        private readonly EntityID m_EntityID;
        private readonly Func<TEntity, EntityID> m_EntityIDFunction;
        private readonly IPresenter<TEntity> m_EntityPresenter;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public SingleEntityPresenter(EntityID entityID, Func<TEntity, EntityID> entityIDFunction, IPresenter<TEntity> entityPresenter)
        {
            this.m_EntityID = entityID ?? throw new ArgumentNullException(nameof(entityID));
            this.m_EntityIDFunction = entityIDFunction ?? throw new ArgumentNullException(nameof(entityIDFunction));
            this.m_EntityPresenter = entityPresenter ?? throw new ArgumentNullException(nameof(entityPresenter));
        }

        #endregion Constructors

        #region - - - - - - IPresenter Implementation - - - - - -

        public Task PresentAsync(IQueryable<TEntity> response, CancellationToken cancellationToken)
        {
            var _Entity = response.SingleOrDefault(entity => Equals(this.m_EntityIDFunction(entity), this.m_EntityID));
            return _Entity == null
                ? this.m_EntityPresenter.PresentEntityNotFoundAsync(this.m_EntityID, cancellationToken)
                : this.m_EntityPresenter.PresentAsync(_Entity, cancellationToken);
        }

        public Task PresentEntityNotFoundAsync(EntityID entityID, CancellationToken cancellationToken)
            => this.m_EntityPresenter.PresentEntityNotFoundAsync(entityID, cancellationToken);

        public Task PresentValidationFailureAsync(ValidationResult validationResult, CancellationToken cancellationToken)
            => this.m_EntityPresenter.PresentValidationFailureAsync(validationResult, cancellationToken);

        #endregion IPresenter Implementation

    }

}
