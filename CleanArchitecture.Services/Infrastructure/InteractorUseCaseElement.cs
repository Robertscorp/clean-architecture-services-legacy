using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Infrastructure
{

    public class InteractorUseCaseElement<TResponse, TValidationResult> : IUseCaseElement<TResponse, TValidationResult> where TValidationResult : IValidationResult
    {

        #region - - - - - - Fields - - - - - -

        private readonly IServiceProvider m_ServiceProvider;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public InteractorUseCaseElement(IServiceProvider serviceProvider)
            => this.m_ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        #endregion Constructors

        #region - - - - - - IUseCaseElement Implementation - - - - - -

        public async Task<bool> TryPresentResultAsync<TPresenter, TRequest>(TRequest request, TPresenter presenter, CancellationToken cancellationToken)
            where TPresenter : IPresenter<TResponse, TValidationResult>
            where TRequest : IUseCaseRequest<TResponse>
        {

            var _UseCaseInteractor = this.m_ServiceProvider.GetService(typeof(IUseCaseInteractor<TPresenter, TRequest, TResponse, TValidationResult>));
            await ((IUseCaseInteractor<TPresenter, TRequest, TResponse, TValidationResult>)_UseCaseInteractor).HandleAsync(request, presenter, cancellationToken);
            return true;
        }

        #endregion IUseCaseElement Implementation

    }

}
