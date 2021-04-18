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

    public class InteractorUseCaseElementTests
    {

        #region - - - - - - TryPresentResultAsync Tests - - - - - -

        [Fact]
        public async Task TryPresentResultAsync_AnyRequest_InvokesUseCaseInteractorAndReturnsTrue()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _MockUseCaseInteractor = new Mock<IUseCaseInteractor<IPresenter<object, IValidationResult>, IUseCaseRequest<object>, object, IValidationResult>>();
            var _MockServiceProvider = new Mock<IServiceProvider>();
            _ = _MockServiceProvider
                    .Setup(mock => mock.GetService(typeof(IUseCaseInteractor<IPresenter<object, IValidationResult>, IUseCaseRequest<object>, object, IValidationResult>)))
                    .Returns(_MockUseCaseInteractor.Object);

            var _MockPresenter = new Mock<IPresenter<object, IValidationResult>>();

            var _Element = new InteractorUseCaseElement<object, IValidationResult>(_MockServiceProvider.Object);

            // Act
            var _Actual = await _Element.TryPresentResultAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _ = _Actual.Should().BeTrue();

            _MockUseCaseInteractor.Verify(mock => mock.HandleAsync(_Request, _MockPresenter.Object, _CancellationToken), Times.Once);
            _MockPresenter.VerifyNoOtherCalls();
            _MockUseCaseInteractor.VerifyNoOtherCalls();
        }

        #endregion TryPresentResultAsync Tests

    }

}
