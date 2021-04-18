using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Internal;
using CleanArchitecture.Services.Pipeline;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Infrastructure
{

    public class UseCaseInvoker : IUseCaseInvoker
    {

        #region - - - - - - Fields - - - - - -

        private readonly IServiceProvider m_ServiceProvider;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public UseCaseInvoker(IServiceProvider serviceProvider)
            => this.m_ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        #endregion Constructors

        #region - - - - - - IUseCaseInvoker Implementation - - - - - -

        public Task InvokeUseCaseAsync<TResponse, TValidationResult>(IUseCaseRequest<TResponse> request, IPresenter<TResponse, TValidationResult> presenter, CancellationToken cancellationToken)
            where TValidationResult : IValidationResult
        {
            var _InternalPresentationInterfaceTypeResolverType = typeof(InternalPresenterInterfaceTypeResolver<,>).MakeGenericType(typeof(TResponse), typeof(TValidationResult));
            var _InternalPresentationInterfaceTypeResolver = (InternalPresenterInterfaceTypeResolver)Activator.CreateInstance(_InternalPresentationInterfaceTypeResolverType);

            var _PresentationInterfaceType = _InternalPresentationInterfaceTypeResolver.GetPresenterInterfaceType(presenter.GetType());
            var _InternalUseCaseInvokerType = typeof(InternalUseCaseInvoker<,,,>).MakeGenericType(_PresentationInterfaceType, request.GetType(), typeof(TResponse), typeof(TValidationResult));
            var _InternalUseCaseInvoker = (InternalUseCaseInvoker)Activator.CreateInstance(_InternalUseCaseInvokerType, presenter, request, this.m_ServiceProvider);

            return _InternalUseCaseInvoker.InvokeUseCaseAsync(cancellationToken);
        }



        #endregion IUseCaseInvoker Implementation

    }

}
