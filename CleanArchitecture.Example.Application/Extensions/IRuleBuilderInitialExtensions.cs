using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Services.Entities;
using FluentValidation;

namespace CleanArchitecture.Example.Application.Extensions
{

    public static class IRuleBuilderInitialExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static IRuleBuilderOptions<T, TEntityID> SetValidator<T, TEntity, TEntityID>(this IRuleBuilder<T, TEntityID> ruleBuilder, EntityIDValidator<TEntity> entityIDValidator, string entityName = null)
            where TEntity : class
            where TEntityID : EntityID
            => ruleBuilder.Must(id => entityIDValidator.IsValidEntityID(id)).WithMessage((request, id) => $"'{id}' is not a valid {entityName ?? typeof(TEntity).Name} ID.");

        #endregion Methods

    }

}
