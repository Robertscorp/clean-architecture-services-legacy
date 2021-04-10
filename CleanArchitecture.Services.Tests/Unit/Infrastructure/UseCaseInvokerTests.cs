using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Infrastructure;
using CleanArchitecture.Services.Pipeline;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Services.Tests.Unit.Infrastructure
{

    public class UseCaseInvokerTests
    {

        #region - - - - - - InvokeUseCaseAsync Tests - - - - - -

        [Fact]
        public async Task InvokeUseCaseAsync_FailsRequestValidation_PresentsValidationFailure()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _MockValidationResult = new Mock<IValidationResult>();
            _MockValidationResult
                .Setup(mock => mock.IsValid)
                .Returns(false);

            var _MockRequestValidator = new Mock<IRequestValidator<IUseCaseRequest<object>, IValidationResult>>();
            _MockRequestValidator
                .Setup(mock => mock.ValidateAsync(_Request, _CancellationToken))
                .Returns(Task.FromResult(_MockValidationResult.Object));

            var _MockUseCaseInteractor = new Mock<IUseCaseInteractor<IUseCaseRequest<object>, object, IValidationResult>>();
            var _MockServiceProvider = new Mock<IServiceProvider>();
            _MockServiceProvider
                .Setup(mock => mock.GetService(typeof(IRequestValidator<IUseCaseRequest<object>, IValidationResult>)))
                .Returns(_MockRequestValidator.Object);
            _MockServiceProvider
                .Setup(mock => mock.GetService(typeof(IUseCaseInteractor<IUseCaseRequest<object>, object, IValidationResult>)))
                .Returns(_MockUseCaseInteractor.Object);

            var _MockPresenter = new Mock<IPresenter<object, IValidationResult>>();

            var _UseCaseInvoker = new UseCaseInvoker(_MockServiceProvider.Object);

            // Act
            await _UseCaseInvoker.InvokeUseCaseAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _MockRequestValidator.Verify(mock => mock.ValidateAsync(_Request, _CancellationToken), Times.Once);
            _MockPresenter.Verify(mock => mock.PresentValidationFailureAsync(_MockValidationResult.Object, _CancellationToken), Times.Once);
            _MockValidationResult.Verify(mock => mock.IsValid, Times.Once);

            _MockRequestValidator.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
            _MockUseCaseInteractor.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task InvokeUseCaseAsync_FailsBusinessRuleValidation_PresentsValidationFailure()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _MockValidationResult = new Mock<IValidationResult>();
            _MockValidationResult
                .Setup(mock => mock.IsValid)
                .Returns(false);

            var _MockBusinessRuleValidator = new Mock<IBusinessRuleValidator<IUseCaseRequest<object>, IValidationResult>>();
            _MockBusinessRuleValidator
                .Setup(mock => mock.ValidateAsync(_Request, _CancellationToken))
                .Returns(Task.FromResult(_MockValidationResult.Object));

            var _MockUseCaseInteractor = new Mock<IUseCaseInteractor<IUseCaseRequest<object>, object, IValidationResult>>();
            var _MockServiceProvider = new Mock<IServiceProvider>();
            _MockServiceProvider
                .Setup(mock => mock.GetService(typeof(IBusinessRuleValidator<IUseCaseRequest<object>, IValidationResult>)))
                .Returns(_MockBusinessRuleValidator.Object);
            _MockServiceProvider
                .Setup(mock => mock.GetService(typeof(IUseCaseInteractor<IUseCaseRequest<object>, object, IValidationResult>)))
                .Returns(_MockUseCaseInteractor.Object);

            var _MockPresenter = new Mock<IPresenter<object, IValidationResult>>();

            var _UseCaseInvoker = new UseCaseInvoker(_MockServiceProvider.Object);

            // Act
            await _UseCaseInvoker.InvokeUseCaseAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _MockBusinessRuleValidator.Verify(mock => mock.ValidateAsync(_Request, _CancellationToken), Times.Once);
            _MockPresenter.Verify(mock => mock.PresentValidationFailureAsync(_MockValidationResult.Object, _CancellationToken), Times.Once);
            _MockValidationResult.Verify(mock => mock.IsValid, Times.Once);

            _MockBusinessRuleValidator.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
            _MockUseCaseInteractor.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task InvokeUseCaseAsync_IsValidWithAllValidators_InvokesUseCase()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _MockValidationResult = new Mock<IValidationResult>();
            _MockValidationResult
                .Setup(mock => mock.IsValid)
                .Returns(true);

            var _MockBusinessRuleValidator = new Mock<IBusinessRuleValidator<IUseCaseRequest<object>, IValidationResult>>();
            _MockBusinessRuleValidator
                .Setup(mock => mock.ValidateAsync(_Request, _CancellationToken))
                .Returns(Task.FromResult(_MockValidationResult.Object));

            var _MockRequestValidator = new Mock<IRequestValidator<IUseCaseRequest<object>, IValidationResult>>();
            _MockRequestValidator
                .Setup(mock => mock.ValidateAsync(_Request, _CancellationToken))
                .Returns(Task.FromResult(_MockValidationResult.Object));

            var _MockUseCaseInteractor = new Mock<IUseCaseInteractor<IUseCaseRequest<object>, object, IValidationResult>>();
            var _MockServiceProvider = new Mock<IServiceProvider>();
            _MockServiceProvider
                .Setup(mock => mock.GetService(typeof(IBusinessRuleValidator<IUseCaseRequest<object>, IValidationResult>)))
                .Returns(_MockBusinessRuleValidator.Object);
            _MockServiceProvider
                .Setup(mock => mock.GetService(typeof(IRequestValidator<IUseCaseRequest<object>, IValidationResult>)))
                .Returns(_MockRequestValidator.Object);
            _MockServiceProvider
                .Setup(mock => mock.GetService(typeof(IUseCaseInteractor<IUseCaseRequest<object>, object, IValidationResult>)))
                .Returns(_MockUseCaseInteractor.Object);

            var _MockPresenter = new Mock<IPresenter<object, IValidationResult>>();

            var _UseCaseInvoker = new UseCaseInvoker(_MockServiceProvider.Object);

            // Act
            await _UseCaseInvoker.InvokeUseCaseAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _MockBusinessRuleValidator.Verify(mock => mock.ValidateAsync(_Request, _CancellationToken), Times.Once);
            _MockRequestValidator.Verify(mock => mock.ValidateAsync(_Request, _CancellationToken), Times.Once);
            _MockUseCaseInteractor.Verify(mock => mock.HandleAsync(_Request, _MockPresenter.Object, _CancellationToken), Times.Once);
            _MockValidationResult.Verify(mock => mock.IsValid);

            _MockBusinessRuleValidator.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
            _MockRequestValidator.VerifyNoOtherCalls();
            _MockUseCaseInteractor.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task InvokeUseCaseAsync_IsValidWithNoValidators_InvokesUseCase()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _MockUseCaseInteractor = new Mock<IUseCaseInteractor<IUseCaseRequest<object>, object, IValidationResult>>();
            var _MockServiceProvider = new Mock<IServiceProvider>();
            _MockServiceProvider
                .Setup(mock => mock.GetService(typeof(IUseCaseInteractor<IUseCaseRequest<object>, object, IValidationResult>)))
                .Returns(_MockUseCaseInteractor.Object);

            var _MockPresenter = new Mock<IPresenter<object, IValidationResult>>();
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _UseCaseInvoker = new UseCaseInvoker(_MockServiceProvider.Object);

            // Act
            await _UseCaseInvoker.InvokeUseCaseAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _MockUseCaseInteractor.Verify(mock => mock.HandleAsync(_Request, _MockPresenter.Object, _CancellationToken), Times.Once);
        }

        #endregion InvokeUseCaseAsync Tests

    }

}
