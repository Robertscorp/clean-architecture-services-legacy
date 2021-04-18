using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Internal;
using CleanArchitecture.Services.Pipeline;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Services.Tests.Unit.Internal
{

    public class InternalPresenterInterfaceTypeResolverTests
    {

        #region - - - - - - GetPresenterInterfaceType Tests - - - - - -

        [Theory]
        [InlineData(typeof(BasicInterfacePresenter), typeof(IPresenter<object, IValidationResult>))]
        [InlineData(typeof(BasicInterfaceAbstractPresenter), typeof(IPresenter<object, IValidationResult>))]
        [InlineData(typeof(BasicInterfaceDerivedPresenter), typeof(IPresenter<object, IValidationResult>))]
        [InlineData(typeof(DerivedInterfacePresenter), typeof(IDerivedPresenter))]
        [InlineData(typeof(DerivedInterfaceAbstractPresenter), typeof(IDerivedPresenter))]
        [InlineData(typeof(DerivedInterfaceDerivedPresenter), typeof(IDerivedPresenter))]
        public void GetPresenterInterfaceType_VariousPresenterTypes_ReturnsExpectedType(Type presenterType, Type expected)
        {
            // Arrange
            var _Resolver = new InternalPresenterInterfaceTypeResolver<object, IValidationResult>();

            // Act
            var _Actual = _Resolver.GetPresenterInterfaceType(presenterType);

            // Assert
            _ = _Actual.Should().Be(expected);
        }

        #endregion GetPresenterInterfaceType Tests

        #region - - - - - - Nested Classes - - - - - -

        public class BasicInterfacePresenter : IPresenter<object, IValidationResult>
        {
            public Task PresentAsync(object response, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentEntityNotFoundAsync(EntityRequest entityRequest, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentValidationFailureAsync(IValidationResult validationResult, CancellationToken cancellationToken) => throw new NotImplementedException();
        }

        public abstract class BasicInterfaceAbstractPresenter : IPresenter<object, IValidationResult>
        {
            public Task PresentAsync(object response, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentEntityNotFoundAsync(EntityRequest entityRequest, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentValidationFailureAsync(IValidationResult validationResult, CancellationToken cancellationToken) => throw new NotImplementedException();
        }

        public class BasicInterfaceDerivedPresenter : BasicInterfaceAbstractPresenter { }

        public interface IDerivedPresenter : IPresenter<object, IValidationResult> { }

        public class DerivedInterfacePresenter : IDerivedPresenter
        {
            public Task PresentAsync(object response, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentEntityNotFoundAsync(EntityRequest entityRequest, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentValidationFailureAsync(IValidationResult validationResult, CancellationToken cancellationToken) => throw new NotImplementedException();
        }

        public abstract class DerivedInterfaceAbstractPresenter : IDerivedPresenter
        {
            public Task PresentAsync(object response, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentEntityNotFoundAsync(EntityRequest entityRequest, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task PresentValidationFailureAsync(IValidationResult validationResult, CancellationToken cancellationToken) => throw new NotImplementedException();
        }

        public class DerivedInterfaceDerivedPresenter : DerivedInterfaceAbstractPresenter { }

        #endregion Nested Classes

    }

}
