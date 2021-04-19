using CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer;
using CleanArchitecture.Services.Entities;
using FluentValidation.TestHelper;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerRequestValidatorTests
    {

        #region - - - - - - Fields - - - - - -

        private const string LENGTH_OF_10 = "1234567890";
        private const string LENGTH_OF_100 = LENGTH_OF_50 + LENGTH_OF_50;
        private const string LENGTH_OF_20 = LENGTH_OF_10 + LENGTH_OF_10;
        private const string LENGTH_OF_40 = LENGTH_OF_20 + LENGTH_OF_20;
        private const string LENGTH_OF_50 = LENGTH_OF_40 + LENGTH_OF_10;

        #endregion Fields

        #region - - - - - - CreateCustomerRequestValidator Tests - - - - - -

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_100 + LENGTH_OF_100 + LENGTH_OF_50)]
        [InlineData(LENGTH_OF_100 + LENGTH_OF_100 + LENGTH_OF_40 + "@23456.com1")]
        public void Validate_InvalidEmailAddress_ShouldHaveValidationError(string emailAddress)
            => new CreateCustomerRequestValidator().ShouldHaveValidationErrorFor(r => r.EmailAddress, emailAddress);

        [Theory]
        [InlineData("a@a")]
        [InlineData(LENGTH_OF_100 + LENGTH_OF_100 + LENGTH_OF_40 + "@23456.com")]
        public void Validate_ValidEmailAddress_ShouldNotHaveValidationError(string emailAddress)
            => new CreateCustomerRequestValidator().ShouldNotHaveValidationErrorFor(r => r.EmailAddress, emailAddress);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_50 + "1")]
        public void Validate_InvalidFirstName_ShouldHaveValidationError(string firstName)
            => new CreateCustomerRequestValidator().ShouldHaveValidationErrorFor(r => r.FirstName, firstName);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_50)]
        public void Validate_ValidFirstName_ShouldNotHaveValidationError(string firstName)
            => new CreateCustomerRequestValidator().ShouldNotHaveValidationErrorFor(r => r.FirstName, firstName);

        [Fact]
        public void Validate_InvalidGenderID_ShouldHaveValidationError()
            => new CreateCustomerRequestValidator().ShouldHaveValidationErrorFor(r => r.GenderID, default(EntityID));

        [Fact]
        public void Validate_ValidGenderID_ShouldNotHaveValidationError()
            => new CreateCustomerRequestValidator().ShouldNotHaveValidationErrorFor(r => r.GenderID, new EntityID());

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_50 + "1")]
        public void Validate_InvalidLastName_ShouldHaveValidationError(string lastName)
            => new CreateCustomerRequestValidator().ShouldHaveValidationErrorFor(r => r.LastName, lastName);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_50)]
        public void Validate_ValidLastName_ShouldNotHaveValidationError(string lastName)
            => new CreateCustomerRequestValidator().ShouldNotHaveValidationErrorFor(r => r.LastName, lastName);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_20 + "1")]
        public void Validate_InvalidMobileNumber_ShouldHaveValidationError(string mobileNumber)
            => new CreateCustomerRequestValidator().ShouldHaveValidationErrorFor(r => r.MobileNumber, mobileNumber);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_20)]
        public void Validate_ValidMobileNumber_ShouldNotHaveValidationError(string mobileNumber)
            => new CreateCustomerRequestValidator().ShouldNotHaveValidationErrorFor(r => r.MobileNumber, mobileNumber);

        #endregion CreateCustomerRequestValidator Tests

    }
}
