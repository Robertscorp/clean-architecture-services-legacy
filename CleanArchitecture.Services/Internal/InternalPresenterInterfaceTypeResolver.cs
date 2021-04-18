using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;
using System;
using System.Linq;

namespace CleanArchitecture.Services.Internal
{

    public abstract class InternalPresenterInterfaceTypeResolver
    {

        #region - - - - - - Methods - - - - - -

        public abstract Type GetPresenterInterfaceType(Type presenterType);

        #endregion Methods

    }

    public class InternalPresenterInterfaceTypeResolver<TResponse, TValidationResult> : InternalPresenterInterfaceTypeResolver
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
