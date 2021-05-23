using CleanArchitecture.Example.Application.Tests.Support;
using CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer;
using CleanArchitecture.Example.Domain.Entities;
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

        private readonly CreateCustomerRequestValidator m_Validator;
        private readonly TestEntityIDValidatorFactory m_ValidatorFactory = new();

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateCustomerRequestValidatorTests()
            => this.m_Validator = new(this.m_ValidatorFactory);

        #endregion Constructors

        #region - - - - - - CreateCustomerRequestValidator Tests - - - - - -

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_100 + LENGTH_OF_100 + LENGTH_OF_50)]
        [InlineData(LENGTH_OF_100 + LENGTH_OF_100 + LENGTH_OF_40 + "@23456.com1")]
        public void Validate_InvalidEmailAddress_ShouldHaveValidationError(string emailAddress)
            => this.m_Validator.ShouldHaveValidationErrorFor(r => r.EmailAddress, emailAddress);

        [Theory]
        [InlineData("a@a")]
        [InlineData(LENGTH_OF_100 + LENGTH_OF_100 + LENGTH_OF_40 + "@23456.com")]
        public void Validate_ValidEmailAddress_ShouldNotHaveValidationError(string emailAddress)
            => this.m_Validator.ShouldNotHaveValidationErrorFor(r => r.EmailAddress, emailAddress);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_50 + "1")]
        public void Validate_InvalidFirstName_ShouldHaveValidationError(string firstName)
            => this.m_Validator.ShouldHaveValidationErrorFor(r => r.FirstName, firstName);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_50)]
        public void Validate_ValidFirstName_ShouldNotHaveValidationError(string firstName)
            => this.m_Validator.ShouldNotHaveValidationErrorFor(r => r.FirstName, firstName);

        [Fact]
        public void Validate_InvalidGenderID_ShouldHaveValidationError()
            => this.m_Validator.ShouldHaveValidationErrorFor(r => r.GenderID, default(EntityID));

        [Fact]
        public void Validate_ValidGenderID_ShouldNotHaveValidationError()
            => this.m_Validator.ShouldNotHaveValidationErrorFor(r => r.GenderID, this.m_ValidatorFactory.GetExistingEntityID<Gender>());

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_50 + "1")]
        public void Validate_InvalidLastName_ShouldHaveValidationError(string lastName)
            => this.m_Validator.ShouldHaveValidationErrorFor(r => r.LastName, lastName);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_50)]
        public void Validate_ValidLastName_ShouldNotHaveValidationError(string lastName)
            => this.m_Validator.ShouldNotHaveValidationErrorFor(r => r.LastName, lastName);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_20 + "1")]
        public void Validate_InvalidMobileNumber_ShouldHaveValidationError(string mobileNumber)
            => this.m_Validator.ShouldHaveValidationErrorFor(r => r.MobileNumber, mobileNumber);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_20)]
        public void Validate_ValidMobileNumber_ShouldNotHaveValidationError(string mobileNumber)
            => this.m_Validator.ShouldNotHaveValidationErrorFor(r => r.MobileNumber, mobileNumber);

        #endregion CreateCustomerRequestValidator Tests

    }

}
