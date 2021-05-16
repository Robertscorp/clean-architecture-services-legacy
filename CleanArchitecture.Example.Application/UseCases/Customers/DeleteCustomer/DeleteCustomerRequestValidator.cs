using CleanArchitecture.Services.Extended.FluentValidation;
using FluentValidation;

namespace CleanArchitecture.Example.Application.UseCases.Customers.DeleteCustomer
{

    public class DeleteCustomerRequestValidator : Validator<DeleteCustomerRequest>
    {

        #region - - - - - - Constructors - - - - - -

        public DeleteCustomerRequestValidator()
            => _ = this.RuleFor(r => r.CustomerID).NotNull();

        #endregion Constructors

    }

}
