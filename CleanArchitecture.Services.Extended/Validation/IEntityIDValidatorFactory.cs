using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Services.Extended.Validation
{

    public interface IEntityIDValidatorFactory
    {

        #region - - - - - - Methods - - - - - -

        EntityIDValidator<TEntity> GetValidator<TEntity>() where TEntity : class, IEntity;

        #endregion Methods

    }

}
