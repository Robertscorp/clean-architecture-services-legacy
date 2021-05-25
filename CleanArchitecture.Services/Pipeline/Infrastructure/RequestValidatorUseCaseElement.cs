using CleanArchitecture.Services.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline.Infrastructure
{

    public class RequestValidatorUseCaseElement<TResponse, TValidationResult> : IUseCaseElement<TResponse, TValidationResult> where TValidationResult : IValidationResult
    {

        #region - - - - - - Fields - - - - - -

        private readonly IServiceProvider m_ServiceProvider;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public RequestValidatorUseCaseElement(IServiceProvider serviceProvider)
            => this.m_ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        #endregion Constructors

        #region - - - - - - IUseCaseElement Implementation - - - - - -

        public async Task<bool> TryPresentResultAsync<TPresenter, TRequest>(TRequest request, TPresenter presenter, CancellationToken cancellationToken)
            where TPresenter : IPresenter<TResponse, TValidationResult>
            where TRequest : IUseCaseRequest<TResponse>
        {
            var _RequestValidator = (IRequestValidator<TRequest, TValidationResult>)this.m_ServiceProvider.GetService(typeof(IRequestValidator<TRequest, TValidationResult>));
            if (_RequestValidator != null)
            {
                var _ValidationResult = await _RequestValidator.ValidateAsync(request, cancellationToken);
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
