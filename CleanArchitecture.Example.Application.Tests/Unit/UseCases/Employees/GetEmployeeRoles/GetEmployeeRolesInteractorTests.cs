using AutoMapper;
using CleanArchitecture.Example.Application.Services;
using CleanArchitecture.Example.Application.UseCases.Employees.GetEmployeeRoles;
using CleanArchitecture.Example.Domain.Enumerations;
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

            var _MockMapper = new Mock<IMapper>();
            _MockMapper
                .Setup(mock => mock.Map<List<EmployeeRoleDto>>(It.IsAny<IEnumerable<EmployeeRoleEnumeration>>()))
                .Returns(_EmployeeRoleDtos);

            var _MockPresenter = new Mock<IPresenter<IQueryable<EmployeeRoleDto>>>();

            var _Interactor = new GetEmployeeRolesInteractor(_MockMapper.Object);

            // Act
            await _Interactor.HandleAsync(new GetEmployeeRolesRequest(), _MockPresenter.Object, _CancellationToken);

            // Assert
            _MockMapper.Verify(mock => mock.Map<List<EmployeeRoleDto>>(It.IsAny<IEnumerable<EmployeeRoleEnumeration>>()));
            _MockPresenter.Verify(mock => mock.PresentAsync(It.IsAny<IQueryable<EmployeeRoleDto>>(), _CancellationToken));

            _MockMapper.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
