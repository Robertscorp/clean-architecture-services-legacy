using CleanArchitecture.Services.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Extended.Pipeline
{

    public abstract class AbstractValidator<TRequest> : FluentValidation.AbstractValidator<TRequest>, IRequestValidator<TRequest, ValidationResult>
    {

        #region - - - - - - IRequestValidator Implementation - - - - - -

        async Task<ValidationResult> IRequestValidator<TRequest, ValidationResult>.ValidateAsync(TRequest request, CancellationToken cancellationToken)
            => new ValidationResult(await base.ValidateAsync(request, cancellationToken));

        #endregion IRequestValidator Implementation

    }

}
