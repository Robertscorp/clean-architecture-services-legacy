using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Application.Services
{

    public class ValidationResult : FluentValidation.Results.ValidationResult, IValidationResult
    {
    }

}
