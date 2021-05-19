using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer;
using CleanArchitecture.Example.Application.UseCases.Customers.DeleteCustomer;
using CleanArchitecture.Example.Application.UseCases.Customers.GetCustomers;
using CleanArchitecture.Example.InterfaceAdapters.Controllers;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Extended.Presenters;
using CleanArchitecture.Services.Pipeline;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.InterfaceAdapters.Tests.Unit.Controllers
{

    public class CustomerControllerTests
    {

        #region - - - - - - Fields - - - - - -

        private readonly Mock<IUseCaseInvoker> m_MockUseCaseInvoker = new();

        private readonly CancellationToken m_CancellationToken = new();
        private readonly CustomerController m_Controller;
        private readonly IPresenter<CustomerDto> m_EntityPresenter = new Mock<IPresenter<CustomerDto>>().Object;
        private readonly IPresenter<IQueryable<CustomerDto>> m_QueryPresenter = new Mock<IPresenter<IQueryable<CustomerDto>>>().Object;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CustomerControllerTests()
            => this.m_Controller = new CustomerController(this.m_MockUseCaseInvoker.Object);

        #endregion Constructors

        #region - - - - - - CreateCustomerAsync Tests - - - - - -

        [Fact]
        public async Task CreateCustomerAsync_AnyRequest_InvokesUseCaseWithPresenterAndCancellationToken()
        {
            // Arrange
            var _Request = new CreateCustomerRequest();

            // Act
            await this.m_Controller.CreateCustomerAsync(_Request, this.m_EntityPresenter, this.m_CancellationToken);

            // Assert
            this.m_MockUseCaseInvoker.Verify(mock => mock.InvokeUseCaseAsync(_Request, this.m_EntityPresenter, this.m_CancellationToken));
        }

        #endregion CreateCustomerAsync Tests

        #region - - - - - - DeleteCustomerAsync Tests - - - - - -

        [Fact]
        public async Task DeleteCustomerAsync_AnyRequest_InvokesUseCaseWithPresenterAndCancellationToken()
        {
            // Arrange
            var _Request = new DeleteCustomerRequest();

            // Act
            await this.m_Controller.DeleteCustomerAsync(_Request, this.m_EntityPresenter, this.m_CancellationToken);

            // Assert
            this.m_MockUseCaseInvoker.Verify(mock => mock.InvokeUseCaseAsync(_Request, this.m_EntityPresenter, this.m_CancellationToken));
        }

        #endregion DeleteCustomerAsync Tests

        #region - - - - - - GetCustomerAsync Tests - - - - - -

        [Fact]
        public async Task GetCustomerAsync_AnyRequest_InvokesUseCaseWithSingleEntityPresenterAndCancellationToken()
        {
            // Arrange
            var _EntityID = new Mock<EntityID>().Object;

            // Act
            await this.m_Controller.GetCustomerAsync(_EntityID, this.m_EntityPresenter, this.m_CancellationToken);

            // Assert
            this.m_MockUseCaseInvoker.Verify(mock => mock.InvokeUseCaseAsync(It.IsAny<GetCustomersRequest>(), It.IsAny<SingleEntityPresenter<CustomerDto>>(), this.m_CancellationToken));
        }

        #endregion GetCustomerAsync Tests

        #region - - - - - - GetCustomersAsync Tests - - - - - -

        [Fact]
        public async Task GetCustomersAsync_AnyRequest_InvokesUseCaseWithPresenterAndCancellationToken()
        {
            // Arrange

            // Act
            await this.m_Controller.GetCustomersAsync(this.m_QueryPresenter, this.m_CancellationToken);

            // Assert
            this.m_MockUseCaseInvoker.Verify(mock => mock.InvokeUseCaseAsync(It.IsAny<GetCustomersRequest>(), this.m_QueryPresenter, this.m_CancellationToken));
        }

        #endregion GetCustomersAsync Tests

    }

}
