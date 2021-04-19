using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.Customers.GetCustomers;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Persistence;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Customers.GetCustomers
{

    public class GetCustomersInteractorTests
    {

        #region - - - - - - HandleAsync Tests - - - - - -

        [Fact]
        public async Task HandleAsync_AnyRequest_PresentsCustomers()
        {
            // Arrange
            var _Actual = default(IQueryable<CustomerDto>);
            var _CancellationToken = new CancellationToken();
            var _Customer = new Customer();
            var _Request = new GetCustomersRequest();
            var _CustomerDto = new CustomerDto();

            var _MockMapper = new Mock<IMapper>();
            _ = _MockMapper
                    .Setup(mock => mock.ConfigurationProvider)
                    .Returns(new MapperConfiguration(cfg
                        => cfg.CreateMap<Customer, CustomerDto>()
                            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.CustomerDetails.FirstName))
                            .ForAllOtherMembers(opts => opts.Ignore())));

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            _ = _MockPersistenceContext
                    .Setup(mock => mock.GetEntitiesAsync<Customer>(_CancellationToken))
                    .Returns(Task.FromResult(new[]
                    {
                        new Customer { CustomerDetails = new Person() { FirstName = "CustomerName1" } },
                        new Customer { CustomerDetails = new Person() { FirstName = "CustomerName2" } }
                    }.AsQueryable()));

            var _MockPresenter = new Mock<IPresenter<IQueryable<CustomerDto>>>();
            _ = _MockPresenter
                    .Setup(mock => mock.PresentAsync(It.IsAny<IQueryable<CustomerDto>>(), _CancellationToken))
                    .Callback((IQueryable<CustomerDto> dtos, CancellationToken c) => _Actual = dtos);

            var _Interactor = new GetCustomersInteractor(_MockMapper.Object, _MockPersistenceContext.Object);

            var _Expected = new[]
            {
                new CustomerDto { FirstName = "CustomerName1" },
                new CustomerDto { FirstName = "CustomerName2" }
            };

            // Act
            await _Interactor.HandleAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _ = _Actual.Should().BeEquivalentTo(_Expected);

            _MockPresenter.Verify(mock => mock.PresentAsync(It.IsAny<IQueryable<CustomerDto>>(), _CancellationToken));
            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
