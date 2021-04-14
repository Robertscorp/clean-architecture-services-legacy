using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.Users.GetUsers;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Persistence;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Users.GetUsers
{

    public class GetUsersInteractorTests
    {

        #region - - - - - - HandleAsync Tests - - - - - -

        [Fact]
        public async Task HandleAsync_AnyRequest_PresentsEmployees()
        {
            // Arrange
            var _Actual = default(IQueryable<UserDto>);
            var _CancellationToken = new CancellationToken();
            var _Employee = new Employee();
            var _Request = new GetUsersRequest();
            var _UserDto = new UserDto();

            var _MockMapper = new Mock<IMapper>();
            _ = _MockMapper
                    .Setup(mock => mock.ConfigurationProvider)
                    .Returns(new MapperConfiguration(cfg
                        => cfg.CreateMap<Employee, UserDto>()
                            .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName))
                            .ForAllOtherMembers(opts => opts.Ignore())));

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            _ = _MockPersistenceContext
                    .Setup(mock => mock.GetEntitiesAsync<Employee>(_CancellationToken))
                    .Returns(Task.FromResult(new[]
                    {
                        new Employee { User = new User { UserName = "UserName1" } },
                        new Employee { User = new User { UserName = "UserName2" } }
                    }.AsQueryable()));

            var _MockPresenter = new Mock<IPresenter<IQueryable<UserDto>>>();
            _ = _MockPresenter
                    .Setup(mock => mock.PresentAsync(It.IsAny<IQueryable<UserDto>>(), _CancellationToken))
                    .Callback((IQueryable<UserDto> dtos, CancellationToken c) => _Actual = dtos);

            var _Interactor = new GetUsersInteractor(_MockMapper.Object, _MockPersistenceContext.Object);

            var _Expected = new[]
            {
                new UserDto { UserName = "UserName1" },
                new UserDto { UserName = "UserName2" }
            };

            // Act
            await _Interactor.HandleAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _ = _Actual.Should().BeEquivalentTo(_Expected);

            _MockPresenter.Verify(mock => mock.PresentAsync(It.IsAny<IQueryable<UserDto>>(), _CancellationToken));
            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
