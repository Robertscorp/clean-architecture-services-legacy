using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Extended.Presenters;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Services.Extended.Tests.Unit.Presenters
{

    public class SingleEntityPresenterTests
    {

        #region - - - - - - Fields - - - - - -

        private readonly Mock<IPresenter<TestEntity>> m_MockEntityPresenter = new();

        private readonly CancellationToken m_CancellationToken = new();
        private readonly TestEntity m_Entity;
        private readonly EntityID m_EntityID = new TestEntityID();
        private readonly SingleEntityPresenter<TestEntity> m_SingleEntityPresenter;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public SingleEntityPresenterTests()
        {
            this.m_Entity = new TestEntity { ID = this.m_EntityID };
            this.m_SingleEntityPresenter = new SingleEntityPresenter<TestEntity>(this.m_EntityID, e => e.ID, this.m_MockEntityPresenter.Object);
        }

        #endregion Constructors

        #region - - - - - - PresentAsync Tests - - - - - -

        [Fact]
        public async Task PresentAsync_SingleEntityFound_PresentsSingleEntityInWrappedPresenter()
        {
            // Arrange
            var _Entities = new[] { new TestEntity(), this.m_Entity, new TestEntity() }.AsQueryable();

            // Act
            await this.m_SingleEntityPresenter.PresentAsync(_Entities, this.m_CancellationToken);

            // Assert
            this.m_MockEntityPresenter.Verify(mock => mock.PresentAsync(this.m_Entity, this.m_CancellationToken));
            this.m_MockEntityPresenter.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task PresentAsync_NoEntityFound_PresentsNotFoundInWrappedPresenter()
        {
            // Arrange
            var _Entities = new[] { new TestEntity(), new TestEntity(), new TestEntity() }.AsQueryable();

            // Act
            await this.m_SingleEntityPresenter.PresentAsync(_Entities, this.m_CancellationToken);

            // Assert
            this.m_MockEntityPresenter.Verify(mock => mock.PresentEntityNotFoundAsync(this.m_EntityID, this.m_CancellationToken));
            this.m_MockEntityPresenter.VerifyNoOtherCalls();
        }

        #endregion PresentAsync Tests

        #region - - - - - - PresentEntityNotFoundAsync Tests - - - - - -

        [Fact]
        public async Task PresentEntityNotFoundAsync_AnyRequest_PassesThroughToWrappedPresenter()
        {
            // Arrange
            var _EntityID = new TestEntityID();

            // Act
            await this.m_SingleEntityPresenter.PresentEntityNotFoundAsync(_EntityID, this.m_CancellationToken);

            // Assert
            this.m_MockEntityPresenter.Verify(mock => mock.PresentEntityNotFoundAsync(_EntityID, this.m_CancellationToken));
            this.m_MockEntityPresenter.VerifyNoOtherCalls();
        }

        #endregion PresentEntityNotFoundAsync Tests

        #region - - - - - - PresentValidationFailureAsync Tests - - - - - -

        [Fact]
        public async Task PresentValidationFailureAsync_AnyRequest_PassesThroughToWrappedPresenter()
        {
            // Arrange
            var _ValidationResult = ValidationResult.Failure("Failure");

            // Act
            await this.m_SingleEntityPresenter.PresentValidationFailureAsync(_ValidationResult, this.m_CancellationToken);

            // Assert
            this.m_MockEntityPresenter.Verify(mock => mock.PresentValidationFailureAsync(_ValidationResult, this.m_CancellationToken));
            this.m_MockEntityPresenter.VerifyNoOtherCalls();
        }

        #endregion PresentValidationFailureAsync Tests

        #region - - - - - - Nested Classes - - - - - -

        public class TestEntity
        {

            #region - - - - - - Properties - - - - - -

            public EntityID ID { get; set; }

            #endregion Properties

        }

        public class TestEntityID : EntityID { }

        #endregion Nested Classes

    }

}
