using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Application.Services.Pipeline
{

    public class ValidationResult : FluentValidation.Results.ValidationResult, IValidationResult
    {
    }

}
