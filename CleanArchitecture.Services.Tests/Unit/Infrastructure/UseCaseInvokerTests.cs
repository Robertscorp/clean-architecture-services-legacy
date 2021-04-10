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
        public async Task InvokeUseCaseAsync_ValidUseCase_SuccessfullyInvokesUseCase()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _MockUseCaseInteractor = new Mock<IUseCaseInteractor<IUseCaseRequest<object>, object, object>>();
            var _MockServiceProvider = new Mock<IServiceProvider>();
            _MockServiceProvider
                .Setup(mock => mock.GetService(typeof(IUseCaseInteractor<IUseCaseRequest<object>, object, object>)))
                .Returns(_MockUseCaseInteractor.Object);

            var _MockPresenter = new Mock<IPresenter<object, object>>();
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
