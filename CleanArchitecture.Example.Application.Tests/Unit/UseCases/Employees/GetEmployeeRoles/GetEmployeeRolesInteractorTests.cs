using AutoMapper;
using CleanArchitecture.Example.Application.UseCases.Employees.GetEmployeeRoles;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Persistence;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.Employees.GetEmployeeRoles
{

    public class GetEmployeeRolesInteractorTests
    {

        #region - - - - - - HandleAsync Tests - - - - - -

        [Fact]
        public async Task HandleAsync_AnyRequest_PresentsSuccessfully()
        {
            // Arrange
            var _Actual = default(IQueryable<EmployeeRoleDto>);
            var _CancellationToken = new CancellationToken();
            var _EmployeeRoleDtos = new[] { new EmployeeRoleDto { Name = EmployeeRole.Admin.Name } };
            var _EmployeeRoles = new[] { EmployeeRole.Admin }.AsQueryable();

            var _MockMapper = new Mock<IMapper>();
            _ = _MockMapper
                    .Setup(mock => mock.ConfigurationProvider)
                    .Returns(new MapperConfiguration(opts => opts.CreateMap<EmployeeRole, EmployeeRoleDto>()));

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            _ = _MockPersistenceContext
                    .Setup(mock => mock.GetEntitiesAsync<EmployeeRole>(_CancellationToken))
                    .Returns(Task.FromResult(_EmployeeRoles));

            var _MockPresenter = new Mock<IPresenter<IQueryable<EmployeeRoleDto>>>();
            _ = _MockPresenter
                    .Setup(mock => mock.PresentAsync(It.IsAny<IQueryable<EmployeeRoleDto>>(), _CancellationToken))
                    .Callback((IQueryable<EmployeeRoleDto> dtos, CancellationToken ct) => _Actual = dtos);

            var _Interactor = new GetEmployeeRolesInteractor(_MockMapper.Object, _MockPersistenceContext.Object);

            // Act
            await _Interactor.HandleAsync(new GetEmployeeRolesRequest(), _MockPresenter.Object, _CancellationToken);

            // Assert
            _ = _Actual.Should().BeEquivalentTo(_EmployeeRoleDtos);

            _MockPersistenceContext.Verify(mock => mock.GetEntitiesAsync<EmployeeRole>(_CancellationToken));
            _MockPresenter.Verify(mock => mock.PresentAsync(It.IsAny<IQueryable<EmployeeRoleDto>>(), _CancellationToken));

            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
