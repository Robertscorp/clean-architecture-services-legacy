using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Services.Extended.Validation;
using FluentValidation;

namespace CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerRequestValidator : Validator<CreateCustomerRequest>
    {

        #region - - - - - - Constructors - - - - - -

        public CreateCustomerRequestValidator(IEntityIDValidatorFactory entityIDValidatorFactory)
        {
            _ = this.RuleFor(r => r.EmailAddress).EmailAddress().NotEmpty().MaximumLength(250);
            _ = this.RuleFor(r => r.FirstName).NotEmpty().MaximumLength(50);
            _ = this.RuleFor(r => r.GenderID).SetValidator(entityIDValidatorFactory.GetValidator<Gender>());
            _ = this.RuleFor(r => r.LastName).NotEmpty().MaximumLength(50);
            _ = this.RuleFor(r => r.MobileNumber).NotEmpty().MaximumLength(20);
        }

        #endregion Constructors

    }

}
