using FluentValidation;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Customers.DeleteCustomer
{

    public class DeleteCustomerRequestValidator : Services.Pipeline.AbstractValidator<DeleteCustomerRequest>
    {

        #region - - - - - - Constructors - - - - - -

        public DeleteCustomerRequestValidator()
            => _ = this.RuleFor(r => r.CustomerID).NotNull();

        #endregion Constructors

    }

}
