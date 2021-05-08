using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Persistence;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerInteractorTests
    {

        #region - - - - - - Fields - - - - - -

        private readonly CancellationToken m_CancellationToken = new CancellationToken();
        private readonly Customer m_Customer = new Customer() { CustomerDetails = new Person() };
        private readonly CustomerDto m_CustomerDto = new CustomerDto();
        private readonly CreateCustomerRequest m_Request = new CreateCustomerRequest() { GenderID = new Mock<EntityID>().Object };

        private readonly Mock<IMapper> m_MockMapper = new Mock<IMapper>();
        private readonly Mock<IPersistenceContext> m_MockPersistenceContext = new Mock<IPersistenceContext>();
        private readonly Mock<IPresenter<CustomerDto>> m_MockPresenter = new Mock<IPresenter<CustomerDto>>();

        private readonly CreateCustomerInteractor m_Interactor;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateCustomerInteractorTests()
        {
            _ = this.m_MockMapper
                    .Setup(mock => mock.Map<Customer>(this.m_Request))
                    .Returns(this.m_Customer);
            _ = this.m_MockMapper
                    .Setup(mock => mock.Map<CustomerDto>(this.m_Customer))
                    .Returns(this.m_CustomerDto);

            this.m_Interactor = new CreateCustomerInteractor(this.m_MockMapper.Object, this.m_MockPersistenceContext.Object);
        }

        #endregion Constructors

        #region - - - - - - HandleAsync Tests - - - - - -

        [Fact]
        public async Task HandleAsync_GenderIsNull_PresentsNotFound()
        {
            // Arrange

            // Act
            await this.m_Interactor.HandleAsync(this.m_Request, this.m_MockPresenter.Object, this.m_CancellationToken);

            // Assert
            this.m_MockPresenter.Verify(mock => mock.PresentEntityNotFoundAsync(this.m_Request.GenderID, this.m_CancellationToken));
            this.m_MockPresenter.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task HandleAsync_ValidRequest_AddsCustomerToPersistenceContextAndPresentsSuccessfully()
        {
            // Arrange
            this.m_Customer.CustomerDetails.Gender = Gender.Mayonnaise;

            // Act
            await this.m_Interactor.HandleAsync(this.m_Request, this.m_MockPresenter.Object, this.m_CancellationToken);

            // Assert
            this.m_MockPersistenceContext.Verify(mock => mock.AddAsync(this.m_Customer, this.m_CancellationToken));
            this.m_MockPresenter.Verify(mock => mock.PresentAsync(this.m_CustomerDto, this.m_CancellationToken));

            this.m_MockPersistenceContext.VerifyNoOtherCalls();
            this.m_MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
