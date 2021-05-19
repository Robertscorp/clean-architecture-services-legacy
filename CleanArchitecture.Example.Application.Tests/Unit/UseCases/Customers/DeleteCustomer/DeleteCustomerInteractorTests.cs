using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.UseCases.Customers.DeleteCustomer;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Persistence;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Customers.DeleteCustomer
{

    public class DeleteCustomerInteractorTests
    {

        #region - - - - - - Fields - - - - - -

        private readonly Mock<IPersistenceContext> m_MockPersistenceContext = new();
        private readonly Mock<IPresenter<CustomerDto>> m_MockPresenter = new();

        private readonly CancellationToken m_CancellationToken = new();
        private readonly Customer m_ExistingCustomer = new();
        private readonly CustomerDto m_ExistingCustomerDto = new();
        private readonly EntityID m_ExistingCustomerID = new Mock<EntityID>().Object;
        private readonly DeleteCustomerInteractor m_Interactor;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DeleteCustomerInteractorTests()
        {
            var _MockMapper = new Mock<IMapper>();
            _ = _MockMapper
                    .Setup(mock => mock.Map<CustomerDto>(this.m_ExistingCustomer))
                    .Returns(this.m_ExistingCustomerDto);

            _ = this.m_MockPersistenceContext
                    .Setup(mock => mock.FindAsync<Customer>(this.m_ExistingCustomerID, this.m_CancellationToken))
                    .Returns(Task.FromResult(this.m_ExistingCustomer));

            this.m_Interactor = new DeleteCustomerInteractor(_MockMapper.Object, this.m_MockPersistenceContext.Object);
        }

        #endregion Constructors

        #region - - - - - - HandleAsync Tests - - - - - -

        [Fact]
        public async Task HandleAsync_CustomerDoesNotExist_PresentsEntityNotFound()
        {
            // Arrange
            var _Request = new DeleteCustomerRequest() { CustomerID = new Mock<EntityID>().Object };

            // Act
            await this.m_Interactor.HandleAsync(_Request, this.m_MockPresenter.Object, this.m_CancellationToken);

            // Assert
            this.m_MockPersistenceContext.Verify(mock => mock.FindAsync<Customer>(_Request.CustomerID, this.m_CancellationToken));
            this.m_MockPresenter.Verify(mock => mock.PresentEntityNotFoundAsync(_Request.CustomerID, this.m_CancellationToken));

            this.m_MockPersistenceContext.VerifyNoOtherCalls();
            this.m_MockPresenter.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task HandleAsync_CustomerExists_RemovesCustomerFromPersistenceContextAndPresentsSuccessfully()
        {
            // Arrange
            var _Request = new DeleteCustomerRequest() { CustomerID = this.m_ExistingCustomerID };

            // Act
            await this.m_Interactor.HandleAsync(_Request, this.m_MockPresenter.Object, this.m_CancellationToken);

            // Assert
            this.m_MockPersistenceContext.Verify(mock => mock.FindAsync<Customer>(_Request.CustomerID, this.m_CancellationToken));
            this.m_MockPersistenceContext.Verify(mock => mock.RemoveAsync(this.m_ExistingCustomer, this.m_CancellationToken));
            this.m_MockPresenter.Verify(mock => mock.PresentAsync(this.m_ExistingCustomerDto, this.m_CancellationToken));

            this.m_MockPersistenceContext.VerifyNoOtherCalls();
            this.m_MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
