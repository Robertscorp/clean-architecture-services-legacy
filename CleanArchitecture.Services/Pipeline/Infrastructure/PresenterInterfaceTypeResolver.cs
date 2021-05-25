using CleanArchitecture.Services.Entities;
using System;
using System.Linq;

namespace CleanArchitecture.Services.Pipeline.Infrastructure
{

    public abstract class PresenterInterfaceTypeResolver
    {

        #region - - - - - - Methods - - - - - -

        public abstract Type GetPresenterInterfaceType(Type presenterType);

        #endregion Methods

    }

    public class PresenterInterfaceTypeResolver<TResponse, TValidationResult> : PresenterInterfaceTypeResolver
        where TValidationResult : IValidationResult
    {

        #region - - - - - - Methods - - - - - -

        public override Type GetPresenterInterfaceType(Type presenterType)
            => typeof(IPresenter<TResponse, TValidationResult>).IsAssignableFrom(presenterType.BaseType)
                ? this.GetPresenterInterfaceType(presenterType.BaseType)
                : presenterType.GetInterfaces().First(i => typeof(IPresenter<TResponse, TValidationResult>).IsAssignableFrom(i));

        #endregion Methods

    }

}
