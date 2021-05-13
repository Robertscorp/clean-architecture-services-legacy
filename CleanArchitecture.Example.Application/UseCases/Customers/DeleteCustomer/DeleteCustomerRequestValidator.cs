using FluentValidation;

namespace CleanArchitecture.Example.Application.UseCases.Customers.DeleteCustomer
{

    public class DeleteCustomerRequestValidator : Services.Extended.Pipeline.AbstractValidator<DeleteCustomerRequest>
    {

        #region - - - - - - Constructors - - - - - -

        public DeleteCustomerRequestValidator()
            => _ = this.RuleFor(r => r.CustomerID).NotNull();

        #endregion Constructors

    }

}
