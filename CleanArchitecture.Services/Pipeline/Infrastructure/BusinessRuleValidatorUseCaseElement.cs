using CleanArchitecture.Services.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline.Infrastructure
{

    public class BusinessRuleValidatorUseCaseElement<TResponse, TValidationResult> : IUseCaseElement<TResponse, TValidationResult> where TValidationResult : IValidationResult
    {

        #region - - - - - - Fields - - - - - -

        private readonly IServiceProvider m_ServiceProvider;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public BusinessRuleValidatorUseCaseElement(IServiceProvider serviceProvider)
            => this.m_ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        #endregion Constructors

        #region - - - - - - IUseCaseElement Implementation - - - - - -

        public async Task<bool> TryPresentResultAsync<TPresenter, TRequest>(TRequest request, TPresenter presenter, CancellationToken cancellationToken)
            where TPresenter : IPresenter<TResponse, TValidationResult>
            where TRequest : IUseCaseRequest<TResponse>
        {
            var _BusinessRuleValidator = (IBusinessRuleValidator<TRequest, TValidationResult>)this.m_ServiceProvider.GetService(typeof(IBusinessRuleValidator<TRequest, TValidationResult>));
            if (_BusinessRuleValidator != null)
            {
                var _ValidationResult = await _BusinessRuleValidator.ValidateAsync(request, cancellationToken);
                if (!_ValidationResult.IsValid)
                {
                    await presenter.PresentValidationFailureAsync(_ValidationResult, cancellationToken);
                    return true;
                }
            }

            return false;
        }

        #endregion IUseCaseElement Implementation

    }

}
