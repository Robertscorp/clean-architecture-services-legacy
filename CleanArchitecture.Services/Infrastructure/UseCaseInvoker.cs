using CleanArchitecture.Services.Entities;
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

        public async Task InvokeUseCaseAsync<TPresenter, TRequest, TResponse, TValidationResult>(TRequest request, TPresenter presenter, CancellationToken cancellationToken)
            where TPresenter : IPresenter<TResponse, TValidationResult>
            where TRequest : IUseCaseRequest<TResponse>
            where TValidationResult : IValidationResult
        {
            var _BusinessRuleValidator = (IBusinessRuleValidator<TRequest, TValidationResult>)this.m_ServiceProvider.GetService(typeof(IBusinessRuleValidator<TRequest, TValidationResult>));
            if (_BusinessRuleValidator != null)
            {
                var _ValidationResult = await _BusinessRuleValidator.ValidateAsync(request, cancellationToken);
                if (!_ValidationResult.IsValid)
                {
                    await presenter.PresentValidationFailureAsync(_ValidationResult, cancellationToken);
                    return;
                }
            }

            var _UseCaseInteractor = this.m_ServiceProvider.GetService(typeof(IUseCaseInteractor<TPresenter, TRequest, TResponse, TValidationResult>));
            await ((IUseCaseInteractor<TPresenter, TRequest, TResponse, TValidationResult>)_UseCaseInteractor).HandleAsync(request, presenter, cancellationToken);
        }

        #endregion IUseCaseInvoker Implementation

    }

}
