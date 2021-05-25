using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;
using CleanArchitecture.Services.Pipeline.Infrastructure;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Services.Tests.Integration.Pipeline.Infrastructure
{

    public class UseCaseInvokerTests
    {

        #region - - - - - - InvokeUseCaseAsync Tests - - - - - -

        [Fact]
        public async Task InvokeUseCaseAsync_ComplexPresenter_ResolvesInteractorCorrectly()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Presenter = new DerivedInterfaceDerivedPresenter();
            var _Request = new TestRequest();

            var _MockInteractor = new Mock<IUseCaseInteractor<IDerivedPresenter, TestRequest, object, IValidationResult>>();
            var _MockServiceProvider = new Mock<IServiceProvider>();
            _ = _MockServiceProvider
                    .Setup(mock => mock.GetService(typeof(IEnumerable<IUseCaseElement<object, IValidationResult>>)))
                    .Returns(new[]
                    {
                        new InteractorUseCaseElement<object, IValidationResult>(_MockServiceProvider.Object)
                    });
            _ = _MockServiceProvider
                    .Setup(mock => mock.GetService(typeof(IUseCaseInteractor<IDerivedPresenter, TestRequest, object, IValidationResult>)))
                    .Returns(_MockInteractor.Object);

            var _UseCaseInvoker = new UseCaseInvoker(_MockServiceProvider.Object);

            // Act
            var _Exception = await Record.ExceptionAsync(() => _UseCaseInvoker.InvokeUseCaseAsync(_Request, _Presenter, _CancellationToken));

            // Assert
            _ = _Exception.Should().BeNull();

            _MockInteractor.Verify(mock => mock.HandleAsync(_Request, _Presenter, _CancellationToken), Times.Once);
            _MockInteractor.VerifyNoOtherCalls();
        }

        #endregion InvokeUseCaseAsync Tests

        #region - - - - - - Nested Classes - - - - - -

        public interface IDerivedPresenter : IPresenter<object, IValidationResult> { }

        public abstract class DerivedInterfaceAbstractPresenter : IDerivedPresenter
        {
            public Task PresentAsync(object response, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentEntityNotFoundAsync(EntityID entityID, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentValidationFailureAsync(IValidationResult validationResult, CancellationToken cancellationToken) => throw new NotImplementedException();
        }

        public class DerivedInterfaceDerivedPresenter : DerivedInterfaceAbstractPresenter { }

        public class TestRequest : IUseCaseRequest<object> { }

        #endregion Nested Classes

    }

}
