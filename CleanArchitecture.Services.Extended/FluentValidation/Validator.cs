using CleanArchitecture.Services.Pipeline;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Extended.FluentValidation
{

    public abstract class Validator<TRequest> : AbstractValidator<TRequest>, IRequestValidator<TRequest, ValidationResult>
    {

        #region - - - - - - IRequestValidator Implementation - - - - - -

        async Task<ValidationResult> IRequestValidator<TRequest, ValidationResult>.ValidateAsync(TRequest request, CancellationToken cancellationToken)
            => new ValidationResult(await base.ValidateAsync(request, cancellationToken));

        #endregion IRequestValidator Implementation

    }

}
