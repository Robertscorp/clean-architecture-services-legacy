using CleanArchitecture.Example.Application.UseCases.Users.CreateUser;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Services.Persistence;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Users.CreateUser
{

    public class CreateUserBusinessRuleValidatorTests
    {

        #region - - - - - - ValidateAsync Tests - - - - - -

        [Fact]
        public async Task ValidateAsync_UserNameAlreadyInUse_ReturnsValidationFailure()
        {
            // Arrange
            var _CancellationToken = CancellationToken.None;
            var _Request = new CreateUserRequest() { UserName = "UserName" };

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            _ = _MockPersistenceContext
                    .Setup(mock => mock.GetEntitiesAsync<User>(_CancellationToken))
                    .Returns(Task.FromResult(new[] { new User { UserName = _Request.UserName } }.AsQueryable()));

            var _BusinessRuleValidator = new CreateUserBusinessRuleValidator(_MockPersistenceContext.Object);

            var _Expected = ValidationResult.Failure("A user with that user name already exists.");

            // Act
            var _Actual = await _BusinessRuleValidator.ValidateAsync(_Request, _CancellationToken);

            // Assert
            _Actual.Should().BeEquivalentTo(_Expected);
        }

        [Fact]
        public async Task ValidateAsync_UserNameNotUsed_ReturnsValidationSuccess()
        {
            // Arrange
            var _CancellationToken = CancellationToken.None;
            var _Request = new CreateUserRequest() { UserName = "UserName" };

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            _ = _MockPersistenceContext
                    .Setup(mock => mock.GetEntitiesAsync<User>(_CancellationToken))
                    .Returns(Task.FromResult(new[] { new User { UserName = "Existing" } }.AsQueryable()));

            var _BusinessRuleValidator = new CreateUserBusinessRuleValidator(_MockPersistenceContext.Object);

            var _Expected = ValidationResult.Success();

            // Act
            var _Actual = await _BusinessRuleValidator.ValidateAsync(_Request, _CancellationToken);

            // Assert
            _Actual.Should().BeEquivalentTo(_Expected);
        }

        #endregion ValidateAsync Tests

    }

}
