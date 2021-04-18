using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Internal;
using CleanArchitecture.Services.Pipeline;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Services.Tests.Unit.Internal
{

    public class InternalUseCaseInvokerTests
    {

        #region - - - - - - InvokeUseCaseAsync Tests - - - - - -

        [Fact]
        public async Task InvokeUseCaseAsync_MultipleElements_PresentsResultsOfElementsUntilOneReturnsTrue()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Presenter = new Mock<IPresenter<object, IValidationResult>>().Object;
            var _Request = new Mock<IUseCaseRequest<object>>().Object;

            var _MockUseCaseElement1 = new Mock<IUseCaseElement<object, IValidationResult>>();
            var _MockUseCaseElement2 = new Mock<IUseCaseElement<object, IValidationResult>>();
            _ = _MockUseCaseElement2
                    .Setup(mock => mock.TryPresentResultAsync(_Request, _Presenter, _CancellationToken))
                    .Returns(Task.FromResult(true));

            var _MockUseCaseElement3 = new Mock<IUseCaseElement<object, IValidationResult>>();

            var _MockServiceProvider = new Mock<IServiceProvider>();
            _ = _MockServiceProvider
                    .Setup(mock => mock.GetService(typeof(IEnumerable<IUseCaseElement<object, IValidationResult>>)))
                    .Returns(new[]
                    {
                        _MockUseCaseElement1.Object,
                        _MockUseCaseElement2.Object,
                        _MockUseCaseElement3.Object
                    });

            var _UseCaseInvoker = new InternalUseCaseInvoker<IPresenter<object, IValidationResult>, IUseCaseRequest<object>, object, IValidationResult>(_Presenter, _Request, _MockServiceProvider.Object);

            // Act
            await _UseCaseInvoker.InvokeUseCaseAsync(_CancellationToken);

            // Assert
            _MockUseCaseElement1.Verify(mock => mock.TryPresentResultAsync(_Request, _Presenter, _CancellationToken), Times.Once);
            _MockUseCaseElement2.Verify(mock => mock.TryPresentResultAsync(_Request, _Presenter, _CancellationToken), Times.Once);
            _MockUseCaseElement3.Verify(mock => mock.TryPresentResultAsync(_Request, _Presenter, _CancellationToken), Times.Never);
            _MockUseCaseElement1.VerifyNoOtherCalls();
            _MockUseCaseElement2.VerifyNoOtherCalls();
            _MockUseCaseElement3.VerifyNoOtherCalls();
        }

        #endregion InvokeUseCaseAsync Tests

    }

}
