using CleanArchitecture.Example.Application.UseCases.Users.CreateUser;
using CleanArchitecture.Services.Entities;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Users.CreateUser
{

    public class CreateUserRequestValidatorTests
    {

        #region - - - - - - Fields - - - - - -

        private const string LENGTH_OF_10 = "1234567890";
        private const string LENGTH_OF_100 = LENGTH_OF_50 + LENGTH_OF_50;
        private const string LENGTH_OF_50 = LENGTH_OF_10 + LENGTH_OF_10 + LENGTH_OF_10 + LENGTH_OF_10 + LENGTH_OF_10;

        #endregion Fields

        #region - - - - - - CreateUserRequestValidator Tests - - - - - -

        [Fact]
        public void Validate_InvalidEmployeeRoleID_ShouldHaveValidationError()
            => new CreateUserRequestValidator().ShouldHaveValidationErrorFor(r => r.EmployeeRoleID, default(EntityID));

        [Fact]
        public void Validate_ValidEmployeeRoleID_ShouldNotHaveValidationError()
            => new CreateUserRequestValidator().ShouldNotHaveValidationErrorFor(r => r.EmployeeRoleID, new Mock<EntityID>().Object);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_50 + "1")]
        public void Validate_InvalidFirstName_ShouldHaveValidationError(string firstName)
            => new CreateUserRequestValidator().ShouldHaveValidationErrorFor(r => r.FirstName, firstName);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_50)]
        public void Validate_ValidFirstName_ShouldNotHaveValidationError(string firstName)
            => new CreateUserRequestValidator().ShouldNotHaveValidationErrorFor(r => r.FirstName, firstName);

        [Fact]
        public void Validate_InvalidGenderID_ShouldHaveValidationError()
            => new CreateUserRequestValidator().ShouldHaveValidationErrorFor(r => r.GenderID, default(EntityID));

        [Fact]
        public void Validate_ValidGenderID_ShouldNotHaveValidationError()
            => new CreateUserRequestValidator().ShouldNotHaveValidationErrorFor(r => r.GenderID, new Mock<EntityID>().Object);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_50 + "1")]
        public void Validate_InvalidLastName_ShouldHaveValidationError(string lastName)
            => new CreateUserRequestValidator().ShouldHaveValidationErrorFor(r => r.LastName, lastName);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_50)]
        public void Validate_ValidLastName_ShouldNotHaveValidationError(string lastName)
            => new CreateUserRequestValidator().ShouldNotHaveValidationErrorFor(r => r.LastName, lastName);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_100 + "1")]
        public void Validate_InvalidPlainTextPassword_ShouldHaveValidationError(string plainTextPassword)
            => new CreateUserRequestValidator().ShouldHaveValidationErrorFor(r => r.PlainTextPassword, plainTextPassword);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_100)]
        public void Validate_ValidPlainTextPassword_ShouldNotHaveValidationError(string plainTextPassword)
            => new CreateUserRequestValidator().ShouldNotHaveValidationErrorFor(r => r.PlainTextPassword, plainTextPassword);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(LENGTH_OF_100 + "1")]
        public void Validate_InvalidUserName_ShouldHaveValidationError(string userName)
            => new CreateUserRequestValidator().ShouldHaveValidationErrorFor(r => r.UserName, userName);

        [Theory]
        [InlineData("1")]
        [InlineData(LENGTH_OF_100)]
        public void Validate_ValidUserName_ShouldNotHaveValidationError(string userName)
            => new CreateUserRequestValidator().ShouldNotHaveValidationErrorFor(r => r.UserName, userName);

        #endregion CreateUserRequestValidator Tests

    }

}
