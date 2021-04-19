using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Persistence;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Customers.CreateCustomer
{

    public class CreateCustomerInteractorTests
    {

        #region - - - - - - HandleAsync Tests - - - - - -

        [Fact]
        public async Task HandleAsync_AnyRequest_AddsCustomerToPersistenceContextAndPresentsSuccessfully()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Customer = new Customer();
            var _Request = new CreateCustomerRequest();
            var _CustomerDto = new CustomerDto();

            var _MockMapper = new Mock<IMapper>();
            _ = _MockMapper
                    .Setup(mock => mock.Map<Customer>(_Request))
                    .Returns(_Customer);
            _ = _MockMapper
                    .Setup(mock => mock.Map<CustomerDto>(_Customer))
                    .Returns(_CustomerDto);

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            var _MockPresenter = new Mock<IPresenter<CustomerDto>>();

            var _Interactor = new CreateCustomerInteractor(_MockMapper.Object, _MockPersistenceContext.Object);

            // Act
            await _Interactor.HandleAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _MockPersistenceContext.Verify(mock => mock.AddAsync(_Customer, _CancellationToken));
            _MockPresenter.Verify(mock => mock.PresentAsync(_CustomerDto, _CancellationToken));

            _MockPersistenceContext.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
