using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Example.Application.Extensions;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Domain.Entities;
using FluentValidation;

namespace CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerRequestValidator : Validator<CreateCustomerRequest>
    {

        #region - - - - - - Constructors - - - - - -

        public CreateCustomerRequestValidator(EntityIDValidator<Gender> genderIDValidator)
        {
            _ = this.RuleFor(r => r.EmailAddress).EmailAddress().NotEmpty().MaximumLength(250);
            _ = this.RuleFor(r => r.FirstName).NotEmpty().MaximumLength(50);
            _ = this.RuleFor(r => r.GenderID).SetValidator(genderIDValidator);
            _ = this.RuleFor(r => r.LastName).NotEmpty().MaximumLength(50);
            _ = this.RuleFor(r => r.MobileNumber).NotEmpty().MaximumLength(20);
        }

        #endregion Constructors

    }



}
