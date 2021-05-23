using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Services.Extended.Validation
{

    public abstract class EntityIDValidator<TEntity> where TEntity : class, IEntity
    {

        #region - - - - - - Methods - - - - - -

        public abstract bool IsValidEntityID(EntityID entityID);

        #endregion Methods

    }

}
