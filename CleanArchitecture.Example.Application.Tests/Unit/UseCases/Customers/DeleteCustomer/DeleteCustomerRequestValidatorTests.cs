using CleanArchitecture.Example.Application.UseCases.Customers.DeleteCustomer;
using CleanArchitecture.Services.Entities;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Customers.DeleteCustomer
{

    public class DeleteCustomerRequestValidatorTests
    {

        #region - - - - - - DeleteCustomerRequestValidator Tests - - - - - -

        [Fact]
        public void Validate_InvalidCustomerID_ShouldHaveValidationError()
            => new DeleteCustomerRequestValidator().ShouldHaveValidationErrorFor(r => r.CustomerID, default(EntityID));

        [Fact]
        public void Validate_ValidCustomerID_ShouldNotHaveValidationError()
            => new DeleteCustomerRequestValidator().ShouldNotHaveValidationErrorFor(r => r.CustomerID, new Mock<EntityID>().Object);

        #endregion DeleteCustomerRequestValidator Tests

    }

}
