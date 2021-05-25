using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;
using CleanArchitecture.Services.Pipeline.Infrastructure;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Services.Tests.Unit.Pipeline.Infrastructure
{

    public class BusinessRuleValidatorUseCaseElementTests
    {

        #region - - - - - - TryPresentResultAsync Tests - - - - - -

        [Fact]
        public async Task TryPresentResultAsync_RequestFailsBusinessRuleValidation_PresentsValidationFailureAndReturnsTrue()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _MockPresenter = new Mock<IPresenter<object, IValidationResult>>();
            var _MockValidationResult = new Mock<IValidationResult>();
            _ = _MockValidationResult
                    .Setup(mock => mock.IsValid)
                    .Returns(false);

            var _MockBusinessRuleValidator = new Mock<IBusinessRuleValidator<IUseCaseRequest<object>, IValidationResult>>();
            _ = _MockBusinessRuleValidator
                    .Setup(mock => mock.ValidateAsync(_Request, _CancellationToken))
                    .Returns(Task.FromResult(_MockValidationResult.Object));

            var _MockServiceProvider = new Mock<IServiceProvider>();
            _ = _MockServiceProvider
                    .Setup(mock => mock.GetService(typeof(IBusinessRuleValidator<IUseCaseRequest<object>, IValidationResult>)))
                    .Returns(_MockBusinessRuleValidator.Object);

            var _Element = new BusinessRuleValidatorUseCaseElement<object, IValidationResult>(_MockServiceProvider.Object);

            // Act
            var _Actual = await _Element.TryPresentResultAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _ = _Actual.Should().BeTrue();

            _MockBusinessRuleValidator.Verify(mock => mock.ValidateAsync(_Request, _CancellationToken), Times.Once);
            _MockPresenter.Verify(mock => mock.PresentValidationFailureAsync(_MockValidationResult.Object, _CancellationToken), Times.Once);
            _MockValidationResult.Verify(mock => mock.IsValid);

            _MockBusinessRuleValidator.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task TryPresentResultAsync_RequestPassesBusinessRuleValidation_DoesntPresentAnythingAndReturnsFalse()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _MockPresenter = new Mock<IPresenter<object, IValidationResult>>();
            var _MockValidationResult = new Mock<IValidationResult>();
            _ = _MockValidationResult
                    .Setup(mock => mock.IsValid)
                    .Returns(true);

            var _MockBusinessRuleValidator = new Mock<IBusinessRuleValidator<IUseCaseRequest<object>, IValidationResult>>();
            _ = _MockBusinessRuleValidator
                    .Setup(mock => mock.ValidateAsync(_Request, _CancellationToken))
                    .Returns(Task.FromResult(_MockValidationResult.Object));

            var _MockServiceProvider = new Mock<IServiceProvider>();
            _ = _MockServiceProvider
                    .Setup(mock => mock.GetService(typeof(IBusinessRuleValidator<IUseCaseRequest<object>, IValidationResult>)))
                    .Returns(_MockBusinessRuleValidator.Object);

            var _Element = new BusinessRuleValidatorUseCaseElement<object, IValidationResult>(_MockServiceProvider.Object);

            // Act
            var _Actual = await _Element.TryPresentResultAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _ = _Actual.Should().BeFalse();

            _MockBusinessRuleValidator.Verify(mock => mock.ValidateAsync(_Request, _CancellationToken), Times.Once);
            _MockPresenter.Verify(mock => mock.PresentValidationFailureAsync(_MockValidationResult.Object, _CancellationToken), Times.Never);
            _MockValidationResult.Verify(mock => mock.IsValid);

            _MockBusinessRuleValidator.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion TryPresentResultAsync Tests

    }

}
