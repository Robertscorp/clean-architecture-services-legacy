using CleanArchitecture.Services.Extended.FluentValidation;
using FluentValidation;

namespace CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerRequestValidator : Validator<CreateCustomerRequest>
    {

        #region - - - - - - Constructors - - - - - -

        public CreateCustomerRequestValidator()
        {
            _ = this.RuleFor(r => r.EmailAddress).EmailAddress().NotEmpty().MaximumLength(250);
            _ = this.RuleFor(r => r.FirstName).NotEmpty().MaximumLength(50);
            _ = this.RuleFor(r => r.GenderID).NotNull();
            _ = this.RuleFor(r => r.LastName).NotEmpty().MaximumLength(50);
            _ = this.RuleFor(r => r.MobileNumber).NotEmpty().MaximumLength(20);
        }

        #endregion Constructors

    }

}
