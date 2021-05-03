using AutoMapper;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.Employees.GetEmployeeRoles;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Persistence;
using Moq;
using System.Collections.Generic;
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
            var _CancellationToken = new CancellationToken();
            var _EmployeeRoleDtos = new List<EmployeeRoleDto>();
            var _EmployeeRoles = new[] { EmployeeRole.Admin }.AsQueryable();

            var _MockMapper = new Mock<IMapper>();
            _ = _MockMapper
                    .Setup(mock => mock.Map<List<EmployeeRoleDto>>(It.IsAny<IEnumerable<EmployeeRole>>()))
                    .Returns(_EmployeeRoleDtos);

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            _ = _MockPersistenceContext
                    .Setup(mock => mock.GetEntitiesAsync<EmployeeRole>(_CancellationToken))
                    .Returns(Task.FromResult(_EmployeeRoles));

            var _MockPresenter = new Mock<IPresenter<IQueryable<EmployeeRoleDto>>>();

            var _Interactor = new GetEmployeeRolesInteractor(_MockMapper.Object, _MockPersistenceContext.Object);

            // Act
            await _Interactor.HandleAsync(new GetEmployeeRolesRequest(), _MockPresenter.Object, _CancellationToken);

            // Assert
            _MockMapper.Verify(mock => mock.Map<List<EmployeeRoleDto>>(_EmployeeRoles));
            _MockPresenter.Verify(mock => mock.PresentAsync(It.IsAny<IQueryable<EmployeeRoleDto>>(), _CancellationToken));

            _MockMapper.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
