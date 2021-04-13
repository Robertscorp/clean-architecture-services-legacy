using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.Users.CreateUser;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Persistence;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Users.CreateUser
{

    public class CreateUserInteractorTests
    {

        #region - - - - - - HandleAsync Tests - - - - - -

        [Fact]
        public async Task HandleAsync_AnyRequest_AddsEmployeeToPersistenceContextAndPresentsSuccessfully()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _Employee = new Employee();
            var _Request = new CreateUserRequest();
            var _UserDto = new UserDto();

            var _MockMapper = new Mock<IMapper>();
            _ = _MockMapper
                    .Setup(mock => mock.Map<Employee>(_Request))
                    .Returns(_Employee);
            _ = _MockMapper
                    .Setup(mock => mock.Map<UserDto>(_Employee))
                    .Returns(_UserDto);

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            var _MockPresenter = new Mock<IPresenter<UserDto>>();

            var _Interactor = new CreateUserInteractor(_MockMapper.Object, _MockPersistenceContext.Object);

            // Act
            await _Interactor.HandleAsync(_Request, _MockPresenter.Object, _CancellationToken);

            // Assert
            _MockPersistenceContext.Verify(mock => mock.AddAsync(_Employee, _CancellationToken));
            _MockPresenter.Verify(mock => mock.PresentAsync(_UserDto, _CancellationToken));

            _MockPersistenceContext.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
