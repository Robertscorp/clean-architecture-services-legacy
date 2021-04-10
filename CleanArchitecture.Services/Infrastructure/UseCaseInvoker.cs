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

        public async Task InvokeUseCaseAsync<TRequest, TResponse, TValidationResult>(TRequest request, IPresenter<TResponse, TValidationResult> presenter, CancellationToken cancellationToken)
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

            var _UseCaseInteractor = this.m_ServiceProvider.GetService(typeof(IUseCaseInteractor<TRequest, TResponse, TValidationResult>));
            await ((IUseCaseInteractor<TRequest, TResponse, TValidationResult>)_UseCaseInteractor).HandleAsync(request, presenter, cancellationToken);
        }

        #endregion IUseCaseInvoker Implementation

    }

}
