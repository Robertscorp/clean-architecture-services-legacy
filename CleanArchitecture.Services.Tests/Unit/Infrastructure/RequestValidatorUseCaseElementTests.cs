using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Infrastructure;
using CleanArchitecture.Services.Pipeline;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Services.Tests.Unit.Infrastructure
{

    public class RequestValidatorUseCaseElementTests
    {

        #region - - - - - - TryPresentResultAsync Tests - - - - - -

        [Fact]
        public async Task TryPresentResultAsync_RequestFailsRequestValidation_PresentsValidationFailureAndReturnsTrue()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _MockPresenter = new Mock<IPresenter<object, IValidationResult>>();
            var _MockValidationResult = new Mock<IValidationResult>();
            _ = _MockValidationResult
                    .Setup(mock => mock.IsValid)
                    .Returns(false);

            var _MockRequestValidator = new Mock<IRequestValidator<IUseCaseRequest<object>, IValidationResult>>();
            _ = _MockRequestValidator
                    .Setup(mock => mock.ValidateAsync(_Request, _CancellationToken))
                    .Returns(Task.FromResult(_MockValidationResult.Object));

            var _MockServiceProvider = new Mock<IServiceProvider>();
            _ = _MockServiceProvider
                    .Setup(mock => mock.GetService(typeof(IRequestValidator<IUseCaseRequest<object>, IValidationResult>)))
                    .Returns(_MockRequestValidator.Object);

            var _Element = new RequestValidatorUseCaseElement<object, IValidationResult>(_MockServiceProvider.Object);

            // Act
            var _Actual = await _Element.TryPresentResultAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _ = _Actual.Should().BeTrue();

            _MockRequestValidator.Verify(mock => mock.ValidateAsync(_Request, _CancellationToken), Times.Once);
            _MockPresenter.Verify(mock => mock.PresentValidationFailureAsync(_MockValidationResult.Object, _CancellationToken), Times.Once);
            _MockValidationResult.Verify(mock => mock.IsValid);

            _MockRequestValidator.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task TryPresentResultAsync_RequestPassesRequestValidation_DoesntPresentAnythingAndReturnsFalse()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _MockPresenter = new Mock<IPresenter<object, IValidationResult>>();
            var _MockValidationResult = new Mock<IValidationResult>();
            _ = _MockValidationResult
                    .Setup(mock => mock.IsValid)
                    .Returns(true);

            var _MockRequestValidator = new Mock<IRequestValidator<IUseCaseRequest<object>, IValidationResult>>();
            _ = _MockRequestValidator
                    .Setup(mock => mock.ValidateAsync(_Request, _CancellationToken))
                    .Returns(Task.FromResult(_MockValidationResult.Object));

            var _MockServiceProvider = new Mock<IServiceProvider>();
            _ = _MockServiceProvider
                    .Setup(mock => mock.GetService(typeof(IRequestValidator<IUseCaseRequest<object>, IValidationResult>)))
                    .Returns(_MockRequestValidator.Object);

            var _Element = new RequestValidatorUseCaseElement<object, IValidationResult>(_MockServiceProvider.Object);

            // Act
            var _Actual = await _Element.TryPresentResultAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _ = _Actual.Should().BeFalse();

            _MockRequestValidator.Verify(mock => mock.ValidateAsync(_Request, _CancellationToken), Times.Once);
            _MockPresenter.Verify(mock => mock.PresentValidationFailureAsync(_MockValidationResult.Object, _CancellationToken), Times.Never);
            _MockValidationResult.Verify(mock => mock.IsValid);

            _MockRequestValidator.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion TryPresentResultAsync Tests

    }

}
