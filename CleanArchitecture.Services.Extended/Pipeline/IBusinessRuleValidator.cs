using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Services.Extended.Pipeline
{

    public interface IBusinessRuleValidator<TRequest> : IBusinessRuleValidator<TRequest, ValidationResult>
    {
    }

}
