using CleanArchitecture.Services.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline.Infrastructure
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
            var _PresenterInterfaceTypeResolverType = typeof(PresenterInterfaceTypeResolver<,>).MakeGenericType(typeof(TResponse), typeof(TValidationResult));
            var _PresenterInterfaceTypeResolver = (PresenterInterfaceTypeResolver)Activator.CreateInstance(_PresenterInterfaceTypeResolverType);

            var _PresentationInterfaceType = _PresenterInterfaceTypeResolver.GetPresenterInterfaceType(presenter.GetType());
            var _UseCaseInvokerInternalType = typeof(UseCaseInvokerInternal<,,,>).MakeGenericType(_PresentationInterfaceType, request.GetType(), typeof(TResponse), typeof(TValidationResult));
            var _UseCaseInvokerInternal = (UseCaseInvokerInternal)Activator.CreateInstance(_UseCaseInvokerInternalType, presenter, request, this.m_ServiceProvider);

            return _UseCaseInvokerInternal.InvokeUseCaseAsync(cancellationToken);
        }

        #endregion IUseCaseInvoker Implementation

    }

    public abstract class UseCaseInvokerInternal
    {

        #region - - - - - - Methods - - - - - -

        public abstract Task InvokeUseCaseAsync(CancellationToken cancellationToken);

        #endregion Methods

    }

    public class UseCaseInvokerInternal<TPresenter, TRequest, TResponse, TValidationResult> : UseCaseInvokerInternal
        where TPresenter : IPresenter<TResponse, TValidationResult>
        where TRequest : IUseCaseRequest<TResponse>
        where TValidationResult : IValidationResult
    {

        #region - - - - - - Fields - - - - - -

        private readonly TPresenter m_Presenter;
        private readonly TRequest m_Request;
        private readonly IServiceProvider m_ServiceProvider;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public UseCaseInvokerInternal(TPresenter presenter, TRequest request, IServiceProvider serviceProvider)
        {
            this.m_Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            this.m_Request = request ?? throw new ArgumentNullException(nameof(request));
            this.m_ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public override async Task InvokeUseCaseAsync(CancellationToken cancellationToken)
        {
            var _UseCaseElements = (IEnumerable<IUseCaseElement<TResponse, TValidationResult>>)this.m_ServiceProvider.GetService(typeof(IEnumerable<IUseCaseElement<TResponse, TValidationResult>>));

            foreach (var _UseCaseElement in _UseCaseElements)
                if (await _UseCaseElement.TryPresentResultAsync(this.m_Request, this.m_Presenter, cancellationToken))
                    return;
        }

        #endregion Methods

    }

}
