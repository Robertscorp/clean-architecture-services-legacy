using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Example.Application.Services
{

    public interface IBusinessRuleValidator<TRequest> : IBusinessRuleValidator<TRequest, ValidationResult>
    {
    }

}
